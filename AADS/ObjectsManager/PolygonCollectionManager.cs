using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace AADS.ObjectsManager
{
    class PolygonCollectionManager
    {
        public PolygonCollectionManager() { }
        public static Dictionary<string, PolygonDataCollection> _polygonDict { get; set; } = new Dictionary<string, PolygonDataCollection>();
        private MainForm main = MainForm.GetInstance();
        private static int number = 001;

        public void Add(string id, PolygonDataCollection data)
        {
            if (!(_polygonDict.ContainsKey(id)))
            {
                _polygonDict.Add(id, data);
                Debug.WriteLine(id + " " + "Polygon data has been added");
            }
        }
        public string FindId(GMapPolygon polygon)
        {
            string id = "";
            foreach (var j in _polygonDict.Keys)
            {
                if (_polygonDict[j].polygon == polygon)
                {
                    id = j;
                }
            }
            return id;
        }
        public void Remove(GMapPolygon polygon)
        {
            string id = this.FindId(polygon);
            if (_polygonDict.ContainsKey(id))
            {
                _polygonDict.Remove(id);
            }
        }
        public void Update(string id, string name, string statusEx, string statusIn, List<PointLatLng> points)
        {
            if (_polygonDict != null)
            {
                _polygonDict[id].name = name;
                _polygonDict[id].statusEx = statusEx;
                _polygonDict[id].statusIn = statusIn;
                _polygonDict[id]._point = points;
            }
        }
        public string GenerateId()
        {
            string prefix = "";
            int num = number;
            if (main.isRdClicked)
            {
                prefix = "rd";
            }
            else if (main.isGeoClicked) 
            {
                prefix = "geo";
            }
            else
            {
                prefix = "ra";
            }
            string id = prefix + num.ToString();
            number++;
            return id;
        }

        public PolygonDataCollection GetPolygonData (string id)
        {
            return _polygonDict[id];
        }
    }
}
