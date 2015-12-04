using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public enum RequestType
    {
        SaveData,
        AddRecord,
        PrintAllData,
        FindRoot
    }

    public class ChainRequest
    {
        public RequestType requestType;
        public object parameters;

        public ChainRequest(RequestType requestType, object parameters)
        {
            this.requestType = requestType;
            this.parameters = parameters;
        }
    }
}
