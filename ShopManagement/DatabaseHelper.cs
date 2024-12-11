using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private static IConfiguration Configuration { get; set; }

    // Statische Initialisierung
    static DatabaseHelper()
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Basisverzeichnis der Anwendung
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Konfigurationsdatei
            Configuration = builder.Build();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Laden der Konfiguration: {ex.Message}");
            throw;
        }
    }

    // Methode zum Abrufen des Verbindungsstrings
    public static string GetConnectionString()
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Verbindungszeichenfolge 'DefaultConnection' wurde nicht gefunden.");
        }
        return connectionString;
    }

    // Methode zum Erstellen einer SQL-Verbindung
    public static SqlConnection CreateConnection()
    {
        string connectionString = GetConnectionString();
        return new SqlConnection(connectionString);
    }

    // Beispiel: Testen der Datenbankverbindung
    public static bool TestConnection()
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open(); // Öffnet die Verbindung zur Datenbank
                Console.WriteLine("Datenbankverbindung erfolgreich.");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Herstellen der Verbindung: {ex.Message}");
            return false;
        }
    }
}
