namespace Backend;
using MySqlConnector;

public class DatabaseConnection
{
    private readonly bool USE_DEFAULT_DATABASE = true; //otherwise its the env variable
    private readonly string DATABASE_ENV_VAR = "DATABASE_URL";

    private readonly string DEFAULT_DATABASE = "server=127.0.0.1;uid=user;pwd=user;database=musicdb";
    
    public MySqlConnection Connection { get; }

    public DatabaseConnection()
    {
        var databaseURL = USE_DEFAULT_DATABASE ? DEFAULT_DATABASE : Environment.GetEnvironmentVariable("DATABASE_URL");
        Connection = new MySqlConnection(databaseURL);
    }

    public void Dispose() => Connection.Dispose();

    public static async Task<int> ExecuteCreateCommand(MySqlCommand _cmd) {
        try {
            await _cmd.ExecuteNonQueryAsync();
            int id = (int)_cmd.LastInsertedId;
            return id;
        } catch (Exception e) {
            await Console.Error.WriteLineAsync(e.ToString());
            return 0;
        }
    }
    
    public static async Task<int> ExecuteUpdateCommand(MySqlCommand _cmd) {
        try {
            return await _cmd.ExecuteNonQueryAsync();
        } catch (Exception e) {
            await Console.Error.WriteLineAsync(e.ToString());
        }
        return 0;
    }
}