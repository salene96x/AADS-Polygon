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
    public partial class FireUnitView : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;

        public FireUnitView()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }

        public void setFireUnitInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if (getmarker != null)
            {
                clearField();
                lbBatteryID.Text = main.detailMarkers[getmarker].GetFireUnitBatteryID();
                lbDetail.Text = main.detailMarkers[getmarker].GetFireUnitDetail();
                lbNumber.Text = main.detailMarkers[getmarker].GetFireUnitNumber();
                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetFireUnitPoint(), cmbPosition.Text);
                lbType.Text = main.detailMarkers[getmarker].GetFireUnitType();
                lbStatus.Text = main.detailMarkers[getmarker].GetFireUnitStatus();

                //if (main.detailMarkers[getmarker].GetFireUnitStatus() == "OP")
                //{
                //    lbStatus.Text = "OP";
                //}
                //else if (main.detailMarkers[getmarker].GetFireUnitStatus() == "Limited")
                //{
                //    lbStatus.Text = "Limited";
                //}
                //else if (main.detailMarkers[getmarker].GetFireUnitStatus() == "NonOP")
                //{
                //    lbStatus.Text = "NonOP";
                //}
            }
        }


        public void clearField()
        {
            lbBatteryID.Text = "";
            lbDetail.Text = "";
            lbNumber.Text = "";
            lbPosition.Text = "";
            lbType.Text = "";
            lbStatus.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ControlViews.Marker.SetControl(ControlViews.FireUnitCreation);
            ControlViews.FireUnitCreation.setEditMode(true);
            ControlViews.FireUnitCreation.setFireUnitInfo(getmarker, getrect);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            main.detailMarkers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getrect);
            ControlViews.Marker.SetControl(ControlViews.FireUnitCreation);
            FireUnitCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว", "FireUnit Marker");
            clearField();
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex > -1 && getmarker != null)
            {
                lbPosition.Text = PositionConverter.ParsePointToString(getmarker.Position, cmbPosition.SelectedItem.ToString());
            }
        }
    }
}
