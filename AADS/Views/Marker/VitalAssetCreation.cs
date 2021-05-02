using AADS;
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
    public partial class VitalAssetCreation : UserControl
    {
        private MainForm main = MainForm.GetInstance();

        public bool editMode = false;

        public GMarker getmarker = null;

        public GMarkerRect getrect = null;

        //ID For Marker
        public static int AutoIncrementID = 0;
        int parsedValue;

        public VitalAssetCreation()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
        }

        private void VitalAssetCreation_Load(object sender, EventArgs e)
        {

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
        public void GMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng point = ControlViews.Main.gMap.FromLocalToLatLng(e.X, e.Y);
            txtPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.Text);
        }

        public void setEditMode(bool editMode)
        {
            // Edit mode see more detail in CityView. 
            this.editMode = editMode;
            if (editMode)
            {
                btnCreate.Text = "ยืนยันการแก้ไข";
            }
            else
            {
                btnCreate.Text = "สร้าง";
            }
        }
        public void setVitalAssetInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;

            if (getmarker != null)
            {
                txtName.Text = main.detailMarkers[getmarker].GetVitalAssetName();
                txtPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetVitalAssetPoint(), cmbPosition.Text);
                cmbType.Text = main.detailMarkers[getmarker].GetVitalAssetType();
                txtPriority.Text = main.detailMarkers[getmarker].GetVitalAssetPriority();
                cmbProvince.Text = main.detailMarkers[getmarker].GetVitalAssetProvince();
                txtSize.Text = main.detailMarkers[getmarker].GetVitalAssetAssetSize();
                cmbResponsibleUnit.Text = main.detailMarkers[getmarker].GetVitalAssetUnitResponsible();
                txtUnitStatus.Text = main.detailMarkers[getmarker].GetVitalAssetUnitStatus();
                cmbResponsiblePerson.Text = main.detailMarkers[getmarker].GetVitalAssetResponsePerson();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        public void clearField()
        {
            txtName.Text = "";
            txtPosition.Text = "";
            cmbType.Text = "";
            txtPriority.Text = "";
            cmbProvince.Text = "";
            txtSize.Text = "";
            cmbResponsibleUnit.Text = "";
            txtUnitStatus.Text = "";
            cmbResponsiblePerson.Text = "";

            txtName.ReadOnly = false;
            txtPosition.ReadOnly = false;
            txtPriority.ReadOnly = false;
            txtSize.ReadOnly = false;
            txtUnitStatus.ReadOnly = false;

        }

        private void btnCreate_Click(object sender, EventArgs e)
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
                else if (c is ComboBox)
                {
                    if (c.Text == "")
                    {
                        MessageBox.Show("กรุณาเลือกข้อมูลให้ครบถ้วน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            //if (String.IsNullOrWhiteSpace(txtName.Text) || String.IsNullOrWhiteSpace(cmbResponsibleUnit.Text) || String.IsNullOrWhiteSpace(cmbResponsiblePerson.Text) || String.IsNullOrWhiteSpace(txtPosition.Text)
            //    || String.IsNullOrWhiteSpace(txtPriority.Text) || String.IsNullOrWhiteSpace(cmbProvince.Text) || String.IsNullOrWhiteSpace(txtSize.Text) || String.IsNullOrWhiteSpace(txtUnitStatus.Text)) 
            //{
            //    MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง","คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            if (!int.TryParse(txtPriority.Text, out parsedValue))
            {
                MessageBox.Show("ระดับความสำคัญใส่ได้เฉพาะตัวเลขจำนวนเต็มเท่านั้น", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (parsedValue > 99 || parsedValue <= 0)
            {
                //txtPriority.Text = txtPriority.Text.Substring(0, txtPriority.TextLength - 1);
                MessageBox.Show("ระดับความสำคัญ สามารถใส่ตัวเลขได้ระหว่าง 1-99 เท่านั้น", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (editMode == true)
                {
                    main.markerOverlay.Markers.Remove(getmarker);
                    main.markerOverlay.Markers.Remove(getrect);
                    AutoIncrementID -= 1;
                    main.detailMarkers.Remove(getmarker);

                    Image image = Image.FromFile("Images/icon/VitalAsset.png");
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);

                    //Add Data to dictionary
                    VitalAsset vitalAsset = new VitalAsset("VA" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, cmbType.Text, Int32.Parse(txtPriority.Text), cmbProvince.Text, txtSize.Text, cmbResponsibleUnit.Text, txtUnitStatus.Text, cmbResponsiblePerson.Text, point);
                    main.detailMarkers.Add(marker, vitalAsset);
                    // TODO : Manage data before adding to overlay
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);

                    MessageBox.Show("แก้ไขสำเร็จ");

                    // Set edit mode to false and clearing textbox.
                    ControlViews.CityCreation.setEditMode(false);
                    clearField();
                }
                else {

                    // TODO : Selecting image to display as marker
                    Image image = Image.FromFile("Images/icon/VitalAsset.png");
                    GMarker marker = new GMarker(point, name, image);
                    GMarkerRect rect = new GMarkerRect(marker);

                    //Add Data to dictionary
                    VitalAsset vitalAsset = new VitalAsset("VA" + "0" + (AutoIncrementID += 1).ToString(), txtName.Text, cmbType.Text, Int32.Parse(txtPriority.Text), cmbProvince.Text, txtSize.Text, cmbResponsibleUnit.Text, txtUnitStatus.Text, cmbResponsiblePerson.Text, point);
                    main.detailMarkers.Add(marker, vitalAsset);

                    // TODO : Manage data before adding to overlay
                    main.markerOverlay.Markers.Add(marker);
                    main.markerOverlay.Markers.Add(rect);
                    MessageBox.Show("เพิ่มมาร์คเกอร์ " + marker.Name + " บนแผนที่แล้ว", "VitalAsset Marker");
                    clearField();

                }

            }
        }
    }
}


