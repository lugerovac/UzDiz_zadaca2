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
        protected bool isHidden = false;
        protected bool isErronous = false;
        protected string errorReport = "";

        public override object HandleRequest(ChainRequest request)
        {
            switch (request.requestType)
            {
                #region Save Data
                case RequestType.SaveData:
                    dataObject = (FileData)request.parameters;
                    return null;
                #endregion

                #region Add Record
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
                #endregion

                #region Print All Data
                case RequestType.PrintAllData:
                    if (!isErronous && !isHidden)
                        PrintMyData();

                    if (successor != null)
                        successor.HandleRequest(request);
                    return null;
                #endregion

                #region Print Erronous Data
                case RequestType.PrintErronousData:
                    if(isErronous)
                    {
                        PrintMyData();
                        Console.WriteLine("Izvještaj o pogrešci: " + errorReport);
                    }

                    if (this.successor != null)
                        successor.HandleRequest(request);
                    return null;
                #endregion

                #region Add ID to the list and check it
                case RequestType.AddIdToListAndCheck:
                    List<int> IdList = (List<int>)request.parameters;
                    if(IdList.Contains(dataObject.ID))
                    {
                        isErronous = true;
                        errorReport = "Već postoji element s ovom šifrom";
                    }else
                    {
                        IdList.Add(dataObject.ID);
                    }

                    if (successor != null)
                        successor.HandleRequest(request);
                    return null;
                #endregion

                #region Validation Check
                case RequestType.ValidationCheck:
                    if(dataObject.RecordType == 0 && dataObject.Coordinates.Count != 4 && !isErronous)
                    {
                        isErronous = true;
                        errorReport = "Složeni element nije pravokutnik";
                    }

                    if(dataObject.RecordType == 0 && dataObject.ID == dataObject.ParentID && !isErronous)
                    {
                        if(dataObject.ID != Elements.GetInstance().RootElement)
                        {
                            isErronous = true;
                            errorReport = "Već postoji ishodišni element";
                        }
                    }

                    if (dataObject.RecordType == 1 && !isErronous)
                    {
                        if(dataObject.ID == dataObject.ParentID && dataObject.RecordType == 1 && !isErronous)
                        {
                            isErronous = true;
                            errorReport = "Jednostavni element ne može biti sam sebi roditelj!";
                        }

                        if (dataObject.ID != dataObject.ParentID && !isErronous)
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
                                errorReport = "Roditelj nije složeni element ili roditelj ne postoji";
                            }
                        }

                        if(dataObject.Coordinates.Count < 3 || dataObject.Coordinates.Count > 14
                            || (dataObject.Coordinates.Count % 2 != 0 && dataObject.Coordinates.Count != 3))
                        {
                            isErronous = true;
                            errorReport = "Broj koordinata je manji od 3 ILI veći od 14 ILI je neparan a nije jednak 3";
                        }
                    }

                    if (this.successor != null)
                        successor.HandleRequest(request);

                    return null;
                #endregion

                #region Check if Parent exists
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
                #endregion

                #region Find the root element
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
                #endregion

                #region Change State
                case RequestType.ChangeState:
                    if ((int)request.parameters == dataObject.ID && !isErronous)
                        isHidden = !isHidden;
                    else if (this.successor != null)
                        successor.HandleRequest(request);
                    return null;
                #endregion

                #region Add To Memento
                case RequestType.AddToMemento:
                    FileData mementoDataObject = new FileData();
                    mementoDataObject.ID = dataObject.ID;
                    mementoDataObject.RecordType = dataObject.RecordType;
                    mementoDataObject.ParentID = dataObject.ParentID;
                    mementoDataObject.Coordinates = dataObject.Coordinates;
                    mementoDataObject.Color = dataObject.Color;

                    ObjectHandler mementoObject = (ObjectHandler)request.parameters;
                    mementoObject.dataObject = mementoDataObject;
                    mementoObject.isErronous = isErronous;
                    mementoObject.isHidden = isHidden;
                    mementoObject.isRootElement = isRootElement;
                    mementoObject.errorReport = errorReport;

                    if(this.successor != null)
                    {
                        ObjectHandler mementoSuccessor = new ObjectHandler();
                        mementoObject.successor = mementoSuccessor;
                        ChainRequest newRequest = new ChainRequest(RequestType.AddToMemento, mementoSuccessor);
                        this.successor.HandleRequest(newRequest);
                    }
                    return null;
                #endregion
            }
            return null;
        }

        void PrintMyData()
        {
            string coordinatesAsString = "";
            bool first = true;
            foreach (int coord in dataObject.Coordinates)
            {
                if (!first)
                    coordinatesAsString += ", ";
                coordinatesAsString += coord.ToString();
                first = false;
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine("ID: " + dataObject.ID);
            Console.Write("Tip zapisa: ");
            Console.WriteLine(dataObject.RecordType == 0 ? "Složeni objekt" : "Jednostavni objekt");
            Console.WriteLine("Roditelj: " + dataObject.ParentID);
            Console.WriteLine("Koordinate: " + coordinatesAsString);
            Console.WriteLine("Boja: " + dataObject.Color);
        }

    }
}
