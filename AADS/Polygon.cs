using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    class Polygon
    {
        private static MainForm mainForm1 = MainForm.GetInstance();
        private GMapOverlay overlay = mainForm1.GetOverlay("polygons");
        private GMapPolygon polygon;
        public Dictionary<GMapPolygon, List<string>> dictPolygons = new Dictionary<GMapPolygon, List<string>>();
        public List<string> polygonData;
        public Polygon() { }
        public void CreatePolygon(List<PointLatLng> points)
        {
            polygon = new GMapPolygon(points, "Polygons");
            overlay.Polygons.Add(polygon);

            List<string> data = AddData();

            dictPolygons.Add(polygon, data);
        }
        public List<string> AddData()
        {
            polygonData = new List<string>();
            return polygonData;
        }
    }
}
