using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


/*
Si vuole progettare un sistema per la gestione di una biblioteca.
Gli utenti registrati al sistema, fornendo cognome, nome, email,
password, recapito telefonico, possono effettuare dei prestiti sui documenti
che sono di vario tipo (libri, DVD).

I documenti sono caratterizzati da un codice identificativo di tipo stringa
(ISBN per i libri, numero seriale per i DVD), titolo, anno, settore
(storia, matematica, economia, …), stato(In Prestito, Disponibile),
uno scaffale in cui è posizionato, un elenco di autori (Nome, Cognome).

Per i libri si ha in aggiunta il numero di pagine, mentre per i dvd la durata.
L’utente deve poter eseguire delle ricerche per codice o per titolo e,
eventualmente, effettuare dei prestiti registrando il periodo (Dal/Al)
del prestito e il documento.

Il sistema per ogni prestito determina un numero progressivo di tipo
alfanumerico.
Deve essere possibile effettuare la ricerca dei prestiti dato nome e cognome
di un utente.
*/

namespace csharp_biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string path = @"c:\temp\MyTest.txt";
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("This");
                    sw.WriteLine("is some text");
                    sw.WriteLine("to test");
                    sw.WriteLine("Reading");
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            */

            string sFileLav;
            string PublicEnv = Environment.GetEnvironmentVariable("PUBLIC");
            if (PublicEnv != null)
                Console.WriteLine("Valore: {0}", PublicEnv);

            if (Directory.Exists(PublicEnv + "/biblioteca"))
            {
                Console.WriteLine("La directory è già presente in: {0}.", PublicEnv);

                if (File.Exists(PublicEnv + "/biblioteca" + "/biblioteca.txt"))
                {
                    Console.WriteLine("Il file .txt esiste già.");
                    File.ReadAllLines(PublicEnv + "/biblioteca" + "/biblioteca.txt");
                    // DEVO LEGGERE BIBLIOTECA.TXT CHE ESISTE, E CAPIRE IL FILE SU CUI DEVO LAVORARE.
                    // sFileLav è la seconda riga di biblioteca.txt
                }
                else
                {
                    Console.WriteLine("Scrivi dove si trova il file oppure se non sai dov'è premi INVIO.");
                    string sFilePos = Console.ReadLine();
                    if (sFilePos == "")
                    {
                        StreamWriter sw = new StreamWriter(PublicEnv + "/biblioteca" + "/biblioteca.txt"); // sFileLav è biblioteca.txt
                        sw.WriteLine("Section: fileConfig;");
                        sw.WriteLine(PublicEnv + "/biblioteca" + "/biblioteca.txt");
                        sw.Close();
                        Console.WriteLine("Il file .txt è stato creato.");
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(PublicEnv + "/biblioteca" + "/biblioteca.txt"); // sFileLav è quello che mi ha detto l'utente (sFilePos).
                        sw.WriteLine("Section: fileConfig;");
                        sw.WriteLine(sFilePos);
                        sw.Close();
                        Console.WriteLine("Il file .txt è stato creato.");
                    }
                }
            } 
            else
            {
                Directory.CreateDirectory(PublicEnv + "/biblioteca");
                Console.WriteLine("La directory non esisteva, ora è stata creata.");
                Console.WriteLine("Scrivi dove si trova il file oppure se non sai dov'è premi INVIO.");
                string sFilePos = Console.ReadLine();
                if (sFilePos == "")
                {
                    StreamWriter sw = new StreamWriter(PublicEnv + "/biblioteca" + "/biblioteca.txt"); // sFileLav è biblioteca.txt
                    sw.WriteLine("Section: fileConfig;");
                    sw.WriteLine(PublicEnv + "/biblioteca" + "/biblioteca.txt");
                    sw.Close();
                    Console.WriteLine("Il file .txt è stato creato.");
                }
                else
                {
                    StreamWriter sw = new StreamWriter(PublicEnv + "/biblioteca" + "/biblioteca.txt"); // sFileLav è quello che mi ha detto l'utente (sFilePos)
                    sw.WriteLine("Section: fileConfig;");
                    sw.WriteLine(sFilePos);
                    sw.Close();
                    Console.WriteLine("Il file .txt è stato creato.");
                }
            }

            // qual'è il file sul quale devo lavorare? sFileLav


            Biblioteca bib = new Biblioteca("Biblioteca Comunale");

            Scaffale scaf1 = new Scaffale("SCAF-001");
            Scaffale scaf2 = new Scaffale("SCAF-002");
            Scaffale scaf3 = new Scaffale("SCAF-003");

            bib.RestoreUtenti("UserData.txt");

            Libro book1 = new Libro("ISBN1", "Titolo libro", 2016, "Fantasy", 352);
            Autore auth1 = new Autore("Mario", "Rossi");
            book1.Autori.Add(auth1);
            book1.Scaffale = scaf1;
            bib.Documenti.Add(book1);

            Libro book2 = new Libro("ISBN2", "Titolo libro più bello", 1995, "Storico", 621);
            Autore auth2 = new Autore("Luigi", "Verdi");
            Autore auth3 = new Autore("Giacomo", "Leone");
            book1.Autori.Add(auth2);
            book1.Autori.Add(auth3);
            book1.Scaffale = scaf2;
            bib.Documenti.Add(book2);

            Dvd dvd1 = new Dvd("Seriale1", "Titolo film", 2004, "Thriller", 128);
            Autore auth4 = new Autore("Giovanni", "Bolli");
            dvd1.Autori.Add(auth4);
            dvd1.Scaffale = scaf3;
            bib.Documenti.Add(dvd1);

            Utente user1 = new Utente("Marco", "Pigna", "telefono1", "email1", "password1");

            bib.Utenti.Add(user1);


            Prestito p1 = new Prestito("Prestito N°: P00000001", new DateTime(2020, 5, 18), new DateTime(2020, 6, 05), user1, book2);
            Prestito p2 = new Prestito("Prestito N°: P00000002", new DateTime(2021, 11, 02), new DateTime(2021, 11, 03), user1, dvd1);

            bib.Prestiti.Add(p1);
            bib.Prestiti.Add(p2);


            Console.WriteLine("\n\nCerca codice libro: ISBN1\n\n");

            List<Documento> results = bib.CercaCodice("ISBN2");

            foreach (Documento doc in results)
            {
                Console.WriteLine(doc.ToString());

                if (doc.Autori.Count > 0)
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Autori");
                    Console.WriteLine("--------------------------");
                    
                    foreach (Autore a in doc.Autori)
                    {
                        Console.WriteLine(a.ToString());
                        Console.WriteLine("--------------------------");
                    }
                }
            }

            Console.WriteLine("\n\nCerca prestiti di: Marco, Pigna \n\n");

            List<Prestito> prestiti = bib.CercaPrestiti("Marco", "Pigna");

            foreach (Prestito p in prestiti)
            {
                Console.WriteLine(p.ToString());
                Console.WriteLine("--------------------------");
            }

            bib.SaveUtenti("UserData.txt");
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "";
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}