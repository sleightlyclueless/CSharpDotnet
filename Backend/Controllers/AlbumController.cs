using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

//[HttpDelete("id")]

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
    private readonly ILogger<ArtistController> _logger;

    public AlbumController(ILogger<ArtistController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAllAlbums")]
    public IEnumerable<Artist> GetAllArtists()
    {
        return null;
    }
}