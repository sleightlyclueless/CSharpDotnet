using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

//[HttpDelete("id")]

[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
    private readonly ILogger<ArtistController> _logger;

    private DatabaseConnection Connection { get; set; }
    
    
    private readonly Artist[] _artists = {
        new Artist { ID = 0, FirstName = "Samuel", LastName = "Koob", ArtistName = "AMOGUS", Password = "amongo" },
        new Artist
        {
            ID = 1, FirstName = "Sebastian", LastName = "Zill", ArtistName = "DJ Firehydrant", Password = "amgogo"
        },
        new Artist
        {
            ID = 2, FirstName = "Mirco", LastName = "Janisch", ArtistName = "ca$h-flow-069", Password = "PekkaRockt"
        }
    };


    public ArtistController(ILogger<ArtistController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Artist> GetAllArtists()
    {
        return _artists;
    }
    

}
