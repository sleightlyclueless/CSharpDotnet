using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("song")]
public class SongController : ControllerBase {
    public SongController(DatabaseConnection _db) {
        db = _db;
    }

    private DatabaseConnection db { get; }

    // GET /song
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllAsync(db);
        return new OkObjectResult(result);
    }

    // GET /song/getByID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(int id) {
        await db.Connection.OpenAsync();
        Song? result = await Song.GetByIDAsync(id, db);
        return new OkObjectResult(result);
    }
    
    // GET /song/getAllByTitle
    [HttpGet("getAllByTitle")]
    public async Task<IActionResult> GetByTitle(string title) {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllByTitle(title, db);
        return new OkObjectResult(result);
    }
    
    // GET /song/getAllByArtistID
    [HttpGet("getAllByArtistID")]
    public async Task<IActionResult> GetAllByArtistID(int id) {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllByArtistID(id, db);
        return new OkObjectResult(result);
    }

    // GET /song/getAllByArtistName
    [HttpGet("getAllByArtistName")]
    public async Task<IActionResult> GetAllByArtistName(string name) {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllByArtistName(name, db);
        return new OkObjectResult(result);
    }
    
    // GET /song/getAllByAlbumID
    [HttpGet("getAllByAlbumID")]
    public async Task<IActionResult> GetAllByAlbumID(int id) {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllByAlbumID(id, db);
        return new OkObjectResult(result);
    }
    
    // GET /song/getAllByAlbumName
    [HttpGet("getAllByAlbumName")]
    public async Task<IActionResult> GetAllByAlbumName(string name) {
        await db.Connection.OpenAsync();
        var result = await Song.GetAllByAlbumName(name, db);
        return new OkObjectResult(result);
    }

    // POST /song
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Song songFromBody) {
        await db.Connection.OpenAsync();
        int result = await songFromBody.InsertAsync(db);
        return new OkObjectResult(result);
    }

    // PUT /song/update
    [HttpPut("update")]
    public async Task<IActionResult> Update(int id, [FromBody] Song songFromBody) {
        await db.Connection.OpenAsync();
        Song? result = await Song.GetByIDAsync(id, db);
        result.Title = songFromBody.Title;
        result.Length = songFromBody.Length;
        result.AlbumID = songFromBody.AlbumID;

        int res = await result.UpdateAsync(db);
        return new OkObjectResult(res);
    }

    // DELETE /song/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id) {
        await db.Connection.OpenAsync();
        Song result = await Song.GetByIDAsync(id, db);
        await result.DeleteAsync(db);
        return new OkObjectResult(result);
    }
}
