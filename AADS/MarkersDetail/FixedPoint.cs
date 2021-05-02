using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    class FixedPoint
    {
        private static MainForm mainInstance = MainForm.GetInstance();

        private String FixedPointNumber { get; set; }
        private String FixedPointType { get; set; }
        private String FixedPointLabel { get; set; }
        private int FixedPointLiveSIM { get; set; }
        private String FixedPointSource { get; set; }
        private String FixedPointID { get; set; }
        private PointLatLng FixedPoint_Point { get; set; }

        public FixedPoint(string FixedPointID, string FixedPointNumber, string FixedPointType, string FixedPointLabel, int FixedPointLiveSIM, string FixedPointSource, PointLatLng FixedPoint_Point)
        {
            this.FixedPointID = FixedPointID;
            this.FixedPointNumber = FixedPointNumber;
            this.FixedPointType = FixedPointType;
            this.FixedPointLabel = FixedPointLabel;
            this.FixedPointLiveSIM = FixedPointLiveSIM;
            this.FixedPointSource = FixedPointSource;
            this.FixedPoint_Point = FixedPoint_Point;

        }
        public String GetFixedPointID()
        {
            return FixedPointID;
        }
        public String GetFixedPointNumber()
        {
            return FixedPointNumber;
        }
        public String GetFixedPointType()
        {
            return FixedPointType;
        }
        public String GetFixedPointLabel()
        {
            return FixedPointLabel;
        }
        public int GetFixedPointLiveSIM()
        {
            return FixedPointLiveSIM;
        }
        public PointLatLng GetFixedPoint_Point()
        {
            return FixedPoint_Point;
        }
        public String GetFixedPointSource()
        {
            return FixedPointSource;
        }
    }
}
