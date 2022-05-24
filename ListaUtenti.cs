using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca
{
    internal class ListaUtenti
    {
        private Dictionary<Tuple<string, string, string>, Utente> MyDictionary;

        public ListaUtenti()
        {
            MyDictionary = new Dictionary<Tuple<string, string, string>, Utente>();
        }

        public void AggiungiUtente(Utente Utente)
        {
            var ChiaveUtente = new Tuple<string, string, string>(Utente.Nome, Utente.Cognome, Utente.Email);
            MyDictionary.Add(ChiaveUtente, Utente);
            
            /*
            string ChiaveUtente = 
                Utente.Nome.ToLower() + ";" + Utente.Cognome.ToLower() + ";" + Utente.Email.ToLower();
            MyDictionary.Add(ChiaveUtente, Utente);
            */
        }
    }
}