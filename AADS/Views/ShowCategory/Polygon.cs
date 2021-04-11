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
        private MainForm mainForm = MainForm.GetInstance();
        public Polygon()
        {
            InitializeComponent();
        }

        private void btnShowGeographic_Click(object sender, EventArgs e)
        {
            var Geographicpage = new Views.Filter_Geographic.main();
            panelShowDetail.Controls.Clear();
            panelShowDetail.Controls.Add(Geographicpage);
            mainForm.isPolygonFuncClicked = true;
        }

        private void btnShowRestrictedArea_Click(object sender, EventArgs e)
        {
            var RestrictedAreapage = new Views.RestrictedArea.main();
            panelShowDetail.Controls.Clear();
            panelShowDetail.Controls.Add(RestrictedAreapage);
            mainForm.isPolygonFuncClicked = true;
        }

        private void btnShowRD_Click(object sender, EventArgs e)
        {
            var ResourceDistributionpage = new Views.Filter_ResourceDistribution.main();
            panelShowDetail.Controls.Clear();
            panelShowDetail.Controls.Add(ResourceDistributionpage);
            mainForm.isPolygonFuncClicked = true;
        }
    }
}
