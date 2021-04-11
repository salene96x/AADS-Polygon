using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS
{
    class Polygon
    {
        private static MainForm mainForm1 = MainForm.GetInstance();
        private GMapOverlay overlay = mainForm1.GetOverlay("polygons");
        private static GMapPolygon polygon;
        public static Dictionary<GMapPolygon, List<string>> dictPolygons = new Dictionary<GMapPolygon, List<string>>();
        public static List<string> polygonData;
        public Polygon() { }
        public void CreatePolygon(List<PointLatLng> points)
        {
            var geoInstance = Views.Filter_Geographic.main.GetInstance();
            polygon = new GMapPolygon(points, "Polygons");
            overlay.Polygons.Add(polygon);
            WritePointsToList(points);
            AddData(geoInstance.textBox1.Text, points);
        }
        public void AddData(string name, List<PointLatLng> points)
        {
            polygonData = new List<string>();
            polygonData.Add(name);
            foreach (var j in points)
            {
                polygonData.Add(j.ToString());
            }
        }
        public static GMapPolygon GetPolygon()
        {
            return polygon;
        }
        void WritePointsToList(List<PointLatLng> points)
        {
            var geoInstance = Views.Filter_Geographic.main.GetInstance();
            if (geoInstance != null)
            {
                geoInstance.SetListBoxValue(points);
            }
            

        }
    }
}
