using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS.ObjectsManager
{
    class PolygonCollectionManager
    {
        public PolygonCollectionManager() { }
        private Dictionary<string, GMapPolygon> _Polygon = new Dictionary<string, GMapPolygon>();
        private Dictionary<string, string> _name = new Dictionary<string, string>();
        private Dictionary<string, string> _statusExclusive = new Dictionary<string, string>();
        private Dictionary<string, string> _statusInclusive = new Dictionary<string, string>();

        public void Add(string name, string statusEx, string statusIn, string id)
        {
            if (!(_Polygon.ContainsKey(id)))
            {
                if (_name != null && _statusExclusive != null && _statusInclusive != null)
                {
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
        public void remove(GMapPolygon polygon)
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
        public void update(string id, string name, string statusEx, string statusIn)
        {
            if (_Polygon != null)
            {
                _name[id] = name;
                _statusExclusive[id] = statusEx;
                _statusInclusive[id] = statusIn;
            }
        }
    }
}
