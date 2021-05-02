using GMap.NET;
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
    /*public struct AirportDetail
    {
        public string Name;
        public string Detail;
        public AirportDetail(string name, string detail)
        {
            Name = name;
            Detail = detail;
        }
    }*/
    public partial class AirportCreation : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private bool editMode = false;
        //ID For Marker
        public static int AutoIncrementID = 0;
        public static int checkInter = 0;
        public AirportCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }
        private void chbInternational_CheckedChanged(object sender, EventArgs e)
        {
            if (chbInternational.Checked == true)
            {
                checkInter = 1;
            }
            else
            {
                checkInter = 0;
            }
        }
        private void AirportCreation_Load(object sender, EventArgs e)
        {
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

        public void setAirportInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;

            if(getmarker != null)
            {
                clearField();
                txtName.Text = main.detailMarkers[getmarker].GetName();
                txtICAO.Text = main.detailMarkers[getmarker].GetAirportICAO();
                txtIATA.Text = main.detailMarkers[getmarker].GetAirportIATA();
                txtCountry.Text = main.detailMarkers[getmarker].GetCountry();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetAirportPoint(), cmbPosition.Text);
                if(main.detailMarkers[getmarker].GetAirportInternational() == 1)
                {
                    chbInternational.Checked = true;
                }
                else
                {
                    chbInternational.Checked = false;
                }
            }
        }
        public void clearField()
        {
            txtName.Text = "";
            txtICAO.Text = "";
            txtIATA.Text = "";
            txtCountry.Text = "";
            txtPosition.Text = "";
            chbInternational.Checked = false;
        }

        public void setEditMode(bool editMode)
        {
            this.editMode = editMode;
            if(editMode == true)
            {
                btnAddMarker.Text = "ยืนยันการแก้ไข";
            }
            else
            {
                btnAddMarker.Text = "สร้าง";
            }
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            PointLatLng point = PositionConverter.ParsePointFromString(txtPosition.Text);
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                
            }
            if (editMode == true)
            {
                main.detailMarkers.Remove(getmarker);
                main.markerOverlay.Markers.Remove(getmarker);
                main.markerOverlay.Markers.Remove(getrect);
                AutoIncrementID -= 1;
                Image image = Image.FromFile("Images/icon/Airport.png");
                GMarker marker = new GMarker(point, name, image);
                GMarkerRect rect = new GMarkerRect(marker);

                //Add Data to Dictionary
                Airport airport = new Airport("AP" + "0" + (AutoIncrementID += 1).ToString(), txtCountry.Text, txtName.Text, checkInter, txtICAO.Text, txtIATA.Text, point);
                main.detailMarkers.Add(marker, airport);

                // TODO : Manage data before adding to overlay
                if (MainForm.isConnected) 
                { 
                    airport.Edit(); 
                }
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                ControlViews.AirportCreation.setEditMode(false);
                MessageBox.Show("แก้ไขสำเร็จ");
                clearField();
            }
            else
            {
                Image image = Image.FromFile("Images/icon/Airport.png");
                GMarker marker = new GMarker(point, name, image);
                GMarkerRect rect = new GMarkerRect(marker);

                //Add Data to Dictionary
                Airport airport = new Airport("AP" + "0" + (AutoIncrementID += 1).ToString(), txtCountry.Text, txtName.Text, checkInter, txtICAO.Text, txtIATA.Text, point);
                main.detailMarkers.Add(marker, airport);

                // TODO : Manage data before adding to overlay
                if (MainForm.isConnected)
                {
                    airport.Insert();
                }
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "Airport Marker");
                clearField();
            }


               
        }

    }
}
