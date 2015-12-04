using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    /// <summary>
    /// Singleton klasa koja sprema strukturu elemenata
    /// </summary>
    public class Elements
    {
        private static Elements _instance;

        private bool _foundRootElement = false;
        public bool FoundRootElement
        {
            get { return _foundRootElement; }
        }

        private int _rootElement;
        public int RootElement
        {
            get { return _rootElement; }
        }

        private ObjectHandler _firstObject = new ObjectHandler();
        public ObjectHandler FirstObject
        {
            get { return _firstObject; }
        }

        private bool _mementoExists = false;
        public bool MementoExists
        {
            get { return _mementoExists; }
        }

        protected Elements()
        {
        }

        public static Elements GetInstance()
        {
            if (_instance == null)
                _instance = new Elements();

            return _instance;
        }

        /// <summary>
        /// Šalje zahtjev elementima lanca da provjere jedinstvenost svojih šifara
        /// </summary>
        public void CheckIDs()
        {
            List<int> IdList = new List<int>();
            ChainRequest request = new ChainRequest(RequestType.AddIdToListAndCheck, IdList);
            _firstObject.HandleRequest(request);
        }

        /// <summary>
        /// Šalje zahtjev elementima lanca da provjere svoje ispravnosti
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            ChainRequest request = new ChainRequest(RequestType.ValidationCheck, null);
            _firstObject.HandleRequest(request);
            return true;
        }

        /// <summary>
        /// Funkcija šalje zahtjev lancu da joj pronađe ispravni, izvorišni element
        /// </summary>
        /// <returns>True ako takav element postoji, inače False</returns>
        public bool FindRootElement()
        {
            if (_firstObject == null)
                return false;
            ChainRequest request = new ChainRequest(RequestType.FindRoot, null);
            int? rootID = (int?) _firstObject.HandleRequest(request);  //int? označava vrijednost koja je ili int ili null
            if (rootID == null)
                return false;
            else
            {
                _foundRootElement = true;
                _rootElement = rootID.GetValueOrDefault();
                Console.WriteLine(rootID.GetValueOrDefault().ToString());
                return true;
            }
        }

        /// <summary>
        /// Sprema trenutnu strukturu u memento
        /// </summary>
        /// <returns>Trenutna struktura spremljena u objektu klase Memento</returns>
        public Memento SaveMemento()
        {
            ObjectHandler newFirstObject = new ObjectHandler();
            ChainRequest request = new ChainRequest(RequestType.AddToMemento, newFirstObject);
            FirstObject.HandleRequest(request);
            _mementoExists = true;
            return new Memento(newFirstObject, FoundRootElement, RootElement);
        }

        /// <summary>
        /// Koristi Memento da zamjeni trenutnu strukturu starijom
        /// </summary>
        /// <param name="memento">Objekt koji sadrži sve podatke o taroj strukturi</param>
        public void RestoreMemento(Memento memento)
        {
            _firstObject = memento.firstObject;
            _foundRootElement = memento.foundRootElement;
            _rootElement = memento.rootElement;
            _mementoExists = false;
        }
    }
}
