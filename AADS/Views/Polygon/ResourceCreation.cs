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
    public partial class ResourceCreation : UserControl
    {
        private ObjectsManager.PolygonManager polygonManager;
        private ObjectsManager.PolygonCollectionManager collectionManager;
        public static List<PointLatLng> points;
        private static ResourceCreation instance;
        private static int count = 1;
        private MainForm main = MainForm.GetInstance();
        private bool isEdit;
        public GMapPolygon obj { get; set; }
        public string id;
        public ResourceCreation()
        {
            InitializeComponent();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!this.CheckNull())
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "อาณาเขต", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                polygonManager.CreatePolygon(points);
                polygonManager.isPreview = false;
                //polygonManager.CreateRealPoints(points);
                AddDataToCollection(txtName.Text, new List<PointLatLng>(main._pointsPoly), cmbStatusEx.SelectedItem.ToString(), cmbStatusIn.SelectedItem.ToString(), polygonManager.polygon);
                Reset();
                main.SetPolygonFuncClick(false);
                this.isEdit = false;
                polygonManager.ClearIndex();
                main._pointsPoly.Clear();
                main.isRdClicked = false;
            }
        }

        public static ResourceCreation GetInstance()
        {
            return instance;
        }

        private void ResourceCreation_Load(object sender, EventArgs e)
        {
            instance = this;
            var polygonManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonManager");
            var collectionManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonCollectionManager");
            polygonManager = (ObjectsManager.PolygonManager)polygonManagerWrap.Unwrap();
            collectionManager = (ObjectsManager.PolygonCollectionManager)collectionManagerWrap.Unwrap();
            //this.panelEditDel.Visible = false;
        }

        private void AddDataToCollection(string name, List<PointLatLng> points, string statusEx, string statusIn, GMapPolygon polygon)
        {
            string id = collectionManager.GenerateId();
            var polygonObj = new ObjectsManager.PolygonDataCollection(name, points, polygon, statusEx, statusIn);
            collectionManager.Add(id, polygonObj);
            
            Debug.WriteLine(polygonObj._point.Count);
            main._pointsPoly.Clear();

        }
        private bool CheckNull()
        {
            bool check = true;
            foreach (var j in this.Controls.OfType<ComboBox>())
            {
                if (j.SelectedIndex == -1)
                {
                    check = false;
                }
            }
            if (txtName.Text == "")
            {
                check = false;
            }
            else
            {
                check = true;
            }

            return check;
        }
        public void SetObj(GMapPolygon objToSet)
        {
            this.obj = objToSet;
        }
        private void Reset()
        {
            txtName.Text = null;
            cmbStatusEx.SelectedIndex = -1;
            cmbStatusIn.SelectedIndex = -1;
            lbPoints.Items.Clear();
            count = 1;
        }
        public void SetListBox(PointLatLng point)
        {
            lbPoints.Items.Add("จุดที่ "+ count.ToString() + " = " + point.Lat.ToString() + " , " + point.Lng.ToString());
            count++;
        }
        public void FillAttributes(string name, string statusEx, string statusIn, List<PointLatLng> points, string id)
        {
            this.id = id;
            txtName.Text = name;
            if (statusEx == "Inactive")
            {
                cmbStatusEx.SelectedItem = "Inactive";
            }
            else
            {
                cmbStatusEx.SelectedItem = "Active";
            }

            if (statusIn == "Inactive")
            {
                cmbStatusIn.SelectedItem = "Inactive";
            }
            else
            {
                cmbStatusIn.SelectedItem = "Active";
            }
            int countP = 1;
            foreach (var j in points)
            {
                lbPoints.Items.Add("จุดที่ " + countP.ToString() + " = " + j.Lat.ToString() + " , " + j.Lng.ToString());
                countP++;
            }
            ViewMode();
        }
        public void ViewMode()
        {
            btnConfirm.Visible = false;
            btnDel.Visible = true;
            btnEdit.Visible = true;
        }
        public void SetPolygon(GMapPolygon obj2) { obj = obj2; }
        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ยืนยันที่จะลบอาณาเขตนี้หรือไม่", "อาณาเขต", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                polygonManager.Remove(obj);
                Reset();
                this.btnConfirm.Visible = true;
                this.btnEditConfirm.Visible = this.btnDel.Visible = this.btnEdit.Visible = false;
                //this.panelEditDel.Visible = false;
            }
        }
        List<PointLatLng> cacheList;
        List<GMarkerGoogle> markerList = new List<GMarkerGoogle>();
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.main.isPolygonEdit = true;
            this.btnEdit.Visible = false;
            this.btnDel.Visible = false;
            this.isEdit = true;
            this.btnEditConfirm.Visible = true;
            string id = collectionManager.FindId(obj);
            var polygonData = collectionManager.GetPolygonData(id);
            this.cacheList = new List<PointLatLng>(polygonData._point);
            foreach (var j in polygonData._point) 
            {
                GMapOverlay previewOverlay = main.GetOverlay("previewOverlay");
                previewOverlay.IsVisibile = true;
                GMarkerGoogle marker = new GMarkerGoogle(j, GMarkerGoogleType.orange_small);
                this.markerList.Add(marker);
                previewOverlay.Markers.Add(marker);
            }
            var polygonOverlay = main.GetOverlay("polygonOverlay");
            polygonOverlay.IsVisibile = false;
        }
        public void CheckChangedPosition(GMapMarker marker)
        {
            var previewOverlay = main.GetOverlay("previewOverlay");
            for (int i = 0; i < markerList.Count; i++)
            {
                if ((GMarkerGoogle) markerList[i] == marker)
                {
                    var temp = cacheList[i];
                    temp.Lat = marker.Position.Lat;
                    temp.Lng = marker.Position.Lng;
                    cacheList[i] = temp;
                    var polygon = new GMapPolygon(cacheList, "polygon");
                    previewOverlay.Polygons.Clear();
                    previewOverlay.Polygons.Add(polygon);
                    
                }
            }
        }
        void CreateRealEdittedPolygon()
        {
            var polygonOverlay = main.GetOverlay("polygonOverlay");
            polygonOverlay.Polygons.Remove(obj);
            polygonOverlay.IsVisibile = true;
            var previewOverlay = main.GetOverlay("previewOverlay");
            previewOverlay.Markers.Clear();
            previewOverlay.Polygons.Clear();
            previewOverlay.IsVisibile = false;
            var polygon = new GMapPolygon(cacheList, "polygon");
            polygon.IsHitTestVisible = true;
            polygonOverlay.Polygons.Add(polygon);
            obj = polygon;
            main.isPolygonEdit = false;
        }
        private void btnEditConfirm_Click(object sender, EventArgs e)
        {
            if (!this.CheckNull())
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "อาณาเขต", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var polygonData = collectionManager.GetPolygonData(this.id);
                this.CreateRealEdittedPolygon();
                polygonData.name = txtName.Text;
                polygonData.statusEx = cmbStatusEx.SelectedItem.ToString();
                polygonData.statusIn = cmbStatusIn.SelectedItem.ToString();
                polygonData._point = new List<PointLatLng>(cacheList);
                polygonData.polygon = obj;
                MessageBox.Show("แก้ไขข้อมูลเสร็จสิ้น", "อาณาเขต", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Reset();
                this.btnEditConfirm.Visible = false;
                this.btnConfirm.Visible = true;
            }
        }
        public void SetPoint(List<PointLatLng> pointLatLngs)
        {
            points = pointLatLngs;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            polygonManager.prevOvl.Markers.Clear();
            polygonManager.prevOvl.Polygons.Clear();
            main.panelRightShow.Controls.Clear();
            main.SetPolygonFuncClick(false);
            main.isGeoClicked = false;
            main.isRaClicked = false;
            main.isRdClicked = false;
        }
    }
}
