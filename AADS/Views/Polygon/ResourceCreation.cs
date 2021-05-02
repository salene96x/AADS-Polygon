using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS.Views.Polygon
{
    public partial class ResourceCreation : UserControl
    {
        private ObjectsManager.PolygonManager polygonManager = ObjectsManager.PolygonManager.GetInstance();
        public ResourceCreation()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            polygonManager.CreatePolygon(polygonManager.GetPoints());
        }
    }
}
