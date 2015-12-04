using System;
using System.Text;

namespace lugerovac_zadaca_2
{
    class Program
    {
        /// <summary>
        /// Početna funkcija
        /// </summary>
        /// <param name="args">Argumenti zadani u cmd-u</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            if (!MainFacade.HandleArgs(args))
            {
                Console.WriteLine("Zbog nastalih problema, program se prekida!");
                return;
            }

            if(!MainFacade.HandleData())
            {
                Console.WriteLine("Zbog nastalih problema, program se prekida!");
                return;
            }

            MainFacade.UserControler();
        }
    }
}
