using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_2
{
    public class FileData
    {
        int _recordType;
        public int RecordType
        {
            get { return _recordType; }
            set { _recordType = value; }
        }

        int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        int _parentId;
        public int ParentID
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public List<int> Coordinates;

        string _color;
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
    }
}
