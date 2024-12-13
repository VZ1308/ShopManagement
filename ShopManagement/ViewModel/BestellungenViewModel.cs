using System;
using System.Collections.Generic;
using MySqlConnector;

namespace ShopManagement.ViewModel
{
    public class BestellungenViewModel
    {
        private readonly DatabaseHelper _dbHelper;

        public List<Bestellung> BestellungsListe { get; private set; } = new();

        public BestellungenViewModel(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        // Lädt alle Bestellungen aus der Datenbank
        public void LoadBestellungen()
        {
            BestellungsListe.Clear();

            try
            {
                using var connection = _dbHelper.GetConnection();
                const string query = "SELECT BestellungID, BenutzerID, Beschreibung, Anzahl, Gesamtbetrag FROM Bestellung";

                using var command = new MySqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BestellungsListe.Add(new Bestellung
                    {
                        BestellungID = reader["BestellungID"] != DBNull.Value ? reader.GetInt32("BestellungID") : 0,
                        BenutzerID = reader["BenutzerID"] != DBNull.Value ? reader.GetInt32("BenutzerID") : 0,
                        Beschreibung = reader["Beschreibung"] != DBNull.Value ? reader.GetString("Beschreibung") : string.Empty,
                        Anzahl = reader["Anzahl"] != DBNull.Value ? reader.GetInt32("Anzahl") : 0,
                        Gesamtbetrag = reader["Gesamtbetrag"] != DBNull.Value ? reader.GetDecimal("Gesamtbetrag") : 0m
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Bestellungen: {ex.Message}");
            }
        }

        // Fügt eine neue Bestellung hinzu
        public void AddBestellung(Bestellung neueBestellung)
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                const string query = @"INSERT INTO Bestellung (BenutzerID, Beschreibung, Anzahl, Gesamtbetrag) 
                                       VALUES (@BenutzerID, @Beschreibung, @Anzahl, @Gesamtbetrag)";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@BenutzerID", neueBestellung.BenutzerID);
                command.Parameters.AddWithValue("@Beschreibung", neueBestellung.Beschreibung);
                command.Parameters.AddWithValue("@Anzahl", neueBestellung.Anzahl);
                command.Parameters.AddWithValue("@Gesamtbetrag", neueBestellung.Gesamtbetrag);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Hinzufügen der Bestellung: {ex.Message}");
            }
        }

        // Aktualisiert eine Bestellung
        public void UpdateBestellung(int bestellungID, Bestellung aktualisierteBestellung)
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                const string query = @"UPDATE Bestellung 
                                       SET BenutzerID = @BenutzerID, Beschreibung = @Beschreibung, 
                                           Anzahl = @Anzahl, Gesamtbetrag = @Gesamtbetrag 
                                       WHERE BestellungID = @BestellungID";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@BestellungID", bestellungID);
                command.Parameters.AddWithValue("@BenutzerID", aktualisierteBestellung.BenutzerID);
                command.Parameters.AddWithValue("@Beschreibung", aktualisierteBestellung.Beschreibung);
                command.Parameters.AddWithValue("@Anzahl", aktualisierteBestellung.Anzahl);
                command.Parameters.AddWithValue("@Gesamtbetrag", aktualisierteBestellung.Gesamtbetrag);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Aktualisieren der Bestellung: {ex.Message}");
            }
        }

        // Löscht eine Bestellung
        public void DeleteBestellung(int bestellungID)
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                const string query = "DELETE FROM Bestellung WHERE BestellungID = @BestellungID";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@BestellungID", bestellungID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Löschen der Bestellung: {ex.Message}");
            }
        }
    }
}