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
    public partial class FireUnitCreation : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private bool editMode = false;
        //ID For Marker
        public static int AutoIncrementID = 0;
        public static string checkStatus = "";
        
        public FireUnitCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
            radioButton3.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                checkStatus = radioButton1.Text;
            }
            else if (radioButton2.Checked == true)
            {
                checkStatus = radioButton2.Text;
            }
            else
            {
                checkStatus = radioButton3.Text;
            }
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
        public void setEditMode(bool editMode)
        {
            this.editMode = editMode;
            if(editMode == true)
            {
                btnCreate.Text = "ยืนยันการแก้ไข";
            }
            else
            {
                btnCreate.Text = "สร้าง";
            }
        }
        public void setFireUnitInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if(getmarker != null)
            {
                clearField();
                txtBatteryID.Text = main.detailMarkers[getmarker].GetFireUnitBatteryID();
                txtDetail.Text = main.detailMarkers[getmarker].GetFireUnitDetail();
                txtNumber.Text = main.detailMarkers[getmarker].GetFireUnitNumber();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetFireUnitPoint(), cmbPosition.Text);
                cmbType.Text = main.detailMarkers[getmarker].GetFireUnitType();
                if(main.detailMarkers[getmarker].GetFireUnitStatus() == "OP")
                {
                    radioButton3.Checked = true;
                }else if(main.detailMarkers[getmarker].GetFireUnitStatus() == "Limited")
                {
                    radioButton2.Checked = true;
                }
                else if (main.detailMarkers[getmarker].GetFireUnitStatus() == "NonOP")
                {
                    radioButton1.Checked = true;
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string name = txtNumber.Text;
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
            if (cmbType.Text == "")
            {
                MessageBox.Show("กรุณาเลือกประเภท", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(editMode == true)
                {
                    main.detailMarkers.Remove(getmarker);
                    main.markerOverlay.Markers.Remove(getmarker);
                    main.markerOverlay.Markers.Remove(getrect);
                    
                    // TODO : Selecting image to display as marker
                    Image image = Image.FromFile("Images/icon/FireUnit.png");
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);
                    AutoIncrementID -= 1;
                    //Add Data to dictionary
                    FireUnit fireUnit = new FireUnit("FU" + "0" + (AutoIncrementID += 1).ToString(),txtBatteryID.Text, txtNumber.Text, cmbType.Text, txtDetail.Text, checkStatus, point);
                    main.detailMarkers.Add(marker, fireUnit);

                    // TODO : Manage data before adding to overlay
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);
                    ControlViews.FireUnitCreation.setEditMode(false);
                    MessageBox.Show("แก้ไขสำเร็จ");
                    clearField();
                }
                else
                {
                    // TODO : Selecting image to display as marker
                    Image image = Image.FromFile("Images/icon/FireUnit.png");
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);

                    //Add Data to dictionary
                    FireUnit fireUnit = new FireUnit("FU" + "0" + (AutoIncrementID += 1).ToString(), txtBatteryID.Text, txtNumber.Text, cmbType.Text, txtDetail.Text, checkStatus, point);
                    main.detailMarkers.Add(marker, fireUnit);

                    // TODO : Manage data before adding to overlay
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);
                    MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "FireUnit Marker");
                    clearField();
                }

            }
        }
        public void clearField()
        {
            txtNumber.Text = "";
            txtBatteryID.Text = "";
            txtDetail.Text = "";
            txtPosition.Text = "";
            cmbType.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = true;
        }


        
    }
}
