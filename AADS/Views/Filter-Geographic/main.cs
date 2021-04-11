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

namespace AADS.Views.Filter_Geographic
{
    public partial class main : UserControl
    {
        private static main instance;
        private static int count = 0;
        private MainForm mainForm = MainForm.GetInstance();
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            instance = this;
        }
        internal static main GetInstance()
        {
            return instance;
        }
        public void SetListBoxValue(List<PointLatLng> points)
        {
            listBox1.Items.Add(points[count].ToString());
            count++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.isPolygonFuncClicked = false;
            var polygonDict = Polygon.dictPolygons;
            polygonDict.Add(Polygon.GetPolygon(), Polygon.polygonData);
        }
    }
}
