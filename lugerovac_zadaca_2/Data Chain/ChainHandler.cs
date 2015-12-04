using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    /// <summary>
    /// Sučelje za uzorak Chain of Responsibility
    /// </summary>
    public abstract class ChainHandler
    {
        protected ChainHandler successor;

        public void SetSuccessor(ChainHandler successor)
        {
            if (this.successor != null)
                this.successor.SetSuccessor(successor);
        }
        public abstract object HandleRequest(ChainRequest request);
    }
}
