using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca
{
    internal class Prestito
    {
        public string Numero { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public Utente Utente { get; set; }
        public Documento Documento { get; set; }
        
        public Prestito(string Numero, DateTime DataInizio, DateTime DataFine, Utente Utente, Documento Documento)
        {
            this.Numero = Numero;
            this.DataInizio = DataInizio;
            this.DataFine = DataFine;
            this.Utente = Utente;
            this.Documento = Documento;
            this.Documento.Stato = Stato.InPrestito;
        }

        public override string ToString()
        {
            return string.Format("Numero: {0}\nDal: {1}\nAl: {2}\nStato: {3}\nUtente: {4}\nDocumento: {5}", 
                this.Numero, this.DataInizio, this.DataFine, this.Documento.Stato, this.Utente.ToString(), this.Documento.ToString());
        }
    }
}
