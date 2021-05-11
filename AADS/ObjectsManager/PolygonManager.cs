using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS.ObjectsManager
{
    class PolygonManager
    {
        private static MainForm main = MainForm.GetInstance();
        private static GMapControl mainMap = main.GetmainMap();
        public GMapPolygon polygon { get; set; }
        private PolygonCollectionManager collectionManager;
        public static int index = 0;
        private Views.Polygon.ResourceCreation instanceResource = Views.Polygon.ResourceCreation.GetInstance();
        private Views.Polygon.RestrictedAreaCreation instanceRa = Views.Polygon.RestrictedAreaCreation.GetInstance();
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
            this.CreateRealPoints(_points);
            if (main.isRdClicked)
            {
                this.instanceResource.SetPoint(_points);
            }
            else if (main.isRaClicked)
            {
                this.instanceRa.polygon = polygon;
                foreach (var j in _points)
                {
                    this.instanceRa.SetLb(j);
                }
            }
            overlay.Polygons.Add(polygon);
        }
        public void ClearIndex()
        {
            index = 0;
        }
        public void Preview(List<PointLatLng> _points)
        {
            isPreview = true;
            if (isPreview)
            {
                if (main.isRaClicked)
                {
                    instanceRa.points = new List<PointLatLng>(_points);
                    //instanceRa.SetLb(_points);
                }
                else if (main.isRdClicked)
                {
                    instanceResource.SetPoint(_points);
                }
                var previewOverlay = main.GetOverlay("previewOverlay");
                previewOverlay.IsVisibile = true;
                previewOverlay.Polygons.Clear();
                previewOverlay.Polygons.Remove(polygon);
                polygon = new GMapPolygon(_points, "prevPolygon");
                previewOverlay.Polygons.Add(polygon);
                PointCreate(_points[index]);
                index++;
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
                if (main.isRdClicked)
                {
                    instanceResource.SetListBox(point);
                }
                else if (main.isRaClicked)
                {
                    instanceRa.SetLb(point);
                }
                
            }
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
            
            if (id != null)
            {
                Debug.WriteLine(id);
            }
            var polygonData = PolygonCollectionManager.GetPolygonData(id);
            if (main.isRdClicked)
            {
                instanceResource.FillAttributes(polygonData.name, polygonData.statusEx, polygonData.statusIn, polygonData._point, id);
            }
            else if (main.isRaClicked)
            {
                instanceRa.FillAttributes(polygonData.name, polygonData._point);
                instanceRa.SetObject(viewObj);
            }
            
        }
        public void Remove(GMapPolygon removeObj)
        {
            var overlay = main.GetOverlay("polygonOverlay");
            string id = PolygonCollectionManager.FindId(removeObj);
            var polygonData = PolygonCollectionManager.GetPolygonData(id);
            overlay.Markers.Clear();
            Debug.WriteLine("Delete Polygon ID = " + id);
            PolygonCollectionManager.Remove(removeObj);
            overlay.Polygons.Remove(removeObj);
        }
    }
}
