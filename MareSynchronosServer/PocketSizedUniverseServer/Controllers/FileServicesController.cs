using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocketSizedUniverse.API.Dto.Files;
using PocketSizedUniverse.API.Routes;
using PocketSizedUniverseShared.Data;

namespace PocketSizedUniverseServer.Controllers;

[Route(MareFiles.Torrents)]
public class FileServicesController : Controller
{
    private readonly ILogger<FileServicesController> _logger;
    private readonly MareDbContext _mareDbContext;
    public FileServicesController(ILogger<FileServicesController> logger, MareDbContext mareDbContext)
    {
        _logger = logger;
        _mareDbContext = mareDbContext;
    }

    [HttpGet]
    [Route(MareFiles.Torrents_GetSuperSeederPackage)]
    public async Task<ActionResult<List<TorrentFileDto>>> GetSuperSeederPackage(int batchSize)
    {
        if (batchSize <= 0 || batchSize > 100)
            return BadRequest("Invalid batch size. Must be between 1 and 100.");
        var filesCount = _mareDbContext.TorrentFileEntries.Count();
        List<TorrentFileDto> files = new();
        long filesProcessed = 0;
        while (filesProcessed < batchSize)
        {
            var randomIndex = Random.Shared.Next(filesCount);
            var randomFile = await _mareDbContext.TorrentFileEntries
                .Skip(randomIndex)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            files.Add(new TorrentFileDto()
            {
                GamePath = randomFile.GamePath,
                Data = randomFile.TorrentData,
                Extension = randomFile.FileExtension,
                ForbiddenBy = randomFile.ForbiddenBy,
                Hash = randomFile.Hash,
                IsForbidden = randomFile.IsForbidden
            });
            filesProcessed++;
        }
        return files;
    }
}