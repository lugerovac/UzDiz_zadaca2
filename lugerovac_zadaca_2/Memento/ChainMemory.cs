using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public class ChainMemory
    {
        private Memento _memento;
        public Memento Memento
        {
            get { return _memento; }
            set { _memento = value; }
        }
    }
}
