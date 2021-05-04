using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public GMapPolygon polygon { get; set; }
        private PolygonCollectionManager collectionManager;
        private static int index = 0;
        private Views.Polygon.ResourceCreation instanceResource = Views.Polygon.ResourceCreation.GetInstance();
        private ObjectsManager.PolygonCollectionManager PolygonCollectionManager;
        public bool isPreview;
        public PolygonManager() 
        {
            var collectionManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonCollectionManager");
            PolygonCollectionManager = (PolygonCollectionManager)collectionManagerWrap.Unwrap();
        }
        public void CreatePolygon(List<PointLatLng> _points)
        {
            var previewOverlay = main.GetOverlay("previewOverlay");
            previewOverlay.IsVisibile = false;
            var overlay = main.GetOverlay("polygonOverlay");
            GMapPolygon polygon = new GMapPolygon(_points, "polygon");
            this.polygon = polygon;
            this.polygon.IsHitTestVisible = true;
            overlay.Polygons.Add(polygon);
        }
        public void Preview(List<PointLatLng> _points)
        {
            isPreview = true;
            if (isPreview)
            {
                var previewOverlay = main.GetOverlay("previewOverlay");
                previewOverlay.Polygons.Remove(polygon);
                polygon = new GMapPolygon(_points, "prevPolygon");
                previewOverlay.Polygons.Add(polygon);
                PointCreate(_points[index]);
                index++;
                instanceResource.SetPoints(_points);
            }
            
        }
        public void Edit(int index, PointLatLng pointChanged, GMapPolygon polygon)
        {
            string id = collectionManager.FindId(polygon);
            //List<PointLatLng> _clickedPoints = collectionManager.GetPoints(id);
            //_clickedPoints[index] = pointChanged;
            //collectionManager.SetPoints(id, _clickedPoints);
            //Preview(_clickedPoints);
        }
        public void PointCreate(PointLatLng point)
        {
            if (isPreview)
            {
                var previewOverlay = main.GetOverlay("previewOverlay");
                GMapMarker points = new GMarkerGoogle(point, GMarkerGoogleType.red_small);
                previewOverlay.Markers.Add(points);
                instanceResource.SetListBox(point);
            }
        }
        public List<PointLatLng> GetPoints()
        {
            return _points;
        }
        public void CreateRealPoints(List<PointLatLng> _pointsToCreate)
        {
            if (!(isPreview))
            {
                GMapOverlay overlay = main.GetOverlay("polygonOverlay");
                foreach (var j in _pointsToCreate)
                {
                    GMapMarker point = new GMarkerGoogle(j, GMarkerGoogleType.red_small);
                    overlay.Markers.Add(point);
                }
            }
        }
        public void View(GMapPolygon viewObj)
        {
            string id = PolygonCollectionManager.FindId(viewObj);
            var polygonData = PolygonCollectionManager.GetPolygonData(id);
            instanceResource.FillAttributes(polygonData.name, polygonData.statusEx, polygonData.statusIn, polygonData._point);
        }
        public void Remove(GMapPolygon removeObj)
        {
            var overlay = main.GetOverlay("polygonOverlay");
            overlay.Polygons.Remove(removeObj);
            PolygonCollectionManager.Remove(removeObj);
            overlay.Markers.Clear();
            Debug.WriteLine("Delete Polygon ID = " + PolygonCollectionManager.FindId(removeObj));
        }
    }
}
