using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            if (!MainFacade.HandleArgs(args))
            {
                Console.WriteLine("Zbog nastalih problema, program se prekida!");
                return;
            }

            if(!MainFacade.TransformData())
            {
                Console.WriteLine("Zbog nastalih problema, program se prekida!");
                return;
            }
        }
    }
}
