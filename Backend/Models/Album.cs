using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Backend.Models;

public class Album {

    public int AlbumID { get; init; }
    public string AlbumName { get; set; }
    public long ReleaseDate { get; set; } // unix timestamp
    public string Misc { get; set; }
    public int ArtistID { get; set; }
    
    #region Misc

    private static async Task<List<Album>> ReturnAllAsync(DbDataReader _reader) {
        var albums = new List<Album>();

        await using (_reader) {
            while (await _reader.ReadAsync()) {
                Album album = new() {
                    AlbumID = _reader.GetInt32(0),
                    ReleaseDate = _reader.GetInt64(1),
                    Misc = _reader.GetString(2),
                    AlbumName = _reader.GetString(3),
                    ArtistID = _reader.GetInt32(4)
                };
                albums.Add(album);
            }
        }

        return albums;
    }
    
    #endregion

    #region CRUD

    //READ
    public static async Task<List<Album>> GetAllAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM album;";
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }

    //READ
    public static async Task<Album> GetByIDAsync(int _albumID, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM album WHERE albumID = @albumID;";
        BindID(cmd, _albumID);

        var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        return result.Count > 0 ? result[0] : null;
    }

    //READ
    public static async Task<List<Album>> GetByNameAsync(string _name, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM album WHERE albumName = @albumName;";
        BindAlbumName(cmd, _name);

        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //READ
    public static async Task<List<Album>> GetAllByArtistIDAsync(int _id, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM `album` WHERE `artistId`= @artistID;";
        BindArtistID(cmd, _id);

        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //READ
    public static async Task<List<Album>> GetAllByArtistNameAsync(string _name, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"SELECT * FROM `album` WHERE `artistId`= (
                SELECT `artistId` FROM artist WHERE `aName` = @artistName);";
        BindArtistName(cmd, _name);
        
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }
    
    //CREATE
    public async Task<int> InsertAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO album (releaseDate, misc, albumName, artistID) VALUES (@releaseDate, @misc, @albumName, @artistID);";
        BindToCurrentValues(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE album SET releaseDate=@releaseDate, misc=@misc, albumName=@albumName, artistID=@artistID WHERE albumID=@albumID;";
        BindToCurrentValues(cmd);
        BindID(cmd, AlbumID);

        return DatabaseConnection.ExecuteUpdateCommand(cmd).Result;
    }

    //DELETE
    public async Task DeleteAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM album WHERE albumID = @albumID;";
        BindID(cmd, AlbumID);

        await cmd.ExecuteNonQueryAsync();
    }

    #endregion

    #region Binding

    private static void BindID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumID",
            DbType = DbType.Int32,
            Value = _id
        });
    }
    
    private static void BindReleaseDate(MySqlCommand _cmd, long _releaseDate) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@releaseDate",
            DbType = DbType.Int64,
            Value = _releaseDate
        });
    }
    
    private static void BindMisc(MySqlCommand _cmd, string _misc) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@misc",
            DbType = DbType.String,
            Value = _misc
        });
    }
    
    private static void BindAlbumName(MySqlCommand _cmd, string _name) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumName",
            DbType = DbType.String,
            Value = _name
        });
    }
    
    private static void BindArtistID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@artistID",
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

    private void BindToCurrentValues(MySqlCommand _cmd) {
        BindReleaseDate(_cmd, ReleaseDate);
        BindMisc(_cmd, Misc);
        BindAlbumName(_cmd, AlbumName);
        BindArtistID(_cmd, ArtistID);
    }

    #endregion
}