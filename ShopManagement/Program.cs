using Microsoft.Extensions.Configuration;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Konfiguration laden
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Projektverzeichnis
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // DatabaseHelper initialisieren mit Konfiguration
        var dbHelper = new DatabaseHelper(configuration);

        // Hauptmenü starten
        var hauptMenü = new HauptMenü(dbHelper);
        hauptMenü.Start();
    }
}
