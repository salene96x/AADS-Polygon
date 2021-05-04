using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS.ObjectsManager
{
    class PolygonDataCollection
    {
        public string name { get; set; }
        public List<PointLatLng> _point { get; set; }
        public string statusEx { get; set; }
        public string statusIn { get; set; }
        public GMapPolygon polygon { get; set; }
        public PolygonDataCollection(string name, List<PointLatLng> _point, string statusEx, string statusIn, GMapPolygon polygon)
        {
            this.name = name;
            this._point = _point;
            this.statusEx = statusEx;
            this.statusIn = statusIn;
            this.polygon = polygon;
        }

       
    }
}
