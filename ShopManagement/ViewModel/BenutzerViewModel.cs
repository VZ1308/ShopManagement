using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;  // Hinzufügen für LINQ

public class BenutzerViewModel
{
    public ObservableCollection<Benutzer> BenutzerListe { get; set; } = new();

    public void AddBenutzer(Benutzer benutzer)
    {
        if (string.IsNullOrWhiteSpace(benutzer.Vorname) || string.IsNullOrWhiteSpace(benutzer.Nachname))
            throw new ArgumentException("Vorname und Nachname dürfen nicht leer sein!");

        if (!Regex.IsMatch(benutzer.Benutzername, @"^\w{5,}$"))
            throw new ArgumentException("Benutzername muss mindestens 5 Zeichen lang sein!");

        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "INSERT INTO Benutzer (Vorname, Nachname, Benutzername, Passwort) VALUES (@Vorname, @Nachname, @Benutzername, @Passwort)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Vorname", benutzer.Vorname);
        command.Parameters.AddWithValue("@Nachname", benutzer.Nachname);
        command.Parameters.AddWithValue("@Benutzername", benutzer.Benutzername);
        command.Parameters.AddWithValue("@Passwort", benutzer.Passwort);

        command.ExecuteNonQuery();
    }

    public void LoadBenutzer()
    {
        BenutzerListe.Clear();
        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "SELECT * FROM Benutzer";
        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            BenutzerListe.Add(new Benutzer
            {
                BenutzerID = (int)reader["BenutzerID"],
                Vorname = reader["Vorname"].ToString(),
                Nachname = reader["Nachname"].ToString(),
                Benutzername = reader["Benutzername"].ToString(),
                Passwort = reader["Passwort"].ToString()
            });
        }
    }

    public void UpdateBenutzer(int id, Benutzer updatedBenutzer)
    {
        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "UPDATE Benutzer SET Vorname = @Vorname, Nachname = @Nachname, Benutzername = @Benutzername, Passwort = @Passwort WHERE BenutzerID = @BenutzerID";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Vorname", updatedBenutzer.Vorname);
        command.Parameters.AddWithValue("@Nachname", updatedBenutzer.Nachname);
        command.Parameters.AddWithValue("@Benutzername", updatedBenutzer.Benutzername);
        command.Parameters.AddWithValue("@Passwort", updatedBenutzer.Passwort);
        command.Parameters.AddWithValue("@BenutzerID", id);

        command.ExecuteNonQuery();
    }

    public void DeleteBenutzer(int id)
    {
        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "DELETE FROM Benutzer WHERE BenutzerID = @BenutzerID";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@BenutzerID", id);

        command.ExecuteNonQuery();
    }
}
