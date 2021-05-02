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
    public partial class AirportView : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        private bool editMode = false;

        public AirportView()
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

        public void setAirportInfo(GMarker marker, GMarkerRect rect)
        {
            this.getmarker = marker;
            this.getrect = rect;
            if( getmarker != null)
            {
                lbName.Text = main.detailMarkers[getmarker].GetName();
                lbICAO.Text = main.detailMarkers[getmarker].GetAirportICAO();
                lbIATA.Text = main.detailMarkers[getmarker].GetAirportIATA();
                lbCountry.Text = main.detailMarkers[getmarker].GetCountry();
                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetAirportPoint(), cmbPosition.Text);
                if (main.detailMarkers[getmarker].GetAirportInternational() == 1)
                {
                    lbInternational.Text = "สนามบินนานาชาติ";
                }
                else
                {
                    
                    lbInternational.Text = "ไม่ใช่สนามบินนานาชาติ";
                }
            }
        }

        public void clearField()
        {
            lbName.Text = "";
            lbICAO.Text = "";
            lbIATA.Text = "";
            lbCountry.Text = "";
            lbPosition.Text = "";
            lbInternational.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ControlViews.Marker.SetControl(ControlViews.AirportCreation);
            ControlViews.AirportCreation.setEditMode(true);
            ControlViews.AirportCreation.setAirportInfo(getmarker, getrect);
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MainForm.isConnected) { 
                main.detailMarkers[getmarker].Delete();
            }
            main.detailMarkers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getrect);
            AirportCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว", "Airport Marker");
            ControlViews.Marker.SetControl(ControlViews.AirportCreation);
            clearField();
        }
    }
}
