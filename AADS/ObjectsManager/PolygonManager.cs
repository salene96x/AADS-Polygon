using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS.ObjectsManager
{
    class PolygonManager
    {
        private static MainForm main = MainForm.GetInstance();
        public static List<PointLatLng> _points;
        private static GMapControl mainMap = main.GetmainMap();
        public static PolygonManager instance;
        private GMapPolygon polygon;
        private PolygonCollectionManager collectionManager = new PolygonCollectionManager();
        private GMapOverlay PrevOverlay = main.GetOverlay("previewOverlay");
        private GMapOverlay mainOverlay = main.GetOverlay("polygonOverlay");
        private static int index = 0;
        public PolygonManager() { }
        public void CreatePolygon(List<PointLatLng> _points)
        {
            GMapPolygon polygon = new GMapPolygon(_points, "polygon");
        }
        public void Preview(List<PointLatLng> _points)
        {
            PrevOverlay.Polygons.Remove(polygon);
            polygon = new GMapPolygon(_points, "prevPolygon");
            PrevOverlay.Polygons.Add(polygon);
            PointCreate(_points[index]);
            index++;
            _points = _points;
        }
        public void Edit(int index, PointLatLng pointChanged, GMapPolygon polygon)
        {
            string id = collectionManager.FindId(polygon);
            List<PointLatLng> _clickedPoints = collectionManager.GetPoints(id);
            _clickedPoints[index] = pointChanged;
            collectionManager.SetPoints(id, _clickedPoints);
            Preview(_clickedPoints);
        }
        public void PointCreate(PointLatLng point)
        {
            GMapMarker points = new GMarkerGoogle(point, GMarkerGoogleType.red_small);
            PrevOverlay.Markers.Add(points);
        }
        public static PolygonManager GetInstance()
        {
            return instance;
        }
        public List<PointLatLng> GetPoints()
        {
            return _points;
        }
    }
}
