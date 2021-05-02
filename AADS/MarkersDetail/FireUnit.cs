using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    class FireUnit
    {

        private String FireUnitBatteryID { get; set; }
        private String FireUnitNumber { get; set; }
        public String FireUnitType { get; set; }
        public String FireUnitDetail { get; set; }
        public String FireUnitStatus { get; set; }
        public PointLatLng FireUnitPoint { get; set; }
        public String FireUnitID { get; set; }

        public FireUnit(string FireUnitID, string FireUnitBatteryID, string FireUnitNumber, string FireUnitType, string FireUnitDetail, string FireUnitStatus, PointLatLng FireUnitPoint)
        {
            this.FireUnitID = FireUnitID;
            this.FireUnitBatteryID = FireUnitBatteryID;
            this.FireUnitNumber = FireUnitNumber;
            this.FireUnitType = FireUnitType;
            this.FireUnitDetail = FireUnitDetail;
            this.FireUnitStatus = FireUnitStatus;
            this.FireUnitPoint = FireUnitPoint;

        }

        public String GetFireUnitBatteryID()
        {
            return FireUnitBatteryID;
        }
        public String GetFireUnitNumber()
        {
            return FireUnitNumber;
        }
        public String GetFireUnitType()
        {
            return FireUnitType;
        }
        public String GetFireUnitDetail()
        {
            return FireUnitDetail;
        }
        public PointLatLng GetFireUnitPoint()
        {
            return FireUnitPoint;
        }
        public String GetFireUnitStatus()
        {
            return FireUnitStatus;
        }
    }
}
