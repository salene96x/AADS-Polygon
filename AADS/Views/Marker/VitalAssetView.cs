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
    public partial class VitalAssetView : UserControl
    {
        private MainForm main = MainForm.GetInstance();
        GMarker getmarker = null;
        GMarkerRect getrect = null;
        public VitalAssetView()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
        }



        public void setVitalAssetInfo(GMarker marker , GMarkerRect rect)
        {
            getmarker = marker;
            getrect = rect;
            if (main.detailMarkers.ContainsKey(marker)){
                clearField();
                lbName.Text = main.detailMarkers[getmarker].GetVitalAssetName();
                lbPosition.Text = PositionConverter.ParsePointToString(main.detailMarkers[getmarker].GetVitalAssetPoint(), cmbPosition.Text);
                lbType.Text = main.detailMarkers[getmarker].GetVitalAssetType();
                lbPriority.Text = main.detailMarkers[getmarker].GetVitalAssetPriority();
                lbProvince.Text = main.detailMarkers[getmarker].GetVitalAssetProvince();
                lbSize.Text = main.detailMarkers[getmarker].GetVitalAssetAssetSize();
                lbResponsibleUnit.Text = main.detailMarkers[getmarker].GetVitalAssetUnitResponsible();
                lbUnitStatus.Text = main.detailMarkers[getmarker].GetVitalAssetUnitStatus();
                lbResponsiblePerson.Text = main.detailMarkers[getmarker].GetVitalAssetResponsePerson();
            }
        }


        public void clearField()
        {
            lbName.Text = "";
            lbPosition.Text = "";
            lbType.Text = "";
            lbPriority.Text = "";
            lbProvince.Text = "";
            lbSize.Text = "";
            lbResponsibleUnit.Text = "";
            lbUnitStatus.Text = "";
            lbResponsiblePerson.Text = "";
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex > -1 && getmarker != null)
            {
                lbPosition.Text = PositionConverter.ParsePointToString(getmarker.Position, cmbPosition.SelectedItem.ToString());
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            ControlViews.Marker.SetControl(ControlViews.VitalAssetCreation);
            ControlViews.VitalAssetCreation.setEditMode(true);
            ControlViews.VitalAssetCreation.setVitalAssetInfo(getmarker, getrect);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            main.detailMarkers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getmarker);
            main.markerOverlay.Markers.Remove(getrect);
            VitalAssetCreation.AutoIncrementID -= 1;
            MessageBox.Show("ลบมาร์คเกอร์ " + getmarker.Name + " จากแผนที่แล้ว", "VitalAsset Marker");
            clearField();
            ControlViews.Marker.SetControl(ControlViews.VitalAssetCreation);
        }
    }

}
