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
    public partial class FixedPointView : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        public FixedPointView()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }


        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex > -1 && getmarker != null)
            {
                lbPosition.Text = PositionConverter.ParsePointToString(getmarker.Position, cmbPosition.SelectedItem.ToString());
            }
        }
        public void setFixedPointInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if(getmarker != null)
            {
                clearField();
                lbLabel.Text = main.detailMarkers[getmarker].GetFixedPointLabel();
                lbNumber.Text = main.detailMarkers[getmarker].GetFixedPointNumber();
                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetFixedPoint_Point(), cmbPosition.Text);
                lbRemark.Text = main.detailMarkers[getmarker].GetFixedPointSource();
                lbType.Text = main.detailMarkers[getmarker].GetFixedPointType();

                if (main.detailMarkers[getmarker].GetFixedPointLiveSIM() == 1)
                {
                    lbLiveSIM.Text = "Check box Checked";
                }
                else
                {
                    lbLiveSIM.Text = "Not Check";
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ControlViews.Marker.SetControl(ControlViews.FixedPointCreation);
            ControlViews.FixedPointCreation.seteditMode(true);
            ControlViews.FixedPointCreation.setFixedPointInfo(getmarker, getrect);

        }

        public void clearField()
        {
            lbLabel.Text = "";
            lbNumber.Text = "";
            lbPosition.Text = "";
            lbRemark.Text = "";
            lbType.Text = "";
            lbLiveSIM.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            main.detailMarkers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getrect);
            ControlViews.Marker.SetControl(ControlViews.FixedPointCreation);
            FixedPointCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว", "FixedPoint Marker");
            clearField();
        }
    }
}
