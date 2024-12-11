using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class StatistikenViewModel
{
    // Methode, um die Anzahl der Bestellungen pro Benutzer zu erhalten
    public int GetAnzahlBestellungenProNutzer(int benutzerID)
    {
        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "SELECT COUNT(*) FROM Bestellung WHERE BenutzerID = @BenutzerID";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@BenutzerID", benutzerID);

        return (int)command.ExecuteScalar();
    }

    // Methode, um den Monatsumsatz zu erhalten (basierend auf Monat und Jahr)
    public decimal GetMonatsumsatz(int monat, int jahr)
    {
        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "SELECT SUM(Gesamtbetrag) FROM Bestellung WHERE MONTH(Bestelldatum) = @Monat AND YEAR(Bestelldatum) = @Jahr";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Monat", monat);
        command.Parameters.AddWithValue("@Jahr", jahr);

        return (decimal)(command.ExecuteScalar() ?? 0);
    }

    // Methode, die die Anzahl der Bestellungen pro Kunde zurückgibt
    public List<KundenBestellungenStatistik> BestellungenProKunde()
    {
        var statistikListe = new List<KundenBestellungenStatistik>();

        using var connection = new SqlConnection(DatabaseHelper.GetConnectionString());
        connection.Open();

        string query = "SELECT BenutzerID, COUNT(*) AS AnzahlBestellungen " +
                       "FROM Bestellung " +
                       "GROUP BY BenutzerID";
        using var command = new SqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            statistikListe.Add(new KundenBestellungenStatistik
            {
                KundenID = reader.GetInt32(0),
                AnzahlBestellungen = reader.GetInt32(1)
            });
        }

        return statistikListe;
    }
}

