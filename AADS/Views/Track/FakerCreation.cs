using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS.Views.Track
{
    public partial class FakerCreation : UserControl
    {
        MainForm main = MainForm.GetInstance();
        PointLatLng currentPoint;
        string speedScale;
        string bearingScale;
        string heightScale;
        public FakerCreation()
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
        private void FakerCreation_Load(object sender, EventArgs e)
        {

        }

        public void GMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng point = main.gMap.FromLocalToLatLng(e.X, e.Y);
            currentPoint = point;
            txtPosition.Text = PositionConverter.ParsePointToString(point, cmbPosition.SelectedItem.ToString());
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            PointLatLng point = PositionConverter.ParsePointFromString(txtPosition.Text);
            int number;
            double speed, bearing, height;
            bool server = rdbServer.Checked;
            if (point.IsEmpty)
            {

            }
            else if (!int.TryParse(txtNumber.Text, out number))
            {

            }
            else if (!double.TryParse(txtSpeed.Text, out speed))
            {

            }
            else if (!double.TryParse(txtBearing.Text, out bearing))
            {

            }
            else if (!double.TryParse(txtHeight.Text, out height))
            {

            }
            else
            {
                TrackData faker = new TrackData
                {
                    Source = RadarClient.Connected ? DataSource.Self : null,
                    Number = number,
                    Position = point,
                    Speed = ScaleConverter.ConvertSpeed(speed, speedScale, "knots"),
                    Bearing = ScaleConverter.ConvertBearing(bearing, bearingScale, "degree"),
                    Height = ScaleConverter.ConvertHeight(height, heightScale, "ft"),
                    LastUpdated = DateTime.Now
                };
                if (server && !RadarClient.Connected)
                {

                }
                else
                {
                    faker.Faker = server && RadarClient.Connected ? TrackFakerType.Server : TrackFakerType.Client;
                    TrackCommandResponse response = main.trackHandler.CreateTrack(faker);
                    if (response.Code == TrackCommandResponseCode.Success)
                    {
                        txtNumber.Text = "";
                        txtPosition.Text = "";
                        txtSpeed.Text = "";
                        txtBearing.Text = "";
                        txtHeight.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(response.Message);
                    }
                }
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
