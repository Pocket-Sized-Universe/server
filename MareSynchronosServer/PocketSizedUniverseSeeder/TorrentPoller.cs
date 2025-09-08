using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MonoTorrent;
using MonoTorrent.Client;
using PocketSizedUniverse.API.Dto.Files;
using PocketSizedUniverse.API.Routes;

namespace PocketSizedUniverseSeeder;

public class TorrentPoller
{
    public Uri PollingEndpoint => MareFiles.GetSuperSeederPackage(new Uri(Env.BaseUrl), Env.BatchSize);

    public string CacheDirectory => Path.Combine(Env.FilePath, "Cache");
    public string TorrentsDirectory => Path.Combine(Env.FilePath, "Torrents");
    public string FilesDirectory => Path.Combine(Env.FilePath, "Files");

    private ClientEngine _clientEngine;
    private HttpClient _client;

    public TorrentPoller()
    {
        var settings = new EngineSettingsBuilder()
        {
            CacheDirectory = CacheDirectory,
            AllowLocalPeerDiscovery = true,
            AllowPortForwarding = true,
            ListenEndPoints = new(StringComparer.Ordinal)
            {
                { "IPv4", new IPEndPoint(IPAddress.Parse("0.0.0.0"), 42069) },
                { "IPv6", new IPEndPoint(IPAddress.IPv6Any, 42069) }
            },
            DhtEndPoint = new IPEndPoint(IPAddress.Any, 42070),
        };
        Directory.CreateDirectory(TorrentsDirectory);
        Directory.CreateDirectory(FilesDirectory);
        Directory.CreateDirectory(CacheDirectory);
        _client = new HttpClient();
        _clientEngine = new ClientEngine(settings.ToSettings());
        _clientEngine.StartAllAsync().GetAwaiter().GetResult();
        Console.WriteLine("Started BitTorrent engine");
    }

    private DateTime _lastPolled = DateTime.MinValue;

    public void Poll()
    {
        if (_lastPolled >= DateTime.Now + TimeSpan.FromSeconds(Env.PollingInterval))
            return;
        _lastPolled = DateTime.Now;
        HttpResponseMessage torrentsResponse = _client.GetAsync(PollingEndpoint).Result;
        if (!torrentsResponse.IsSuccessStatusCode)
            throw new InvalidOperationException($"Torrent polling failed: {torrentsResponse.StatusCode}");
        string torrent = torrentsResponse.Content.ReadAsStringAsync().Result;
        List<TorrentFileDto>? torrents = JsonSerializer.Deserialize<List<TorrentFileDto>>(torrent);
        if (torrents == null)
            throw new InvalidOperationException($"Torrent polling failed: Unable to deserialize {torrent}");
        foreach (TorrentFileDto torrentFile in torrents)
        {
            Console.WriteLine($"Starting torrent {torrentFile.TorrentName}");
            EnsureTorrentFileAndStart(torrentFile);
        }

        Console.WriteLine(TorrentReportString);
    }

    public void EnsureTorrentFileAndStart(TorrentFileDto torrentFileDto)
    {
        var torrent = Torrent.LoadAsync(torrentFileDto.Data).ConfigureAwait(false).GetAwaiter().GetResult();
        VerifyAndStartTorrent(torrent);
    }

    private void VerifyAndStartTorrent(Torrent torrent)
    {
        if (_clientEngine.Torrents.Any(t => string.Equals(t.Name, torrent.Name, StringComparison.OrdinalIgnoreCase)))
            return;
        var manager = _clientEngine.AddAsync(torrent, FilesDirectory).ConfigureAwait(false).GetAwaiter().GetResult();
        manager.HashCheckAsync(true).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public string TorrentReportString =>
        $"REPORT | S: {_clientEngine.Torrents.Count(t => t.State == TorrentState.Seeding)} | L: {_clientEngine.Torrents.Count(t => t.State != TorrentState.Seeding)}";
}