using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.Model
{
    public class AddEquipInfo
    {
        public string barCode;
        public string factoryCode;
        public string productLineNo;
        public string equipNo;
        public string stationCodeNo;
        public string @operator;
        public string productCode;
        public string mainPartCode;
        public string childPartCode;
        public string childPartUUID;
        public DateTime startTime;
        public DateTime endTime;
        public string result;
        public int status = 1;
        public bool isByPass = false;
        public bool isCheckRepeat = true;
        public bool isReject = true;
        public List<parms> detailTable;

        public string BarCode { get => barCode; set => barCode = value; }
        public string FactoryCode { get => factoryCode; set => factoryCode = value; }
        public string ProductLineNo { get => productLineNo; set => productLineNo = value; }
        public string EquipNo { get => equipNo; set => equipNo = value; }
        public string StationCodeNo { get => stationCodeNo; set => stationCodeNo = value; }
        public string Operator { get => @operator; set => @operator = value; }
        public string ProductCode { get => productCode; set => productCode = value; }
        public string MainPartCode { get => mainPartCode; set => mainPartCode = value; }
        public string ChildPartCode { get => childPartCode; set => childPartCode = value; }
        public string ChildPartUUID { get => childPartUUID; set => childPartUUID = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public DateTime EndTime { get => endTime; set => endTime = value; }
        public string Result { get => result; set => result = value; }
        public int Status { get => status; set => status = value; }
        public bool IsByPass { get => isByPass; set => isByPass = value; }
        public bool IsCheckRepeat { get => isCheckRepeat; set => isCheckRepeat = value; }
        public bool IsReject { get => isReject; set => isReject = value; }
        public List<parms> DetailTable { get => detailTable; set => detailTable = value; }

        public AddEquipInfo()
        {
        }

        public AddEquipInfo(string barCode, string factoryCode, string productLineNo, string equipNo, string stationCodeNo, string @operator, string productCode, string mainPartCode, string childPartCode, string childPartUUID, DateTime startTime, DateTime endTime, string result, int status, bool isByPass, bool isCheckRepeat, bool isReject, List<parms> detailTable)
        {
            BarCode = barCode;
            FactoryCode = factoryCode;
            ProductLineNo = productLineNo;
            EquipNo = equipNo;
            StationCodeNo = stationCodeNo;
            Operator = @operator;
            ProductCode = productCode;
            MainPartCode = mainPartCode;
            ChildPartCode = childPartCode;
            ChildPartUUID = childPartUUID;
            StartTime = startTime;
            EndTime = endTime;
            Result = result;
            Status = status;
            IsByPass = isByPass;
            IsCheckRepeat = isCheckRepeat;
            IsReject = isReject;
            DetailTable = detailTable;
        }
    }
}
