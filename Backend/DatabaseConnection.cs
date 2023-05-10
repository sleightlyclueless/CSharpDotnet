namespace Backend;
using MySqlConnector;

public class DatabaseConnection
{
    private readonly bool USE_DEFAULT_DATABASE = true; //otherwise its 

    private readonly string DEFAULT_DATABASE =
        "server=127.0.0.1;user id=netuser;password=netpass;port=3306;database=university;";
    private readonly string DATABASE_ENV_VAR = "DATABASE_URL";
    
    public MySqlConnection Connection { get; }

    public DatabaseConnection()
    {
        string databaseURL;
        databaseURL = USE_DEFAULT_DATABASE ? DEFAULT_DATABASE : Environment.GetEnvironmentVariable("DATABASE_URL");
        Connection = new MySqlConnection(databaseURL);
    }

    public void Dispose() => Connection.Dispose();
}