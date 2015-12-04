using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public static class MainFacade
    {
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

        public static bool HandleData()
        {
            Elements elements = Elements.GetInstance();
            elements.CheckIDs();
            elements.Validate();
            elements.FindRootElement();
            if (!elements.FoundRootElement)
            {
                Console.WriteLine("Ne postoji izvorišni element!");
                return false;
            }
            return true;
        }
    }
}
