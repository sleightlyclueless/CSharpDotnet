using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("artist")]
public class ArtistController : ControllerBase {
    public ArtistController(DatabaseConnection _db) {
        db = _db;
    }

    private DatabaseConnection db { get; }

    // GET /artist
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        await db.Connection.OpenAsync();
        var result = await Artist.GetAllAsync(db);
        return new OkObjectResult(result);
    }

    // GET /artist/getByID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByID(int id) {
        await db.Connection.OpenAsync();
        Artist? result = await Artist.GetByIDAsync(id, db);
        return new OkObjectResult(result);
    }

    // GET /artist/getByName
    [HttpGet("getByName")]
    public async Task<IActionResult> GetByName(string name) {
        await db.Connection.OpenAsync();
        var result = await Artist.GetByNameAsync(name, db);
        return new OkObjectResult(result);
    }

    // GET /artist/getByExactName
    [HttpGet("getByExactName")]
    public async Task<IActionResult> GetByExactName(string name)
    {
        await db.Connection.OpenAsync();
        var result = await Artist.GetByExactNameAsync(name, db);
        return new OkObjectResult(result);
    }

    // POST /artist
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Artist artistFromBody) {
        await db.Connection.OpenAsync();
        int result = await artistFromBody.InsertAsync(db);
        return new OkObjectResult(result);
    }

    // PUT /artist/update
    [HttpPut("update")]
    public async Task<IActionResult> Update(int id, [FromBody] Artist artistFromBody) {
        await db.Connection.OpenAsync();
        Artist? result = await Artist.GetByIDAsync(id, db);
        result.FirstName = artistFromBody.FirstName;
        result.LastName = artistFromBody.LastName;
        result.ArtistName = artistFromBody.ArtistName;
        result.Password = artistFromBody.Password;

        int res = await result.UpdateAsync(db);
        return new OkObjectResult(res);
    }

    // DELETE /artist/delete
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id) {
        await db.Connection.OpenAsync();
        Artist result = await Artist.GetByIDAsync(id, db);
        await result.DeleteAsync(db);

        return new OkObjectResult(result);
    }
}
