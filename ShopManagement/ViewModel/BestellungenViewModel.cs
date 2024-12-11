using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ShopManagement.ViewModel
{

    public class BestellungenViewModel
    {
        public List<Bestellung> BestellungsListe { get; private set; } = new List<Bestellung>();

        // Bestellungen aus der Datenbank laden
        public void LoadBestellungen()
        {
            BestellungsListe.Clear();

            using (var connection = DatabaseHelper.CreateConnection())
            {
                string query = "SELECT BestellungID, BenutzerID, Beschreibung, Anzahl, Gesamtbetrag FROM Bestellungen";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BestellungsListe.Add(new Bestellung
                            {
                                BestellungID = reader.GetInt32(0),
                                BenutzerID = reader.GetInt32(1),
                                Beschreibung = reader.GetString(2),
                                Anzahl = reader.GetInt32(3),
                                Gesamtbetrag = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }
        }

        // Bestellung hinzufügen
        public void AddBestellung(Bestellung neueBestellung)
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                string query = "INSERT INTO Bestellungen (BenutzerID, Beschreibung, Anzahl, Gesamtbetrag) " +
                               "VALUES (@BenutzerID, @Beschreibung, @Anzahl, @Gesamtbetrag)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BenutzerID", neueBestellung.BenutzerID);
                    command.Parameters.AddWithValue("@Beschreibung", neueBestellung.Beschreibung);
                    command.Parameters.AddWithValue("@Anzahl", neueBestellung.Anzahl);
                    command.Parameters.AddWithValue("@Gesamtbetrag", neueBestellung.Gesamtbetrag);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Bestellung aktualisieren
        public void UpdateBestellung(int bestellungID, Bestellung aktualisierteBestellung)
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                string query = "UPDATE Bestellungen SET BenutzerID = @BenutzerID, Beschreibung = @Beschreibung, " +
                               "Anzahl = @Anzahl, Gesamtbetrag = @Gesamtbetrag WHERE BestellungID = @BestellungID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BestellungID", bestellungID);
                    command.Parameters.AddWithValue("@BenutzerID", aktualisierteBestellung.BenutzerID);
                    command.Parameters.AddWithValue("@Beschreibung", aktualisierteBestellung.Beschreibung);
                    command.Parameters.AddWithValue("@Anzahl", aktualisierteBestellung.Anzahl);
                    command.Parameters.AddWithValue("@Gesamtbetrag", aktualisierteBestellung.Gesamtbetrag);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Bestellung löschen
        public void DeleteBestellung(int bestellungID)
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                string query = "DELETE FROM Bestellungen WHERE BestellungID = @BestellungID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BestellungID", bestellungID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
