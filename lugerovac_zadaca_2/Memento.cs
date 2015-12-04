using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public class Memento
    {
        public ObjectHandler firstObject;
        public bool foundRootElement;
        public int rootElement;

        public Memento(ObjectHandler firstObject, bool foundRootElement, int rootElement)
        {
            this.firstObject = firstObject;
            this.foundRootElement = foundRootElement;
            this.rootElement = rootElement;
        }
    }
}
