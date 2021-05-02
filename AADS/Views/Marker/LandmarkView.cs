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
    public partial class LandmarkView : UserControl
    {
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private MainForm main = MainForm.GetInstance();

        public LandmarkView()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }
        public void setLandMarkInfo(GMarker marker, GMarkerRect rect)
        {
            getmarker = marker;
            getrect = rect;

            if (main.detailMarkers.ContainsKey(marker))
            {
                clearField();


                lbName.Text = main.detailMarkers[getmarker].GetLandMarkName();

                lbLabel.Text = main.detailMarkers[getmarker].GetLandMarkLabel();

                lbRemark.Text = main.detailMarkers[getmarker].GetLandMarkRemark();

                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetLandmarkPoint(), cmbPosition.Text);

                lbType.Text = main.detailMarkers[getmarker].GetLandMarkType();
            }
        }

        private void CityView_Load(object sender, EventArgs e)
        {
            // Not use for now
        }
        public void clearField()
        {
            lbLabel.Text = "";
            lbName.Text = "";
            lbPosition.Text = "";
            lbRemark.Text = "";
        }

        private void btnDelMarker_Click(object sender, EventArgs e)
        {
            if (MainForm.isConnected)
            {
                main.detailMarkers[getmarker].Delete();
            }
            main.detailMarkers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getrect);
            LandmarkCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว", "LandMark Marker");
            clearField();
            ControlViews.Marker.SetControl(ControlViews.LandmarkCreation);
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex > -1 && getmarker != null)
            {
                lbPosition.Text = PositionConverter.ParsePointToString(getmarker.Position, cmbPosition.SelectedItem.ToString());
            }
        }

        private void btnEditMarker_Click(object sender, EventArgs e)
        {
            ControlViews.Marker.SetControl(ControlViews.LandmarkCreation);
            ControlViews.LandmarkCreation.setEditMode(true);
            ControlViews.LandmarkCreation.setLandMarkInfo(getmarker, getrect);
        }
    }
}
