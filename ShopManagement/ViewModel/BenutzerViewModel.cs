using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using MySqlConnector;

namespace ShopManagement.ViewModel
{
    public class BenutzerViewModel
    {
        private readonly DatabaseHelper _dbHelper;

        public ObservableCollection<Benutzer> BenutzerListe { get; set; } = new();

        public BenutzerViewModel(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void AddBenutzer(Benutzer benutzer)
        {
            if (string.IsNullOrWhiteSpace(benutzer.Vorname) || string.IsNullOrWhiteSpace(benutzer.Nachname))
                throw new ArgumentException("Vorname und Nachname dürfen nicht leer sein!");

            if (!Regex.IsMatch(benutzer.Benutzername, @"^\w{5,}$"))
                throw new ArgumentException("Benutzername muss mindestens 5 Zeichen lang sein!");

            using var connection = _dbHelper.GetConnection();
            const string query = @"INSERT INTO Benutzer (Vorname, Nachname, Benutzername, Passwort) 
                                   VALUES (@Vorname, @Nachname, @Benutzername, @Passwort)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Vorname", benutzer.Vorname);
            command.Parameters.AddWithValue("@Nachname", benutzer.Nachname);
            command.Parameters.AddWithValue("@Benutzername", benutzer.Benutzername);
            command.Parameters.AddWithValue("@Passwort", benutzer.Passwort);

            command.ExecuteNonQuery();
        }

        public void LoadBenutzer()
        {
            BenutzerListe.Clear();

            using var connection = _dbHelper.GetConnection();
            const string query = "SELECT BenutzerID, Vorname, Nachname, Benutzername, Passwort FROM Benutzer";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                BenutzerListe.Add(new Benutzer
                {
                    BenutzerID = reader.GetInt32("BenutzerID"),
                    Vorname = reader.GetString("Vorname"),
                    Nachname = reader.GetString("Nachname"),
                    Benutzername = reader.GetString("Benutzername"),
                    Passwort = reader.GetString("Passwort")
                });
            }
        }

        public void UpdateBenutzer(int id, Benutzer updatedBenutzer)
        {
            using var connection = _dbHelper.GetConnection();
            const string query = @"UPDATE Benutzer 
                                   SET Vorname = @Vorname, Nachname = @Nachname, 
                                       Benutzername = @Benutzername, Passwort = @Passwort 
                                   WHERE BenutzerID = @BenutzerID";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Vorname", updatedBenutzer.Vorname);
            command.Parameters.AddWithValue("@Nachname", updatedBenutzer.Nachname);
            command.Parameters.AddWithValue("@Benutzername", updatedBenutzer.Benutzername);
            command.Parameters.AddWithValue("@Passwort", updatedBenutzer.Passwort);
            command.Parameters.AddWithValue("@BenutzerID", id);

            command.ExecuteNonQuery();
        }

        public void DeleteBenutzer(int id)
        {
            using var connection = _dbHelper.GetConnection();
            const string query = "DELETE FROM Benutzer WHERE BenutzerID = @BenutzerID";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@BenutzerID", id);

            command.ExecuteNonQuery();
        }
    }
}
