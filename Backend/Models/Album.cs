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

    private static async Task<List<Album>> ReturnAllAsync(DbDataReader _reader, DatabaseConnection _db) {
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
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync(), _db);
    }

    //READ
    public static async Task<Album> GetByIDAsync(int _albumID, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM album WHERE albumID = @albumID;";
        BindID(cmd, _albumID);

        var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync(), _db);
        return result.Count > 0 ? result[0] : null;
    }
    
    //todo: get all albums of artists

    //CREATE
    public async Task<int> InsertAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO album (releaseDate, misc, albumName, artistID) VALUES (@releaseDate, @misc, @albumName, @artistID);";
        BindParameter(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE album SET releaseDate=@releaseDate, misc=@misc, albumName=@albumName, artistID=@artistID WHERE albumID=@albumID;";
        BindParameter(cmd);
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

    private void BindParameter(MySqlCommand _cmd) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@releaseDate",
            DbType = DbType.Int64,
            Value = ReleaseDate
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@misc",
            DbType = DbType.String,
            Value = Misc
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@albumName",
            DbType = DbType.String,
            Value = AlbumName
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@artistID",
            DbType = DbType.String,
            Value = ArtistID
        });
    }

    #endregion
}