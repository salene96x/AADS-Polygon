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

namespace AADS.Views.Track
{
    public partial class TrackTable : UserControl
    {
        MainForm main = MainForm.GetInstance();
        TrackData currentTrack;
        PointLatLng currentPoint;
        string speedScale;
        string bearingScale;
        string heightScale;
        public TrackTable()
        {
            InitializeComponent();
            cmbPosition.SelectedIndex = 0;
            cmbSpeed.SelectedIndex = 0;
            cmbBearing.SelectedIndex = 0;
            cmbHeight.SelectedIndex = 0;
            speedScale = cmbSpeed.SelectedItem.ToString();
            bearingScale = cmbBearing.SelectedItem.ToString();
            heightScale = cmbHeight.SelectedItem.ToString();
            txtPosition.Text = "";
        }

        private void TrackTable_Load(object sender, EventArgs e)
        {
            setTrackInfo(null);
        }
        public void GMap_MarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (item is GMarkerTrack trackMarker) {
                TrackData track = trackMarker.track;
                setTrackInfo(track);
            }
        }
        public void setTrackInfo(TrackData track)
        {
            currentTrack = track;
            if (track != null)
            {
                txtKey.Text = track.Key;
                txtPosition.Text = PositionConverter.ParsePointToString(track.Position, cmbPosition.Text);
                txtSpeed.Text = ScaleConverter.ConvertSpeed(track.Speed, "knots", cmbSpeed.Text).ToString();
                txtBearing.Text = ScaleConverter.ConvertBearing(track.Bearing, "degree", cmbBearing.Text).ToString();
                txtPosition.ReadOnly = false;
                txtSpeed.ReadOnly = false;
                txtBearing.ReadOnly = false;
                if (track.Height.HasValue)
                {
                    txtHeight.Text = ScaleConverter.ConvertHeight(track.Height.Value, "ft", cmbHeight.Text).ToString();
                    txtHeight.ReadOnly = false;
                }
                else
                {
                    txtHeight.Text = "Unknown";
                    txtHeight.ReadOnly = true;
                }
                lbScope.Text = track.Faker == TrackFakerType.Client ? "Client" : "Server";
            }
            else
            {
                txtPosition.ReadOnly = true;
                txtSpeed.ReadOnly = true;
                txtBearing.ReadOnly = true;
                txtKey.Text = "";
                txtPosition.Text = "";
                txtSpeed.Text = "";
                txtBearing.Text = "";
                txtHeight.Text = "";
                lbScope.Text = "";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentTrack == null)
            {
                
            }
            else if (currentTrack.Faker != TrackFakerType.None)
            {
                var key = currentTrack.Key;
                main.trackHandler.RemoveTrack(key, currentTrack.Faker);
                setTrackInfo(null);
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedIndex > -1 && txtPosition.Text != "")
            {
                txtPosition.Text = PositionConverter.ParsePointToString(currentPoint, cmbPosition.SelectedItem.ToString());
            }
        }

        private void cmbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newScale = cmbSpeed.SelectedItem.ToString();
            double speed;
            if (double.TryParse(txtSpeed.Text, out speed))
            {
                double newvalue = ScaleConverter.ConvertSpeed(speed, speedScale, newScale);
                txtSpeed.Text = newvalue.ToString();
            }
            speedScale = newScale;
        }

        private void cmbBearing_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newScale = cmbBearing.SelectedItem.ToString();
            double bearing;
            if (double.TryParse(txtBearing.Text, out bearing))
            {
                double newvalue = ScaleConverter.ConvertBearing(bearing, bearingScale, newScale);
                txtBearing.Text = newvalue.ToString();
            }
            bearingScale = newScale;
        }

        private void cmbHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newScale = cmbHeight.SelectedItem.ToString();
            double height;
            if (double.TryParse(txtHeight.Text, out height))
            {
                double newvalue = ScaleConverter.ConvertHeight(height, heightScale, newScale);
                txtHeight.Text = newvalue.ToString();
            }
            heightScale = newScale;
        }
    }
}
