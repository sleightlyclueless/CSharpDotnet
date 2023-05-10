using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Backend.Models;

public class Artist {
    private int ArtistID { get; init; }

    public string FirstName { get; set; } //nullable field

    public string LastName { get; set; } //nullable field

    public string ArtistName { get; set; } // used as username

    public string Password { get; set; }



    #region Misc

    private static async Task<List<Artist>> ReturnAllAsync(DatabaseConnection _db, DbDataReader _reader) {
        var artists = new List<Artist>();

        await using (_reader) {
            while (await _reader.ReadAsync()) {
                Artist artist = new() {
                    ArtistID = _reader.GetInt32(0),
                    FirstName = _reader.GetString(1),
                    LastName = _reader.GetString(2),
                    ArtistName = _reader.GetString(3),
                    Password = _reader.GetString(4)
                };
                artists.Add(artist);
            }
        }

        return artists;
    }

    #endregion

    #region CRUD

    //READ
    public static async Task<List<Artist>> GetAllAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM artist;";
        return await ReturnAllAsync(_db, await cmd.ExecuteReaderAsync());
    }

    //READ
    public static async Task<Artist> GetByIDAsync(int _artistID, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM artist WHERE artistID = @artistID;";
        BindID(cmd, _artistID);

        var result = await ReturnAllAsync(_db, await cmd.ExecuteReaderAsync());
        return result.Count > 0 ? result[0] : null;
    }

    //CREATE
    public async Task<int> InsertAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO artist (fName, lName, aName, password) VALUES (@fName, @lName, @aName, @password);";
        BindParameter(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE artist SET fName=@fName, lName=@lName, aName=@aName, password=@password WHERE artistID=@artistID;";
        BindParameter(cmd);
        BindID(cmd, ArtistID);

        return DatabaseConnection.ExecuteUpdateCommand(cmd).Result;
    }

    //DELETE
    public async Task DeleteAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM artist WHERE artistID = @artistID;";
        BindID(cmd, ArtistID);

        await cmd.ExecuteNonQueryAsync();
    }

    #endregion

    #region Binding

    private static void BindID(MySqlCommand _cmd, int _id) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@artistID",
            DbType = DbType.Int32,
            Value = _id
        });
    }

    private void BindParameter(MySqlCommand _cmd) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@fName",
            DbType = DbType.Int64,
            Value = FirstName
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@lName",
            DbType = DbType.String,
            Value = LastName
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@aName",
            DbType = DbType.String,
            Value = ArtistName
        });
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@password",
            DbType = DbType.String,
            Value = Password
        });
    }

    #endregion
}