using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Backend.Models;

public class Artist {
    public int ArtistID { get; init; }

    public string FirstName { get; set; } //nullable field

    public string LastName { get; set; } //nullable field

    public string ArtistName { get; set; } // used as username

    public string Password { get; set; }


    #region Misc

    private static async Task<List<Artist>> ReturnAllAsync(DbDataReader _reader) {
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
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }

    public static async Task<List<Artist>> GetByNameAsync(string _name, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM `artist` WHERE `aName` LIKE @aName;";
        _name = "%" + _name + "%";
        BindArtistName(cmd, _name);
        return await ReturnAllAsync(await cmd.ExecuteReaderAsync());
    }

    //READ
    public static async Task<Artist> GetByIDAsync(int _artistID, DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText = @"SELECT * FROM artist WHERE artistID = @artistID;";
        BindID(cmd, _artistID);

        var result = await ReturnAllAsync(await cmd.ExecuteReaderAsync());
        return result.Count > 0 ? result[0] : null;
    }

    //CREATE
    public async Task<int> InsertAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"INSERT INTO artist (fName, lName, aName, password) VALUES (@fName, @lName, @aName, @password);";
        BindToCurrentValues(cmd);

        return DatabaseConnection.ExecuteCreateCommand(cmd).Result;
    }

    //UPDATE
    public async Task<int> UpdateAsync(DatabaseConnection _db) {
        await using MySqlCommand cmd = _db.Connection.CreateCommand();
        cmd.CommandText =
            @"UPDATE artist SET fName=@fName, lName=@lName, aName=@aName, password=@password WHERE artistID=@artistID;";
        BindToCurrentValues(cmd);
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

    private static void BindFirstName(MySqlCommand _cmd, string _fName) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@fName",
            DbType = DbType.Int64,
            Value = _fName
        });
    }

    private static void BindLastName(MySqlCommand _cmd, string _lName) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@lName",
            DbType = DbType.String,
            Value = _lName
        });
    }

    private static void BindArtistName(MySqlCommand _cmd, string _aName) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@aName",
            DbType = DbType.String,
            Value = _aName
        });
    }

    private static void BindPassword(MySqlCommand _cmd, string _password) {
        _cmd.Parameters.Add(new MySqlParameter {
            ParameterName = "@password",
            DbType = DbType.String,
            Value = _password
        });
    }

    private void BindToCurrentValues(MySqlCommand _cmd) {
        BindFirstName(_cmd, FirstName);
        BindLastName(_cmd, LastName);
        BindArtistName(_cmd, ArtistName);
        BindPassword(_cmd, Password);
    }

    #endregion
}