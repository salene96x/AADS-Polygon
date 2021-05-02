
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AADS.Views.Marker
{
    public partial class CityCreation : UserControl
    {
        private bool editMode = false;

        public GMarker getmarker = null;

        public GMarkerRect getrect = null;

        private MainForm main = MainForm.GetInstance();

        //ID For Marker
        public static int AutoIncrementID = 0;

        //public static Dictionary<GMapMarker, City> cityMarker = new Dictionary<GMapMarker, City>();

        public CityCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }

        internal void GMap_MouseClick(object sender, MouseEventArgs e)
        {
                PointLatLng point = ControlViews.Main.gMap.FromLocalToLatLng(e.X, e.Y);
                txtPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.Text);
        }  

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Can change eographic coordinate system
            if (cmbPosition.SelectedIndex > -1 && txtPosition.Text != "")
            {
                PointLatLng point = PositionConverter.ParsePointFromString(txtPosition.Text);
                txtPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.Text);
            } 
        }

        public void setEditMode (bool editMode)
        {
            // Edit mode see more detail in CityView. 
            this.editMode = editMode;
            if (editMode)
            {
                btnAddMarker.Text = "ยืนยันการแก้ไข";
            }
            else
            {
                btnAddMarker.Text = "สร้าง";
            }
        }

        public void seCityInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;

            if(getmarker != null)
            {
                MessageBox.Show("Not null");
                txtLabel.Text = main.detailMarkers[getmarker].GetCityLabel().ToString();
                txtName.Text = main.detailMarkers[getmarker].GetCityName().ToString();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetCityPoint(), cmbPosition.Text);
                txtRemark.Text = main.detailMarkers[getmarker].GetCityRemark().ToString();
            }else if (getmarker == null)
            {
                MessageBox.Show("Null");
            }
        }

        public void clearField()
        {
            txtLabel.Text = "";
            txtName.Text = "";
            txtPosition.Text = "";
            txtRemark.Text = "";

            txtLabel.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPosition.ReadOnly = false;
            txtRemark.ReadOnly = false;
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            PointLatLng point = PositionConverter.ParsePointFromString(txtPosition.Text);
            Image image = Image.FromFile("Images/icon/City.png");
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        if (c != txtRemark)
                        {
                            MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                    }
                    
                }
            }

            // TODO : Selecting image to display as marker
            if (editMode == true)
            {
                // If edit mode do this 
                main.markerOverlay.Markers.Remove(getmarker);
                main.markerOverlay.Markers.Remove(getrect);
                AutoIncrementID -= 1;
                main.detailMarkers.Remove(getmarker);
                GMarker marker = new GMarker(point, name, image); 
                GMarkerRect rect = new GMarkerRect(marker);
                City city = new City("CI" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, txtLabel.Text, txtRemark.Text, point);
                main.detailMarkers.Add(marker, city);
                if (MainForm.isConnected)
                {
                    city.Edit();
                }
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                MessageBox.Show("แก้ไขสำเร็จ");
                ControlViews.CityCreation.setEditMode(false);
                clearField();

            }
            else if (editMode == false)
            {
                // If on default mode do this 
                GMarker marker = new GMarker(point, name, image);
                GMarkerRect rect = new GMarkerRect(marker);
                City city = new City("CI" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, txtLabel.Text, txtRemark.Text, point);
                main.detailMarkers.Add(marker, city);

                // TODO : Manage data before adding to overlay
                if (MainForm.isConnected)
                {
                    city.Insert();
                }
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "City Marker");
                clearField();

            }
            
        }
    }
}
