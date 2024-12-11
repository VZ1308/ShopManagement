using System;

public class Bestellung
{
    public int BestellungID { get; set; }
    public int BenutzerID { get; set; }
    public string Beschreibung { get; set; }
    public int Anzahl { get; set; }
    public DateTime Bestelldatum { get; set; }
    public decimal Gesamtbetrag { get; set; }
}
