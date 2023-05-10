using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Backend.Models;

public class Song {
    private Song(DatabaseConnection _db) {
        db = _db;
    }

    public int SongID { get; init; }

    public string Title { get; init; }
    public int Length { get; init; }
    private int AlbumID { get; init; }

    private DatabaseConnection db { get; }

    #region Misc

    private async Task<List<Song>> ReturnAllAsync(DbDataReader _reader) {
        var songs = new List<Song>();

        await using (_reader) {
            while (await _reader.ReadAsync()) {
                Song song = new(db) {
                    SongID = _reader.GetInt32(0),
                    Title = _reader.GetString(1),
                    Length = _reader.GetInt32(2),
                    AlbumID = _reader.GetInt32(3)
                };
                songs.Add(song);
            }
        }

        return songs;
    }

    #endregion

    #region CRUD

    //READ
    public async Task<List<Song>> GetAllAsync() {
        await using MySqlCommand cmd = db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM song;";
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }

    //READ
    public async Task<Song> GetByIDAsync(int _songID) {
        await using MySqlCommand cmd = db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM song WHERE songID = @songID;";
        BindID(cmd, _songID);

        var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        return result.Count > 0 ? result[0] : null;
    }
    
    //todo: get all songs of artists
    //todo: get all songs of album

    //CREATE
    public async Task<int> InsertAsync() {
        await using MySqlCommand cmd = db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO song (title, length, albumID) VALUES (@title, @length, @albumID);";
        BindParameter(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync() {
        await using MySqlCommand cmd = db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE song SET title=@title, length=@length, albumID=@albumID WHERE songID=@songID;";
        BindParameter(cmd);
        BindID(cmd, SongID);

        return DatabaseConnection.ExecuteUpdateCommand(cmd).Result;
    }

    //DELETE
    public async Task DeleteAsync() {
        await using MySqlCommand cmd = db.Connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM song WHERE songID = @songID;";
        BindID(cmd, SongID);

        await cmd.ExecuteNonQueryAsync();
    }

    #endregion

    #region Binding

    private void BindID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@songID",
            DbType = DbType.Int32,
            Value = _id
        });
    }

    private void BindParameter(MySqlCommand _cmd) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@title",
            DbType = DbType.String,
            Value = Title
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@length",
            DbType = DbType.Int32,
            Value = Length
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumID",
            DbType = DbType.String,
            Value = AlbumID
        });
    }

    #endregion
}