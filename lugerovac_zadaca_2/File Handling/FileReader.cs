using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public static class FileReader
    {
        public static bool CheckArgs(string[] args)
        {
            if (args.Length < 1)
                return false;
            else
                return true;
        }

        public static bool ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            List<FileData> fileData = new List<FileData>();
            string[] fileLines = File.ReadAllLines(filePath);
            foreach(string line in fileLines)
            {
                string[] split = line.Split('\t');
                FileData newFileDataObject = new FileData();
                newFileDataObject.RecordType = Int32.Parse(split[0]);
                newFileDataObject.ID = Int32.Parse(split[1]);
                newFileDataObject.ParentID = Int32.Parse(split[2]);
                newFileDataObject.Color = split[4];

                string[] coordinatesAsStrings = split[3].Split(',');
                List<int> coordinates = new List<int>();
                foreach (string coordinate in coordinatesAsStrings)
                    coordinates.Add(Int32.Parse(coordinate));
                newFileDataObject.Coordinates = coordinates;

                fileData.Add(newFileDataObject);
            }

            Elements elements = Elements.GetInstance();
            ObjectHandler root = elements.FirstObject;
            foreach(FileData data in fileData)
            {
                ChainRequest request = new ChainRequest(RequestType.AddRecord, data);
                root.HandleRequest(request);
            }

            return true;
        }
    }
}
