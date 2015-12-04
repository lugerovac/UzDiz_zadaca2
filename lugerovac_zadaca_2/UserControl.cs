﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public static class UserControl
    {
        static ChainMemory storedMemento;

        public static void ShowUserControls()
        {
            int userInput = GetUserInput();
            switch (userInput)
            {
                case 0:
                    return;
                case 1:
                    ChainRequest request1 = new ChainRequest(RequestType.PrintAllData, null);
                    Elements.GetInstance().FirstObject.HandleRequest(request1);
                    break;
                case 3:
                    ChangeState();
                    break;
                case 5:
                    ReadAnotherFile();
                    Elements elements = Elements.GetInstance();
                    elements.CheckIDs();
                    elements.Validate();
                    break;
                case 6:
                    ChainRequest request6 = new ChainRequest(RequestType.PrintErronousData, null);
                    Elements.GetInstance().FirstObject.HandleRequest(request6);
                    break;
                case 7:
                    storedMemento = new ChainMemory();
                    storedMemento.Memento = Elements.GetInstance().SaveMemento();
                    break;
                case 8:
                    if(Elements.GetInstance().MementoExists)
                        Elements.GetInstance().RestoreMemento(storedMemento.Memento);
                    break;
            }
            ShowUserControls();
        }

        static void ReadAnotherFile()
        {
            Console.Write("Unesite putanju do dodatne datoteke: ");
            string filePath = Console.ReadLine();
            if(!FileReader.ReadFile(filePath))
                Console.WriteLine("Datoteka nije bila pronađena ili učitana!");
            else
                Console.WriteLine("Datoteka je uspješno učitana!!");
        }

        static void ChangeState()
        {
            Console.WriteLine("Kojem objektu želite promjeniti stanje aktivnosti?");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                ChainRequest request = new ChainRequest(RequestType.ChangeState, userInput);
                Elements.GetInstance().FirstObject.HandleRequest(request);
            }
            catch
            {
                Console.WriteLine("Vaš unos je neispravan!");
                return;
            }
        }


        static int GetUserInput()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - pregled stanja");
            //Console.WriteLine("2 - pregled jednostavnih elemenata u presjeku");
            Console.WriteLine("3 - promjena statusa elementa");
            //Console.WriteLine("4 - ukupne površine boja");
            Console.WriteLine("5 - učitavanje dodatne datoteke");
            Console.WriteLine("6 - ispis neispravnih stavki");
            Console.WriteLine("7 - stvori memento podataka");
            if(Elements.GetInstance().MementoExists) Console.WriteLine("8 - iskoristi memento da se obnove spremljeni podaci");
            Console.WriteLine("0 - izlaz iz programa");

            Console.Write("Vaš odabir: ");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                return userInput;
            }
            catch
            {
                Console.WriteLine("Unos je neispravan! Pokušajte ponovno!\n");
                return GetUserInput();
            }
        }
    }
}
