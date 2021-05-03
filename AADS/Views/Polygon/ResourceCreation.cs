using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public List<PointLatLng> _points = new List<PointLatLng>();
        private static ResourceCreation instance;
        private MainForm main;
        private ObjectsManager.PolygonCollectionManager polygonCollectionManager;
        private bool isNew;
        public ResourceCreation()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            polygonManager.CreatePolygon(_points);
            polygonManager.isPreview = false;
            polygonManager.CreateRealPoints(_points);
            AddDataToCollection();
        }

        public static ResourceCreation GetInstance()
        {
            return instance;
        }

        private void ResourceCreation_Load(object sender, EventArgs e)
        {
            instance = this;
            var polygonManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonManager");
            var polygonCollectionManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonCollectionManager");
            polygonManager = (ObjectsManager.PolygonManager)polygonManagerWrap.Unwrap();
            polygonCollectionManager = (ObjectsManager.PolygonCollectionManager)polygonCollectionManagerWrap.Unwrap();
            main = MainForm.GetInstance();
        }

        public void SetPoints(List<PointLatLng> _points)
        {
            this._points = _points;
        }
        private void AddDataToCollection()
        {
            string id = polygonCollectionManager.GenerateId();
            polygonCollectionManager.Add(
                txtName.Text, 
                cmbStatusEx.SelectedItem.ToString(), 
                cmbStatusIn.SelectedItem.ToString(), 
                id, _points);
        }
    }
}
