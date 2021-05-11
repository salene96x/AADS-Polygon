using GMap.NET;
using GMap.NET.WindowsForms;
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
            this.btnEditConfirm.Visible = true;
            this.btnEditConfirm.Location = this.btnEdit.Location;
        }

        private void btnEditConfirm_Click(object sender, EventArgs e)
        {
            string id = PolygonCollectionManager.FindId(instanceObj);
            var polygonData = PolygonCollectionManager.GetPolygonData(id);
            polygonData.name = String.Copy(txtName.Text);
            this.isEdit = false;
            this.btnConfirm.Visible = false;
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
