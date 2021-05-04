﻿using GMap.NET;
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
        public List<PointLatLng> _points = new List<PointLatLng>();
        private static ResourceCreation instance;
        private static int count = 1;
        private MainForm main = MainForm.GetInstance();
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
                polygonManager.CreatePolygon(_points);
                polygonManager.isPreview = false;
                polygonManager.CreateRealPoints(_points);
                AddDataToCollection();
                Reset();
                main.isPolygonFuncClicked = false;
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
        }

        public void SetPoints(List<PointLatLng> _points)
        {
            this._points = _points;
        }
        private void AddDataToCollection()
        {
            string id = collectionManager.GenerateId();
            var polygonObj = new ObjectsManager.PolygonDataCollection(txtName.Text, _points, cmbStatusEx.SelectedItem.ToString(), 
                                                                      cmbStatusIn.SelectedItem.ToString(), polygonManager.polygon);
            collectionManager.Add(id, polygonObj);
            
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
        public void FillAttributes(string name, string statusEx, string statusIn, List<PointLatLng> points)
        {
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

        }
    }
}
