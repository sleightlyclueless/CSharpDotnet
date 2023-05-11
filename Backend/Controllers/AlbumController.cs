using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("album")]
public class AlbumController : ControllerBase
{
    public AlbumController(DatabaseConnection _db)
    {
        db = _db;
    }
    
    private DatabaseConnection db { get; }

    // GET /album
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        await db.Connection.OpenAsync();
        var result = await Album.GetAllAsync(db);
        return new OkObjectResult(result);
    }
    
    // GET /album/byID
    [HttpGet("byID")]
    public async Task<IActionResult> GetByID(int id) {
        await db.Connection.OpenAsync();
        Album? result = await Album.GetByIDAsync(id, db);
        return new OkObjectResult(result);
    }
    
    // POST /album
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Album albumFromBody) {
        await db.Connection.OpenAsync();
        int result = await albumFromBody.InsertAsync(db);
        return new OkObjectResult(result);
    }
    
    // PUT /album/update
    [HttpPut("update")]
    public async Task<IActionResult> Update(int id, [FromBody] Album albumFromBody) {
        await db.Connection.OpenAsync();
        Album? result = await Album.GetByIDAsync(id, db);
        result.ReleaseDate = albumFromBody.ReleaseDate;
        result.Misc = albumFromBody.Misc;
        result.AlbumName = albumFromBody.AlbumName;
        result.ArtistID = albumFromBody.ArtistID;
        
        int res = await result.UpdateAsync(db);
        return new OkObjectResult(res);
    }
    
    // DELETE /album/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await db.Connection.OpenAsync();
        Album result = await Album.GetByIDAsync(id, db);
        await result.DeleteAsync(db);
        
        return new OkObjectResult(result);
    }
}