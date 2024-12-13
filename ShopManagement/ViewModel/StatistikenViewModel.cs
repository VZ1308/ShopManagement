using System;
using System.Collections.Generic;
using MySqlConnector;

public class StatistikenViewModel
{
    private readonly DatabaseHelper _dbHelper;

    public StatistikenViewModel(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    // Methode, um die Anzahl der Bestellungen pro Benutzer zu erhalten
    public int GetAnzahlBestellungenProNutzer(int benutzerID)
    {
        using var connection = _dbHelper.GetConnection();

        const string query = "SELECT COUNT(*) FROM Bestellung WHERE BenutzerID = @BenutzerID";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@BenutzerID", benutzerID);

        return Convert.ToInt32(command.ExecuteScalar());
    }

    // Methode, um den Monatsumsatz zu erhalten (basierend auf Monat und Jahr)
    public decimal GetMonatsumsatz(int monat, int jahr)
    {
        using var connection = _dbHelper.GetConnection();

        const string query = @"
            SELECT SUM(Gesamtbetrag) 
            FROM Bestellungen 
            WHERE MONTH(Bestelldatum) = @Monat AND YEAR(Bestelldatum) = @Jahr";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Monat", monat);
        command.Parameters.AddWithValue("@Jahr", jahr);

        return Convert.ToDecimal(command.ExecuteScalar() ?? 0);
    }

    // Methode, die die Anzahl der Bestellungen pro Kunde zurückgibt
    public List<KundenBestellungenStatistik> BestellungenProKunde()
    {
        var statistikListe = new List<KundenBestellungenStatistik>();

        using var connection = _dbHelper.GetConnection();

        const string query = @"
            SELECT BenutzerID, COUNT(*) AS AnzahlBestellungen 
            FROM Bestellung 
            GROUP BY BenutzerID";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            statistikListe.Add(new KundenBestellungenStatistik
            {
                KundenID = reader.GetInt32("BenutzerID"),
                AnzahlBestellungen = reader.GetInt32("AnzahlBestellungen")
            });
        }

        return statistikListe;
    }
}
