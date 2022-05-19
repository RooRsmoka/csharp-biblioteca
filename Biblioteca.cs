using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csharp_biblioteca
{
    internal class Biblioteca
    {
        public string Nome { get; set; }
        public List<Documento> Documenti { get; set; }
        public List<Utente> Utenti { get; set; }
        public List<Prestito> Prestiti { get; set; }
        public ListaUtenti MiaListaUtenti { get; set; }

        public Biblioteca(string Nome)
        {
            this.Nome = Nome;
            this.Documenti = new List<Documento>();
            this.Prestiti = new List<Prestito>();
            this.Utenti = new List<Utente>();
        }

        public List<Documento> CercaCodice(string Codice)
        {
            return this.Documenti.Where(d => d.Codice == Codice).ToList();
        }

        public List<Documento> CercaTitolo(string Titolo)
        {
            return this.Documenti.Where(d => d.Titolo == Titolo).ToList();
        }

        public List<Prestito> CercaPrestiti(string Numero)
        {
            return this.Prestiti.Where(p => p.Numero == Numero).ToList();
        }

        public List<Prestito> CercaPrestiti(string Nome, string Cognome)
        {
            return this.Prestiti.Where(p => p.Utente.Nome == Nome &&
            p.Utente.Cognome == Cognome).ToList(); 
        }

        public bool SaveUtenti(string filename)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                foreach (Utente user in Utenti)
                {
                    string crea_stringa = user.Nome + ";" + user.Cognome + ";" + user.Email;
                    sw.WriteLine(crea_stringa);
                }
                sw.Close();  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return true;
        }

        public bool RestoreUtenti(string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(filename);
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] sVect = line.Split(";");
                    string Nome = sVect[0];
                    string Cognome = sVect[1];
                    string Telefono = sVect[2];
                    string Email = sVect[3];
                    string Password = sVect[4];

                    Utente utente = new Utente(Nome, Cognome, Telefono, Email, Password);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return true;
        }
    }
}
