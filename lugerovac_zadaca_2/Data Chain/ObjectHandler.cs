﻿using System;
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

                case RequestType.FindRoot:
                    if (dataObject == null)
                        return null;
                    else if (isRootElement)
                        return dataObject.ID;
                    else if (dataObject.ID == dataObject.ParentID)
                    {
                        isRootElement = true;
                        return dataObject.ID;
                    }
                    else if (this.successor != null)
                        return successor.HandleRequest(request);

                    return null;
            }
            return null;
        }
    }
}