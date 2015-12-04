using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public class ObjectHandler : ChainHandler
    {
        protected FileData dataObject;
        protected bool isRootElement = false;
        protected bool isErronous = false;
        protected string errorReport = "";

        public override object HandleRequest(ChainRequest request)
        {
            switch (request.requestType)
            {
                case RequestType.SaveData:
                    dataObject = (FileData)request.parameters;
                    return null;

                case RequestType.AddRecord:
                    if(dataObject == null)
                    {
                        ChainRequest newRequest = new ChainRequest(RequestType.SaveData, request.parameters);
                        this.HandleRequest(newRequest);
                    }
                    else if (successor != null)
                        successor.HandleRequest(request);
                    else
                    {
                        ObjectHandler newRecord = new ObjectHandler();
                        successor = newRecord;
                        ChainRequest newRequest = new ChainRequest(RequestType.SaveData, request.parameters);
                        this.successor.HandleRequest(newRequest);
                    }
                    return null;

                case RequestType.PrintAllData:
                    string coordinatesAsString = "";
                    bool first = true;
                    foreach (int coord in dataObject.Coordinates)
                    {
                        if (!first)
                            coordinatesAsString += ", ";
                        coordinatesAsString += coord.ToString();
                        first = false;
                    }

                    Console.WriteLine(dataObject.RecordType.ToString() + "\t"
                        + dataObject.ID.ToString() + "\t"
                        + dataObject.ParentID.ToString() + "\t"
                        + coordinatesAsString + "\t"
                        + dataObject.Color);

                    if (successor != null)
                        successor.HandleRequest(request);
                    return null;

                case RequestType.AddIdToListAndCheck:
                    List<int> IdList = (List<int>)request.parameters;
                    if(IdList.Contains(dataObject.ID))
                    {
                        isErronous = true;
                        errorReport += "Već postoji element s ovom šifrom\n";
                    }else
                    {
                        IdList.Add(dataObject.ID);
                    }

                    if (successor != null)
                        successor.HandleRequest(request);
                    return null;

                case RequestType.ValidationCheck:
                    if(dataObject.RecordType == 0 && dataObject.Coordinates.Count != 4 && !isErronous)
                    {
                        isErronous = true;
                        errorReport += "Složeni element nije pravokutnik\n";
                        return null;
                    }

                    if (dataObject.RecordType == 1 && !isErronous)
                    {
                        ChainRequest newReqeust = new ChainRequest(RequestType.AreYouMydaddy, dataObject.ParentID);
                        if ((bool)Elements.GetInstance().FirstObject.HandleRequest(newReqeust))  //I go look for daddy :)
                        {
                            //I found my daddy :D
                        }
                        else
                        {
                            //No, he is not my real daddy or I don't actually have a daddy :(
                            isErronous = true;  //I am illegitimate :'(
                            errorReport += "Roditelj nije složeni element ili roditelj ne postoji\n";
                            return null;
                        }

                        if(dataObject.Coordinates.Count < 3 || dataObject.Coordinates.Count > 14
                            || (dataObject.Coordinates.Count % 2 != 0))
                        {
                            isErronous = true;
                            errorReport += "Broj koordinata je manji od 3 ILI veći od 14 ILI je neparan a nije jednak 3\n";
                            return null;
                        }
                    }
                    return null;

                case RequestType.AreYouMydaddy:
                    if ((int)request.parameters == dataObject.ID)
                    {
                        if (dataObject.RecordType == 0)
                            return true;
                        else
                            return false;
                    }
                    else if (this.successor != null)
                        return successor.HandleRequest(request);
                    else
                        return false;

                case RequestType.FindRoot:
                    if (dataObject.RecordType == 0 && !isErronous)
                    {
                        if (dataObject == null)
                            return null;
                        else if (isRootElement)
                            return dataObject.ID;
                        else if (dataObject.ID == dataObject.ParentID)
                        {
                            isRootElement = true;
                            return dataObject.ID;
                        }
                    }

                    if (this.successor != null)
                        return successor.HandleRequest(request);

                    return null;
            }
            return null;
        }
    }
}
