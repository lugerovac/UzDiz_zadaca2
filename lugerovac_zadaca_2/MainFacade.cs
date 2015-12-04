using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    /// <summary>
    /// Statična klasa kojom se realizira uzorak Facade
    /// </summary>
    public static class MainFacade
    {
        /// <summary>
        /// Funkcija koja iskorištava argumente zadane u cmd-u
        /// </summary>
        /// <param name="args">Argumenti koej je zadao korisnik</param>
        /// <returns>Vraće True ako je sve bilo uredno učitano, inače False</returns>
        public static bool HandleArgs(string[] args)
        {
            if (!FileReader.CheckArgs(args))
            {
                Console.WriteLine("Nedovoljan broj argumenata!");
                return false;
            }

            if (!FileReader.ReadFile(args[0]))
            {
                Console.WriteLine("Datoteka nije bila pronađena ili učitana!");
                return false;
            }

            /*ChainRequest printRequest = new ChainRequest(RequestType.PrintAllData, null);
            Elements.GetInstance().RootObject.HandleRequest(printRequest);*/
            return true;
        }

        /// <summary>
        /// Iskorištava podatke učitane iz datoteke
        /// </summary>
        /// <returns>Vraće True ako nije došlo do pogreške, inače False</returns>
        public static bool HandleData()
        {
            Elements elements = Elements.GetInstance();
            elements.CheckIDs();
            elements.FindRootElement();
            elements.Validate();
            if (!elements.FoundRootElement)
            {
                Console.WriteLine("Ne postoji izvorišni element!");
                return false;
            }
            PrintErronousData();
            return true;
        }

        /// <summary>
        /// Šalje zahtjev za ispis neispravnih podataka
        /// </summary>
        static void PrintErronousData()
        {
            ChainRequest request = new ChainRequest(RequestType.PrintErronousData, null);
            Elements.GetInstance().FirstObject.HandleRequest(request);
        }

        /// <summary>
        /// Prikazuje korisničke kontrole korisniku
        /// </summary>
        public static void UserControler()
        {
            UserControl.ShowUserControls();
        }
    }
}
