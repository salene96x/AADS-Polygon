using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS.Views.ShowCategory
{
    public partial class Polygon : UserControl
    {
        public Polygon()
        {
            InitializeComponent();
        }
        private MainForm main = MainForm.GetInstance();
        public UserControl currentControl;
        public void SetControl(UserControl control)
        {
            currentControl = control;
            panelShowDetail.Controls.Clear();
            panelShowDetail.Controls.Add(currentControl);
        }
        private void btnShowGeographic_Click(object sender, EventArgs e)
        {
            SetControl(ControlViews.GeographicCreation);
            main.SetPolygonFuncClick(true);
            main.isGeoClicked = true;
            main.isRaClicked = false;
            main.isRdClicked = false;
        }

        private void btnShowRestrictedArea_Click(object sender, EventArgs e)
        {
            SetControl(ControlViews.RestrictedAreaCreation);
            main.SetPolygonFuncClick(true);
            main.isRaClicked = true;
            main.isRdClicked = false;
            main.isGeoClicked = false;
        }

        private void btnShowRD_Click(object sender, EventArgs e)
        {
            SetControl(ControlViews.ResourceCreation);
            main.SetPolygonFuncClick(true);
            main.isRdClicked = true;
            main.isRaClicked = false;
            main.isGeoClicked = false;
        }
    }
}
