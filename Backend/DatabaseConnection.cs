namespace Backend;
using MySqlConnector;

public class DatabaseConnection
{
    private readonly bool USE_DEFAULT_DATABASE = true; //otherwise its 
    private readonly string DEFAULT_DATABASE = "localhost";
    private readonly string DATABASE_ENV_VAR = "DATABASE_URL";
    
    public MySqlConnection Connection { get; }

    public Database(string connectionString)
    {
        string databaseURL;
        if (USE_DEFAULT_DATABASE)
        {
            databaseURL = DEFAULT_DATABASE;
        }
        else
        {
            .Environment.GetEnvironmentVariable("DATABASE_URL");
        }
        Connection = new MySqlConnection();
    }

    public void Dispose() => Connection.Dispose();
}