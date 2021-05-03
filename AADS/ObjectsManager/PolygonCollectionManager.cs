using System;
using System.Collections.Generic;
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
        private Dictionary<string, List<PointLatLng>> _pointsClick = new Dictionary<string, List<PointLatLng>>();
        private Dictionary<string, GMapPolygon> _Polygon = new Dictionary<string, GMapPolygon>();
        private Dictionary<string, string> _name = new Dictionary<string, string>();
        private Dictionary<string, string> _statusExclusive = new Dictionary<string, string>();
        private Dictionary<string, string> _statusInclusive = new Dictionary<string, string>();
        private MainForm main = MainForm.GetInstance();
        private static int number = 001;

        public void Add(string name, string statusEx, string statusIn, string id, List<PointLatLng> points)
        {
            if (!(_Polygon.ContainsKey(id)))
            {
                if (_name != null && _statusExclusive != null && _statusInclusive != null)
                {
                    _pointsClick.Add(id, points);
                    _name.Add(id, name);
                    _statusExclusive.Add(id, statusEx);
                    _statusInclusive.Add(id, statusIn);
                }
            }
        }
        public string FindId(GMapPolygon polygon)
        {
            string id = "";
            if (_Polygon != null)
            {
                foreach (var j in _Polygon.Values)
                {
                    if (j == polygon)
                    {
                        id = _Polygon.Keys.ToString();
                    }
                }
            }
            return id;
        }
        public void Remove(GMapPolygon polygon)
        {
            string id = this.FindId(polygon);
            if ((_Polygon.ContainsKey(id) && _name.ContainsKey(id) && _statusExclusive.ContainsKey(id) && _statusInclusive.ContainsKey(id)))
            {
                _Polygon.Remove(id);
                _name.Remove(id);
                _statusInclusive.Remove(id);
                _statusExclusive.Remove(id);
            }
        }
        public void Update(string id, string name, string statusEx, string statusIn)
        {
            if (_Polygon != null)
            {
                _name[id] = name;
                _statusExclusive[id] = statusEx;
                _statusInclusive[id] = statusIn;
            }
        }
        public List<PointLatLng> GetPoints(string id) 
        {
            return _pointsClick[id]; 
        }
        public void SetPoints(string id, List<PointLatLng> pointsUpdated)
        {
            _pointsClick[id] = pointsUpdated;
        }
        public string GenerateId()
        {
            string prefix = "";
            int num = number;
            if (main.isRdClicked)
            {
                prefix = "rd";
            }
            string id = prefix + num.ToString();
            return id;
        }
    }
}
