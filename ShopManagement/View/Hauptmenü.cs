using ShopManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

public class HauptMenü
{
    private BenutzerViewModel _benutzerViewModel = new BenutzerViewModel();
    private BestellungenViewModel _bestellungenViewModel = new BestellungenViewModel();
    private StatistikenViewModel _statistikenViewModel = new StatistikenViewModel();

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Hauptmenü ===");
            Console.WriteLine("1. Benutzer verwalten");
            Console.WriteLine("2. Bestellungen verwalten");
            Console.WriteLine("3. Statistiken anzeigen");
            Console.WriteLine("4. Beenden");

            Console.Write("Wählen Sie eine Option: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1: BenutzerVerwalten(); break;
                    case 2: BestellungenVerwalten(); break;
                    case 3: StatistikenAnzeigen(); break;
                    case 4: return;
                    default: Console.WriteLine("Ungültige Auswahl!"); break;
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie eine Zahl ein.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }

    private void BenutzerVerwalten()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Benutzer verwalten ===");
            Console.WriteLine("1. Kunden hinzufügen");
            Console.WriteLine("2. Kunden anzeigen");
            Console.WriteLine("3. Kunden aktualisieren");
            Console.WriteLine("4. Kunden löschen");
            Console.WriteLine("5. Zurück");

            Console.Write("Wählen Sie eine Option: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1: KundeHinzufügen(); break;
                    case 2: KundenAnzeigen(); break;
                    case 3: KundeAktualisieren(); break;
                    case 4: KundeLöschen(); break;
                    case 5: return;
                    default: Console.WriteLine("Ungültige Auswahl!"); break;
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie eine gültige Zahl ein.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }

    private void KundeHinzufügen()
    {
        Console.Clear();
        Console.WriteLine("=== Kunde hinzufügen ===");
        Benutzer neuerBenutzer = new Benutzer();

        Console.Write("Vorname: ");
        neuerBenutzer.Vorname = Console.ReadLine();

        Console.Write("Nachname: ");
        neuerBenutzer.Nachname = Console.ReadLine();

        Console.Write("Benutzername: ");
        neuerBenutzer.Benutzername = Console.ReadLine();

        Console.Write("Passwort: ");
        neuerBenutzer.Passwort = Console.ReadLine();

        try
        {
            _benutzerViewModel.AddBenutzer(neuerBenutzer);
            Console.WriteLine("Kunde erfolgreich hinzugefügt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    private void KundenAnzeigen()
    {
        Console.Clear();
        Console.WriteLine("=== Kunden anzeigen ===");

        try
        {
            _benutzerViewModel.LoadBenutzer();
            foreach (var benutzer in _benutzerViewModel.BenutzerListe)
            {
                Console.WriteLine($"ID: {benutzer.BenutzerID}, Name: {benutzer.Vorname} {benutzer.Nachname}, Benutzername: {benutzer.Benutzername}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    private void KundeAktualisieren()
    {
        Console.Clear();
        Console.WriteLine("=== Kunde aktualisieren ===");

        Console.Write("Geben Sie die ID des Kunden ein: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var benutzer = _benutzerViewModel.BenutzerListe.FirstOrDefault(b => b.BenutzerID == id);
            if (benutzer == null)
            {
                Console.WriteLine("Kunde nicht gefunden!");
                return;
            }

            Console.WriteLine($"Aktuelle Werte: Vorname: {benutzer.Vorname}, Nachname: {benutzer.Nachname}, Benutzername: {benutzer.Benutzername}");

            Console.Write("Neuer Vorname (Leer lassen, um den alten Wert zu behalten): ");
            string vorname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(vorname)) benutzer.Vorname = vorname;

            Console.Write("Neuer Nachname (Leer lassen, um den alten Wert zu behalten): ");
            string nachname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nachname)) benutzer.Nachname = nachname;

            Console.Write("Neuer Benutzername (Leer lassen, um den alten Wert zu behalten): ");
            string benutzername = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(benutzername)) benutzer.Benutzername = benutzername;

            Console.Write("Neues Passwort (Leer lassen, um das alte zu behalten): ");
            string passwort = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(passwort)) benutzer.Passwort = passwort;

            try
            {
                _benutzerViewModel.UpdateBenutzer(id, benutzer);
                Console.WriteLine("Kunde erfolgreich aktualisiert!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ungültige ID!");
        }
    }

    private void KundeLöschen()
    {
        Console.Clear();
        Console.WriteLine("=== Kunde löschen ===");

        Console.Write("Geben Sie die ID des Kunden ein: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                _benutzerViewModel.DeleteBenutzer(id);
                Console.WriteLine("Kunde erfolgreich gelöscht!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ungültige ID!");
        }
    }

    private void BestellungenVerwalten()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Bestellungen verwalten ===");
            Console.WriteLine("1. Bestellung hinzufügen");
            Console.WriteLine("2. Bestellungen anzeigen");
            Console.WriteLine("3. Bestellung aktualisieren");
            Console.WriteLine("4. Bestellung löschen");
            Console.WriteLine("5. Zurück");

            Console.Write("Wählen Sie eine Option: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1: BestellungHinzufügen(); break;
                    case 2: BestellungenAnzeigen(); break;
                    case 3: BestellungAktualisieren(); break;
                    case 4: BestellungLöschen(); break;
                    case 5: return;
                    default: Console.WriteLine("Ungültige Auswahl!"); break;
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie eine gültige Zahl ein.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }

    private void BestellungHinzufügen()
    {
        Console.Clear();
        Console.WriteLine("=== Bestellung hinzufügen ===");
        Bestellung neueBestellung = new Bestellung();

        Console.Write("Kunden-ID: ");
        neueBestellung.BenutzerID = int.Parse(Console.ReadLine());

        Console.Write("Produktbeschreibung: ");
        neueBestellung.Beschreibung = Console.ReadLine();

        Console.Write("Anzahl: ");
        neueBestellung.Anzahl = int.Parse(Console.ReadLine());

        Console.Write("Preis pro Einheit: ");
        neueBestellung.Gesamtbetrag = decimal.Parse(Console.ReadLine()) * neueBestellung.Anzahl;

        try
        {
            _bestellungenViewModel.AddBestellung(neueBestellung);
            Console.WriteLine("Bestellung erfolgreich hinzugefügt!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    private void BestellungenAnzeigen()
    {
        Console.Clear();
        Console.WriteLine("=== Bestellungen anzeigen ===");

        try
        {
            _bestellungenViewModel.LoadBestellungen();
            foreach (var bestellung in _bestellungenViewModel.BestellungsListe)
            {
                Console.WriteLine($"ID: {bestellung.BestellungID}, Produkt: {bestellung.Beschreibung}, Anzahl: {bestellung.Anzahl}, Gesamtpreis: {bestellung.Gesamtbetrag}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    private void BestellungAktualisieren()
    {
        Console.Clear();
        Console.WriteLine("=== Bestellung aktualisieren ===");

        Console.Write("Geben Sie die ID der Bestellung ein: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var bestellung = _bestellungenViewModel.BestellungsListe.FirstOrDefault(b => b.BestellungID == id);
            if (bestellung == null)
            {
                Console.WriteLine("Bestellung nicht gefunden!");
                return;
            }

            Console.WriteLine($"Aktuelle Werte: Produkt: {bestellung.Beschreibung}, Anzahl: {bestellung.Anzahl}, Preis pro Einheit: {bestellung.Gesamtbetrag / bestellung.Anzahl}");

            Console.Write("Neuer Produktname: ");
            bestellung.Beschreibung = Console.ReadLine();

            Console.Write("Neue Anzahl: ");
            bestellung.Anzahl = int.Parse(Console.ReadLine());

            Console.Write("Neuer Preis pro Einheit: ");
            bestellung.Gesamtbetrag = decimal.Parse(Console.ReadLine()) * bestellung.Anzahl;

            try
            {
                _bestellungenViewModel.UpdateBestellung(id, bestellung);
                Console.WriteLine("Bestellung erfolgreich aktualisiert!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ungültige ID!");
        }
    }

    private void BestellungLöschen()
    {
        Console.Clear();
        Console.WriteLine("=== Bestellung löschen ===");

        Console.Write("Geben Sie die ID der Bestellung ein: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                _bestellungenViewModel.DeleteBestellung(id);
                Console.WriteLine("Bestellung erfolgreich gelöscht!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ungültige ID!");
        }
    }

    private void StatistikenAnzeigen()
    {
        // Statistiken anzeigen
        Console.Clear();
        Console.WriteLine("=== Statistiken anzeigen ===");

        try
        {
            var bestellungenProKunde = _statistikenViewModel.BestellungenProKunde();
            Console.WriteLine("Anzahl Bestellungen pro Kunde:");

            foreach (var statistik in bestellungenProKunde)
            {
                Console.WriteLine($"Kunde ID {statistik.KundenID}: {statistik.AnzahlBestellungen} Bestellungen");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }
}
