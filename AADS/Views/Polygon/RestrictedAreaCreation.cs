using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS.Views.Polygon
{
    public partial class RestrictedAreaCreation : UserControl
    {
        private static RestrictedAreaCreation Instance;
        private static GMapPolygon instanceObj;
        public List<PointLatLng> points;
        public GMapPolygon polygon;
        private ObjectsManager.PolygonCollectionManager PolygonCollectionManager;
        private ObjectsManager.PolygonManager PolygonManager;
        private bool isEdit;
        private static int index = 1;
        private MainForm instanceMain;
        public RestrictedAreaCreation()
        {
            InitializeComponent();
        }

        private void RestrictedAreaCreation_Load(object sender, EventArgs e)
        {
            Instance = this;
            instanceMain = MainForm.GetInstance();
            //create instances of polygon managers
            var PolygonCollectionManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonCollectionManager");
            var PolygonManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonManager");
            PolygonCollectionManager = (ObjectsManager.PolygonCollectionManager)PolygonCollectionManagerWrap.Unwrap();
            PolygonManager = (ObjectsManager.PolygonManager)PolygonManagerWrap.Unwrap();

            this.panelEditDel.Visible = false;
            this.btnConfirm.Visible = true;
            this.btnConfirm.BringToFront();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //create a real polygon which is not a preview polygon
            PolygonManager.CreatePolygon(points);

            //Create an object to collect data of polygon
            var polygonData = new ObjectsManager.PolygonDataCollection(txtName.Text, points, polygon);
            string id = PolygonCollectionManager.GenerateId();

            //Add to dict of polygon
            PolygonCollectionManager.Add(id, polygonData);

            //reset index value
            index = 1;

            //reset attributes
            Reset();
            instanceMain.isRaClicked = false;
            instanceMain.SetPolygonFuncClick(false);
            instanceMain._pointsPoly.Clear();
        }
        public static RestrictedAreaCreation GetInstance()
        {
            return Instance;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("คุณต้องการที่จะลบอาณาเขตนี้ทิ้งใช่หรือไม่", "อาณาเขต", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); 
            if (dialogResult == DialogResult.Yes)
            {
                PolygonManager.Remove(instanceObj);
            }
            Reset();
        }

        public void SetObject(GMapPolygon obj)
        {
            instanceObj = obj;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.isEdit = true;
            this.btnEdit.Visible = false;
            this.btnDel.Visible = false;
            this.btnConfirm.Visible = false;
            this.btnCancel.Visible = false;
            this.btnEditConfirm.Visible = true;
            this.btnEditConfirm.Location = this.btnEdit.Location;
            this.instanceMain.isPolygonEdit = true;
            this.CreateMarker();  
        }
        List<PointLatLng> cacheList;
        List<PointLatLng> secondCacheList;
        List<GMarkerGoogle> markerList;
        List<PointLatLng> tempList;
        public void CheckChangedPosition(GMapMarker marker)
        {
            var polygonOverlay = instanceMain.GetOverlay("polygonOverlay");
            polygonOverlay.IsVisibile = false;
            for (int i = 0; i < markerList.Count; i++)
            {
                if ((GMapMarker) markerList[i] == marker)
                {
                    var v = cacheList[i];
                    v.Lat = marker.Position.Lat;
                    v.Lng = marker.Position.Lng;
                    cacheList[i] = v;
                    GMapOverlay ovl = instanceMain.GetOverlay("previewOverlay");
                    GMapPolygon poly = new GMapPolygon(cacheList, "polygon");
                    ovl.Polygons.Clear();
                    ovl.Polygons.Add(poly);
                }
            }
        }
        public void CreateMarker()
        {
            string id = PolygonCollectionManager.FindId(instanceObj);
            var polygon = PolygonCollectionManager.GetPolygonData(id);
            cacheList = new List<PointLatLng>(polygon._point);
            tempList = new List<PointLatLng>(polygon._point);
            var prevOvl = instanceMain.GetOverlay("previewOverlay");
            prevOvl.IsVisibile = true;
            prevOvl.Polygons.Clear();
            prevOvl.Markers.Clear();
            markerList = new List<GMarkerGoogle>();
            foreach (var j in polygon._point)
            {
                var preMarker = new GMarkerGoogle(j, GMarkerGoogleType.orange_small);
                prevOvl.Markers.Add(preMarker);
                markerList.Add(preMarker);
            }
        }
        void CreateRealPolygonAfterEditted()
        {
            var polygonOverlay = instanceMain.GetOverlay("polygonOverlay");
            polygonOverlay.Polygons.Remove(instanceObj);
            var previewOverlay = instanceMain.GetOverlay("previewOverlay");
            polygonOverlay.IsVisibile = true;
            previewOverlay.IsVisibile = false;
            previewOverlay.Polygons.Clear();
            previewOverlay.Markers.Clear();
            GMapPolygon polygon = new GMapPolygon(cacheList, "polygon") ;
            polygonOverlay.Polygons.Add(polygon);
            polygon.IsHitTestVisible = true;
            instanceObj = polygon;
            this.instanceMain.isPolygonEdit = false;
        }
        private void btnEditConfirm_Click(object sender, EventArgs e)
        {
            this.btnDel.Visible = true;
            this.btnEdit.Visible = true;
            this.btnCancel.Visible = true;
            if (this.txtName.Text != "")
            {
                string id = PolygonCollectionManager.FindId(instanceObj);
                var polygonData = PolygonCollectionManager.GetPolygonData(id);
                this.CreateRealPolygonAfterEditted();
                polygonData.name = String.Copy(txtName.Text);
                polygonData._point = new List<PointLatLng>(cacheList);
                polygonData.polygon = instanceObj;
                this.isEdit = false;
                this.btnConfirm.Visible = false;
                MessageBox.Show("แก้ไขข้อมูลเสร็จสิ้น", "อาณาเขต", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Reset();
            }
            else
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "อาณาเขต", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        public void SetLb (PointLatLng point)
        {
            lbPoints.Items.Add("จุดที่ " + index + " " + point.Lat + " " + point.Lng);
            index++;
        }
        public void FillAttributes(string name, List<PointLatLng> pointsToFill)
        {
            this.panelEditDel.Visible = true;
            this.txtName.Text = name;
            foreach (var j in pointsToFill)
            {
                this.SetLb(j);
            }
        }
        void Reset()
        {
            txtName.Text = "";
            lbPoints.Items.Clear();
        }
    }
}
