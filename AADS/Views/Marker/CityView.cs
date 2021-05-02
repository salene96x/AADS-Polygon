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
    public partial class CityView : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        public bool editMode = false;


        public CityView()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }

        public void setCityInfo(GMarker marker, GMarkerRect rect)
        {
            getmarker = marker;
            getrect = rect;

            if (main.detailMarkers.ContainsKey(marker))
            {
                clearField();


                lbName.Text = main.detailMarkers[getmarker].GetCityName();

                lbLabel.Text = main.detailMarkers[getmarker].GetCityLabel();

                lbRemark.Text = main.detailMarkers[getmarker].GetCityRemark();

                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetCityPoint(), cmbPosition.Text);
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
            CityCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว","City Marker");
            clearField();
            ControlViews.Marker.SetControl(ControlViews.CityCreation);
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
            editMode = true;
            ControlViews.Marker.SetControl(ControlViews.CityCreation);
            ControlViews.CityCreation.setEditMode(true);
            ControlViews.CityCreation.seCityInfo(getmarker, getrect);
        }

        internal void GMap_MouseClick(object sender, MouseEventArgs e)
        {
            //Not use for now

            // point = ControlViews.Main.gMap.FromLocalToLatLng(e.X, e.Y);
            //lbPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.Text);
        }

    }
}
