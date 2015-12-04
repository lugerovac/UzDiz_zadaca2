﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
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

        public bool CheckIDs()
        {
            List<int> IdList = new List<int>();
            ChainRequest request = new ChainRequest(RequestType.AddIdToListAndCheck, IdList);
            _firstObject.HandleRequest(request);
            return true;
        }

        public bool Validate()
        {
            ChainRequest request = new ChainRequest(RequestType.ValidationCheck, null);
            _firstObject.HandleRequest(request);
            return true;
        }

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

        public Memento SaveMemento()
        {
            ObjectHandler newFirstObject = new ObjectHandler();
            ChainRequest request = new ChainRequest(RequestType.AddToMemento, newFirstObject);
            FirstObject.HandleRequest(request);
            _mementoExists = true;
            return new Memento(newFirstObject, FoundRootElement, RootElement);
        }

        public void RestoreMemento(Memento memento)
        {
            _firstObject = memento.firstObject;
            _foundRootElement = memento.foundRootElement;
            _rootElement = memento.rootElement;
            _mementoExists = false;

            Console.WriteLine("\nMemento je iskorišten!");
        }
    }
}
