namespace PocketSizedUniverseSeeder;

class Program
{
    static void Main(string[] args)
    {
        var poller = new TorrentPoller();
        while (true)
        {
            poller.Poll();
        }
    }
}