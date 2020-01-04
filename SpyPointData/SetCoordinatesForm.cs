using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap;

namespace SpyPointData
{
    public partial class SetCoordinatesForm : Form
    {
        public double Latitude;
        public double Longitude;
        public SetCoordinatesForm()
        {
            InitializeComponent();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(38.0323, -89.5657);

            gMapControl1.ShowCenter = true;
        }

        private void SetCoordinatesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Latitude = gMapControl1.Position.Lat;
            Longitude = gMapControl1.Position.Lng;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
