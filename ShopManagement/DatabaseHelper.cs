using Microsoft.Extensions.Configuration;
using MySqlConnector; // Bibliothek für MySQL-Datenbankzugriff

public class DatabaseHelper
{
    private readonly string _connectionString; // Speichert die Verbindungszeichenfolge

    // Konstruktor: Lädt die Verbindungszeichenfolge aus der IConfiguration
    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Methode zum Abrufen einer Datenbankverbindung
    public MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(_connectionString);
        connection.Open(); // Öffnet die Verbindung
        return connection; // Gibt die offene Verbindung zurück
    }
}
