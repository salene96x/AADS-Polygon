using AADS;
using GMap.NET;
using GMap.NET.WindowsForms;
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
    public partial class LandmarkCreation : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private bool editMode = false;

        //ID For Marker
        public static int AutoIncrementID = 0;

        public LandmarkCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }
        Image image = null;

        public void clearField()
        {
            //change txtbox to empty
            txtLabel.Text = "";
            txtPosition.Text = "";
            cmbType.Text = "";
            txtName.Text = "";
            txtRemark.Text = "";
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
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select images from cmb
            if (cmbType.SelectedIndex == 0)
            {
                image = Image.FromFile("Images/icon/Landmark.png");
            }
            else if (cmbType.SelectedIndex == 1)
            {
                image = Image.FromFile("Images/landmark/040-royal palace.png");
            }
            else if (cmbType.SelectedIndex == 2)
            {
                image = Image.FromFile("Images/landmark/039-temple.png");
            }
            else if (cmbType.SelectedIndex == 3)
            {
                image = Image.FromFile("Images/landmark/026-police station.png");
            }
            else if (cmbType.SelectedIndex == 4)
            {
                image = Image.FromFile("Images/landmark/014-hospital.png");
            }
            else if (cmbType.SelectedIndex == 5)
            {
                image = Image.FromFile("Images/landmark/011-education.png");
            }
        }

        public void setLandMarkInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if (getmarker != null)
            {
                clearField();
                txtName.Text = main.detailMarkers[getmarker].GetLandMarkName();
                txtLabel.Text = main.detailMarkers[getmarker].GetLandMarkLabel();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetLandmarkPoint(), cmbPosition.Text);
                txtRemark.Text = main.detailMarkers[getmarker].GetLandMarkRemark();
                cmbType.Text = main.detailMarkers[getmarker].GetLandMarkType();
            }
        }

        public void setEditMode(bool editMode)
        {
            this.editMode = editMode;
            if(editMode == true)
            {
                btnAddMarker.Text = "ยันยันการแก้ไข";
            }
            else
            {
                btnAddMarker.Text = "สร้าง";
            }
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            string name = txtLabel.Text;
            PointLatLng point = PositionConverter.ParsePointFromString(txtPosition.Text);
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
            if (cmbType.Text == "")
            {
                //Check if user not seleted cmbType
                MessageBox.Show("กรุณาเลือกชนิด", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(editMode == true)
                {
                    
                    main.detailMarkers.Remove(getmarker);
                    main.markerOverlay.Markers.Remove(getmarker);
                    main.markerOverlay.Markers.Remove(getrect);
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);
                    AutoIncrementID -= 1;
                    //Add Data to Dictionary
                    LandMark landMark = new LandMark("LM" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, txtLabel.Text, point, cmbType.SelectedItem.ToString(), txtRemark.Text);
                    main.detailMarkers.Add(marker, landMark);

                    // TODO : Manage data before adding to overlay
                    if (MainForm.isConnected)
                    {
                        landMark.Edit();
                    }
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);
                    MessageBox.Show("แก้ไขสำเร็จ");
                    ControlViews.LandmarkCreation.setEditMode(false);
                    clearField();
                }
                else
                {
                    // TODO : Selecting image to display as marker
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);

                    //Add Data to Dictionary
                    LandMark landMark = new LandMark("LM" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, txtLabel.Text, point, cmbType.SelectedItem.ToString(),txtRemark.Text);
                    main.detailMarkers.Add(marker, landMark);
                    // TODO : Manage data before adding to overlay
                    if (MainForm.isConnected)
                    {
                        landMark.Insert();
                    }
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);
                    MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "LandMark Marker");
                    clearField();
                }

            }
            
        }

        
    }
}
