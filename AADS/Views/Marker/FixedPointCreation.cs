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
    public partial class FixedPointCreation : UserControl
    {
        private MainForm main = MainForm.GetInstance();

        //ID For Marker
        public static int AutoIncrementID = 0;
        public static string checkType = "";
        public static int checkSIM = 0;
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private bool editMode = false;
        public FixedPointCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
            rdbTactical.Checked = true;
        }

        internal void GMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng point = ControlViews.Main.gMap.FromLocalToLatLng(e.X, e.Y);
            txtPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.Text);
        }
        private void rdbTactical_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTactical.Checked == true)
            {
                checkType = rdbTactical.Text;
            }
            else if (rdbMRadar.Checked == true)
            {
                checkType = rdbMRadar.Text;
            }
            else if (rdbFRadar.Checked == true)
            {
                checkType = rdbFRadar.Text;
            }
            else if (rdbOBSPost.Checked == true)
            {
                checkType = rdbOBSPost.Text;
            }
            else
            {
                checkType = rdbSpecial.Text;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkSIM = 1;
            }
            else
            {
                checkSIM = 0;
            }
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
        public void seteditMode(bool editMode)
        {
            this.editMode = editMode;
            if (editMode == true)
            {
                btnAddMarker.Text = "ยืนยันแก้ไข";
            }
            else
            {
                btnAddMarker.Text = "สร้าง";
            }
        }
        public void setFixedPointInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if(getmarker != null)
            {
                txtLabel.Text = main.detailMarkers[getmarker].GetFixedPointLabel();
                txtNumber.Text = main.detailMarkers[getmarker].GetFixedPointNumber();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetFixedPoint_Point(), cmbPosition.Text);
                txtRemark.Text = main.detailMarkers[getmarker].GetFixedPointSource();
                if (main.detailMarkers[getmarker].GetFixedPointType() == rdbFRadar.Text)
                {
                    rdbFRadar.Checked = true;
                }else if (main.detailMarkers[getmarker].GetFixedPointType() == rdbMRadar.Text)
                {
                    rdbMRadar.Checked = true;
                }
                else if (main.detailMarkers[getmarker].GetFixedPointType() == rdbOBSPost.Text)
                {
                    rdbOBSPost.Checked = true;
                }
                else if (main.detailMarkers[getmarker].GetFixedPointType() == rdbSpecial.Text)
                {
                    rdbSpecial.Checked = true;
                }
                else if (main.detailMarkers[getmarker].GetFixedPointType() == rdbTactical.Text)
                {
                    rdbTactical.Checked = true;
                }
                if(main.detailMarkers[getmarker].GetFixedPointLiveSIM() == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }
        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            string name = txtNumber.Text;
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
            if (editMode == true)
            {
                main.detailMarkers.Remove(getmarker);
                main.markerOverlay.Markers.Remove(getmarker);
                main.markerOverlay.Markers.Remove(getrect);
                AutoIncrementID -= 1;
                Image image = Image.FromFile("Images/icon/FIxedPoint.png");
                GMarker marker = new GMarker(point, name, image);
                GMarkerRect rect = new GMarkerRect(marker);

                //Add Data to dictionary
                FixedPoint fixedPoint = new FixedPoint("FP" + "0" + (AutoIncrementID += 1).ToString(), txtNumber.Text, checkType, txtLabel.Text, checkSIM, txtRemark.Text, point);
                main.detailMarkers.Add(marker, fixedPoint);

                // TODO : Manage data before adding to overlay
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                this.seteditMode(false);
                MessageBox.Show("แก้ไขสำเร็จ");
                clearField();
            }
            else
            {
                // TODO : Selecting image to display as marker
                Image image = Image.FromFile("Images/icon/FIxedPoint.png");
                GMarker marker = new GMarker(point, name, image);
                GMarkerRect rect = new GMarkerRect(marker);

                //Add Data to dictionary
                FixedPoint fixedPoint = new FixedPoint("FP" + "0" + (AutoIncrementID += 1).ToString(), txtNumber.Text, checkType, txtLabel.Text, checkSIM, txtRemark.Text, point);
                main.detailMarkers.Add(marker, fixedPoint);

                // TODO : Manage data before adding to overlay
                main.markerOverlay.Markers.Add(marker);
                main.markerOverlay.Markers.Add(rect);
                MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "FixedPoint Marker");
                clearField();
            }
        }

        public void clearField()
        {
            txtLabel.Text = "";
            txtNumber.Text = "";
            txtPosition.Text = "";
            txtRemark.Text = "";
            checkBox1.Checked = false;
            rdbFRadar.Checked = false;
            rdbMRadar.Checked = false;
            rdbOBSPost.Checked = false;
            rdbSpecial.Checked = false;
            rdbTactical.Checked = true;
        }
    }
}
