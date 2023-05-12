using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Backend.Models;

public class Song {
    public int SongID { get; init; }
    public string Title { get; set; }
    public int Length { get; set; }
    public int AlbumID { get; set; }
    
    #region Misc

    private static async Task<List<Song>> ReturnAllAsync(DbDataReader _reader) {
        var songs = new List<Song>();

        await using (_reader) {
            while (await _reader.ReadAsync()) {
                Song song = new() {
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
    public static async Task<List<Song>> GetAllAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM song;";
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }

    //READ
    public static async Task<Song> GetByIDAsync(int _songID, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM song WHERE songID = @songID;";
        BindID(cmd, _songID);

        var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        return result.Count > 0 ? result[0] : null;
    }
    
    //get by artist
    //READ
    //todo
    public static async Task<List<Song>> GetAllByArtistID(int _id, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"SELECT * FROM `song` WHERE `albumId` IN((
                SELECT `albumId` FROM `album` WHERE `artistID` = @artistId));";
        BindArtistID(cmd, _id);
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //READ
    //todo
    public static async Task<List<Song>> GetAllByArtistName(string _name, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"SELECT * FROM `song` WHERE `albumId` IN( (
                SELECT `albumId` FROM `album` WHERE `artistID`= (
                    SELECT `artistId` FROM `artist` WHERE `aName` = @artistName)));";
        BindArtistName(cmd, _name);
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //get by album
    //READ
    //todo
    public static async Task<List<Song>> GetAllByAlbumID(int _id, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM `song` WHERE `albumId` = @albumID;";
        BindAlbumID(cmd, _id);
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //READ
    //todo
    public static async Task<List<Song>> GetAllByAlbumName(string _name, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"SELECT * FROM `song` WHERE `albumId` = (
                SELECT `albumId` FROM `album` where `albumName` =  @albumName);";
        BindAlbumName(cmd, _name);
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    

    //CREATE
    public async Task<int> InsertAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO song (title, length, albumID) VALUES (@title, @length, @albumID);";
        BindToCurrentValues(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE song SET title=@title, length=@length, albumID=@albumID WHERE songID=@songID;";
        BindToCurrentValues(cmd);
        BindID(cmd, SongID);

        return DatabaseConnection.ExecuteUpdateCommand(cmd).Result;
    }

    //DELETE
    public async Task DeleteAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM song WHERE songID = @songID;";
        BindID(cmd, SongID);

        await cmd.ExecuteNonQueryAsync();
    }

    #endregion

    #region Binding

    private static void BindID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@songID",
            DbType = DbType.Int32,
            Value = _id
        });
    }
    
    private static void BindArtistID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@artistId",
            DbType = DbType.Int32,
            Value = _id
        });
    }
    
    private static void BindArtistName(MySqlCommand _cmd, string _name) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@artistName",
            DbType = DbType.String,
            Value = _name
        });
    }
    
    private static void BindTitle(MySqlCommand _cmd, string _title) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@title",
            DbType = DbType.String,
            Value = _title
        });
    }
    
    private static void BindLength(MySqlCommand _cmd, int _length) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@length",
            DbType = DbType.Int32,
            Value = _length
        });
    }
    
    private static void BindAlbumID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumID",
            DbType = DbType.String,
            Value = _id
        });
    }
    
    private static void BindAlbumName(MySqlCommand _cmd, string _name) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumName",
            DbType = DbType.String,
            Value = _name
        });
    }

    private void BindToCurrentValues(MySqlCommand _cmd) {
        BindTitle(_cmd, Title);
        BindLength(_cmd, Length);
        BindAlbumID(_cmd, AlbumID);
    }

    #endregion
}