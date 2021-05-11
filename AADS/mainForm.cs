using AADS.Views.Marker;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AADS
{
    public partial class MainForm : Form
    {
        //Event for get Marker
        public event EventHandler<GMapMarker> MarkerCurrent;

        internal readonly GMapOverlay markerOverlay = new GMapOverlay("markerOverlay");
        internal readonly GMapOverlay radarOverlay = new GMapOverlay("radarOverlay");
        internal readonly GMapOverlay trackOverlay = new GMapOverlay("trackOverlay");
        internal readonly GMapOverlay polygonOverlay = new GMapOverlay("polygonOverlay");
        internal readonly GMapOverlay previewOverlay = new GMapOverlay("previewOverlay");

        internal readonly GMapOverlay top = new GMapOverlay("top");

        internal readonly GMapOverlay minMapOverlay = new GMapOverlay("minMapOverlay");

        // marker detail key = marker, value = marker detail
        public Dictionary<GMarker, dynamic> detailMarkers = new Dictionary<GMarker, dynamic>();
 
        private Dictionary<string, GMarkerRect> radarMarkers = new Dictionary<string, GMarkerRect>();
        private Dictionary<string, GMarkerRect> trackMarkers = new Dictionary<string, GMarkerRect>();
        private Dictionary<string, GMarkerRect> markerMarkers = new Dictionary<string, GMarkerRect>();

        public GMapControl gMap;
        public TrackManager trackHandler = new TrackManager();
        public RadarManager radarHandler = new RadarManager();
        public ControlViews views;
        List<MapMode> mapModes = new List<MapMode>();
        public GMapMarker currentMarker;
        bool minMapAutoZoom = false;
        public static MySqlConnection com = new MySqlConnection("host=localhost;user=root;password=1234;database=aads");
        public static bool isConnected = true;
        public GMapOverlay GetOverlay(string Name)
        {
            return mainMap.Overlays.FirstOrDefault(x => x.Id == Name);
        }
        public static MainForm GetInstance()
        {
            return Instance;
        }
        public GMapControl GetmainMap()
        {
            return mainMap;
        }
        public static MainForm Instance;
        public void Invoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Invoke(action)));
            }
            else
            {
                action();
            }
        }
        private void readJsonMap()
        {
            using (StreamReader reader = new StreamReader("Resources/Maps.json"))
            {
                string json = reader.ReadToEnd();
                mapModes = JsonSerializer.Deserialize<List<MapMode>>(json);
            }
        }
        public static double DegreesToRadians(double degrees)
        {
            const double degToRadFactor = Math.PI / 180;
            return degrees * degToRadFactor;
        }
        public static double RadiansToDegrees(double radians)
        {
            const double radToDegFactor = 180 / Math.PI;
            return radians * radToDegFactor;
        }
        public static PointLatLng FindPointAtDistance(PointLatLng startPoint, double bearingDegree, double distanceKilometres)
        {
            double radius = 6371.01;

            var δ = distanceKilometres / radius; // angular distance in radians
            var θ = DegreesToRadians(bearingDegree);

            var φ1 = DegreesToRadians(startPoint.Lat);
            var λ1 = DegreesToRadians(startPoint.Lng);

            var sinφ2 = Math.Sin(φ1) * Math.Cos(δ) + Math.Cos(φ1) * Math.Sin(δ) * Math.Cos(θ);
            var φ2 = Math.Asin(sinφ2);
            var y = Math.Sin(θ) * Math.Sin(δ) * Math.Cos(φ1);
            var x = Math.Cos(δ) - Math.Sin(φ1) * sinφ2;
            var λ2 = λ1 + Math.Atan2(y, x);

            var lat = RadiansToDegrees(φ2);
            var lon = RadiansToDegrees(λ2);

            return new PointLatLng(lat, lon);
        }
        private void radarHandler_RadarClear()
        {
            radarHandler.Radars.ForEach(x =>
            {
                GMarkerRect rect = radarMarkers[x.Name];
                GMarkerRadar marker = rect.InnerMarker as GMarkerRadar;
                if (marker.IsRadiusShow)
                {
                    radarOverlay.Polygons.Remove(marker.RadiusPolygon);
                }
                radarOverlay.Markers.Remove(marker);
                radarOverlay.Markers.Remove(rect);
                rect.Dispose();
            });
            radarMarkers.Clear();
        }
        private void radarHandler_RadarCreate(RadarSite radar)
        {
            GMarkerRadar marker = new GMarkerRadar(radar);
            GMarkerRect rect = new GMarkerRect(marker);
            radarOverlay.Markers.Add(marker);
            radarOverlay.Markers.Add(rect);
            radarMarkers.Add(radar.Name, rect);
        }
        private void radarHandler_RadarUpdate(RadarSite radar)
        {
            GMarkerRect rect = radarMarkers[radar.Name];
            GMarkerRadar marker = rect.InnerMarker as GMarkerRadar;
            radarOverlay.Polygons.Remove(marker.RadiusPolygon);
            marker.RenewRadius();
            if (marker.IsRadiusShow)
            {
                radarOverlay.Polygons.Add(marker.RadiusPolygon);
            }
            rect.SetPosition(radar.Position);
            mainMap.Invalidate();
        }
        private void radarHandler_RadarRemove(RadarSite radar)
        {
            GMarkerRect rect = radarMarkers[radar.Name];
            GMarkerRadar marker = rect.InnerMarker as GMarkerRadar;
            if (marker.IsRadiusShow)
            {
                radarOverlay.Polygons.Remove(marker.RadiusPolygon);
            }
            trackOverlay.Markers.Remove(marker);
            trackOverlay.Markers.Remove(rect);
            rect.Dispose();
            radarMarkers.Remove(radar.Name);
        }
        private void trackHandler_TrackClear()
        {
            trackHandler.Tracks.ForEach(x =>
            {
                GMarkerRect rect = trackMarkers[x.Key];
                GMarkerTrack marker = rect.InnerMarker as GMarkerTrack;
                trackOverlay.Markers.Remove(marker);
                trackOverlay.Markers.Remove(rect);
                rect.Dispose();
            });
            trackMarkers.Clear();
        }
        private void trackHandler_TrackCreate(TrackData item)
        {
            MessageBox.Show("Track Event Add");
            GMarkerTrack marker = new GMarkerTrack(item);
            GMarkerRect rect = new GMarkerRect(marker);
            trackOverlay.Markers.Add(marker);
            trackOverlay.Markers.Add(rect);
            trackMarkers.Add(item.Key, rect);
        }
        private void trackHandler_TrackUpdate(TrackData item)
        {
            GMarkerRect rect = trackMarkers[item.Key];
            GMarkerTrack marker = rect.InnerMarker as GMarkerTrack;
            rect.SetPosition(item.Position);
            marker.Track = item;
            mainMap.Invalidate();
        }
        private void trackHandler_TrackRemove(TrackData item)
        {
            GMarkerRect rect = trackMarkers[item.Key];
            GMarkerTrack marker = rect.InnerMarker as GMarkerTrack;
            trackOverlay.Markers.Remove(marker);
            trackOverlay.Markers.Remove(rect);
            markerOverlay.Markers.Remove(marker);
            rect.Dispose();
            trackMarkers.Remove(item.Key);
        }
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            gMap = mainMap;
            views = ControlViews.Instance;
            mainMap.Manager.BoostCacheEngine = true;
            mainMap.MapScaleInfoEnabled = true;
            mainMap.MapScaleInfoPosition = MapScaleInfoPosition.Bottom;

            readJsonMap();

            radarHandler.OnRadarClear += radarHandler_RadarClear;
            radarHandler.OnRadarCreate += radarHandler_RadarCreate;
            radarHandler.OnRadarUpdate += radarHandler_RadarUpdate;
            radarHandler.OnRadarRemove += radarHandler_RadarRemove;

            trackHandler.OnTrackClear += trackHandler_TrackClear;
            trackHandler.OnTrackCreate += trackHandler_TrackCreate;
            trackHandler.OnTrackUpdate += trackHandler_TrackUpdate;
            trackHandler.OnTrackRemove += trackHandler_TrackRemove;

            if (!GMapControl.IsDesignerHosted)
            {
                // Secondary overlay
                mainMap.Overlays.Add(markerOverlay);
                mainMap.Overlays.Add(radarOverlay);
                mainMap.Overlays.Add(trackOverlay);
                // First overlay
                mainMap.Overlays.Add(top);

                minMap1.Overlays.Add(minMapOverlay);

                mainMap.Manager.Mode = AccessMode.ServerOnly;
                mainMap.MapProvider = mapModes[0].MapProvider;
                mainMap.Position = new PointLatLng(13.7563, 100.5018);
                mainMap.MinZoom = mapModes[0].MainMap.MinZoom;
                mainMap.MaxZoom = mapModes[0].MainMap.MaxZoom;
                mainMap.Zoom = mapModes[0].MainMap.Default;

                minMap1.Manager.Mode = AccessMode.ServerOnly;
                minMap1.MapProvider = mapModes[0].MapProvider;
                minMap1.Position = new PointLatLng(13.7563, 100.5018);
                minMap1.MinZoom = mapModes[0].MiniMap.MinZoom;
                minMap1.MaxZoom = mapModes[0].MiniMap.MaxZoom;
                minMap1.Zoom = mapModes[0].MiniMap.Default;

                {
                    mainMap.OnPositionChanged += new PositionChanged(mainMap_OnPositionChanged);

                    //mainMap.OnMapZoomChanged += new MapZoomChanged(mainMap_OnMapZoomChanged);
                    mainMap.OnMapTypeChanged += new MapTypeChanged(mainMap_OnMapTypeChanged);

                    mainMap.MouseUp += new MouseEventHandler(mainMap_MouseUp);
                    mainMap.MouseDown += new MouseEventHandler(mainMap_MouseDown);
                    mainMap.MouseMove += new MouseEventHandler(mainMap_MouseMove);
                    mainMap.MouseClick += new MouseEventHandler(mainMap_MouseClick);

                    mainMap.OnMarkerClick += new MarkerClick(mainMap_OnMarkerClick);
                    mainMap.OnMarkerEnter += new MarkerEnter(mainMap_OnMarkerEnter);
                    mainMap.OnMarkerLeave += new MarkerLeave(mainMap_OnMarkerLeave);

                    mainMap.OnPolygonClick += new PolygonClick(mainMap_OnPolygonClick);
                }

                currentMarker = new GMarkerGoogle(mainMap.Position, GMarkerGoogleType.arrow);
                currentMarker.IsHitTestVisible = false;
                top.Markers.Add(currentMarker);
                //Console.WriteLine(CPC.Intersection(new PointLatLng(51.8853, 0.2545), new PointLatLng(49.0034, 2.5735), 108.547, 32.435).ToString());
                //Console.WriteLine(CPC.destinationPoint(new PointLatLng(51.127, 1.338), 40300, 116.7));
                //Console.WriteLine(CPC.rhumbBearingTo(new PointLatLng(51.127, 1.338),new PointLatLng(50.964, 1.853)).ToString());
            }
        }
        private void updateMinMap()
        {
            minMap1.Position = mainMap.Position;
            List<PointLatLng> plist = new List<PointLatLng>();
            int width = mainMap.Size.Width - 1;
            int height = mainMap.Size.Height - 1;
            plist.Add(mainMap.FromLocalToLatLng(0, 0));
            plist.Add(mainMap.FromLocalToLatLng(width, 0));
            plist.Add(mainMap.FromLocalToLatLng(width, height));
            plist.Add(mainMap.FromLocalToLatLng(0, height));
            GMapPolygon poly = new GMapPolygon(plist, "");
            poly.Fill = Brushes.Transparent;
            poly.Stroke = new Pen(Brushes.Red, 1.6f);
            minMapOverlay.Polygons.Clear();
            minMapOverlay.Polygons.Add(poly);
            if (minMapAutoZoom)
            {
                int minMapArea = minMap1.Size.Height * minMap1.Size.Width;
                long dX = minMap1.FromLatLngToLocal(poly.Points[1]).X - minMap1.FromLatLngToLocal(poly.Points[0]).X;
                long dY = minMap1.FromLatLngToLocal(poly.Points[2]).Y - minMap1.FromLatLngToLocal(poly.Points[1]).Y;
                long area = dX * dY;
                double ratio = (double)area / minMapArea;
                if (ratio < 0.2)
                {
                    minMap1.Zoom++;
                }
                else if (ratio > 0.8)
                {
                    minMap1.Zoom--;
                }
            }
        }

        private void mainMap_OnPositionChanged(PointLatLng point)
        {
            updateMinMap();
        }

        private void mainMap_OnMapTypeChanged(GMapProvider type)
        {

        }

        private void mainMap_OnMapZoomChanged()
        {
            updateMinMap();
        }
        private void mainMap_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMarkerRect rect)
            {
                if (rect.InnerMarker is GMarkerRadar radar)
                {
                    if (!radar.IsRadiusShow)
                    {
                        radar.IsRadiusShow = true;
                        radarOverlay.Polygons.Add(radar.RadiusPolygon);
                    }
                }
            }
        }
        private void mainMap_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMarkerRect rect)
            {
                if (rect.InnerMarker is GMarkerRadar radar)
                {
                    if (!radar.LockPolygon && radar.IsRadiusShow)
                    {
                        radar.IsRadiusShow = false;
                        radarOverlay.Polygons.Remove(radar.RadiusPolygon);
                    }
                }
            }
        }
        GMapMarker alternateMarker;
        private void mainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            alternateMarker = item;
            if (item is GMarkerRect rect)
            {
                if (rect.InnerMarker is GMarkerRadar radar)
                {
                    radar.LockPolygon = !radar.LockPolygon;
                }
                else if (rect.InnerMarker is GMarkerTrack track)
                {
                    if (!hasPanelRight)
                    {
                        MessageBox.Show("1");
                        panelRight_Map();
                        panelRightShowControl(ControlViews.Track);
                        ControlViews.Track.SetControl(ControlViews.TrackView);
                        ControlViews.TrackView.setTrackInfo(track.Track);
                        fadePanelRight();
                    }
                    else if (ControlViews.Main.currentControl == ControlViews.Track)
                    {
                        MessageBox.Show("2");
                        ControlViews.Track.SetControl(ControlViews.TrackView);
                        ControlViews.TrackView.setTrackInfo(track.Track);
                    }
                }
                else if (rect.InnerMarker is GMarker marker)
                {
                    // TODO : Request right panel and show this marker data
                    // See above else if condition for example
                    if(detailMarkers.ContainsKey(marker))
                    {
                        if (detailMarkers[marker].GetType() == typeof(City))
                        {
                            ControlViews.Marker.SetControl(ControlViews.CityView);
                            ControlViews.CityView.setCityInfo(marker, rect);
                        }
                        else if (detailMarkers[marker].GetType() == typeof(Airport))
                        {
                            //TODO : SetControl Here (Titan to Toon)
                            //.Show("Airport View", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ControlViews.Marker.SetControl(ControlViews.AirportView);
                            ControlViews.AirportView.setAirportInfo(marker, rect);
                        }
                        else if (detailMarkers[marker].GetType() == typeof(FireUnit))
                        {
                            //TODO : SetControl Here (Titan to Toon)
                            //MessageBox.Show("FireUnit View", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ControlViews.Marker.SetControl(ControlViews.FireUnitView);
                            ControlViews.FireUnitView.setFireUnitInfo(marker, rect);
                        }
                        else if (detailMarkers[marker].GetType() == typeof(FixedPoint))
                        {
                            //TODO : SetControl Here (Titan to Toon)
                            //MessageBox.Show("FixedPoint View", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ControlViews.Marker.SetControl(ControlViews.FixedPointView);
                            ControlViews.FixedPointView.setFixedPointInfo(marker, rect);
                        }
                        else if (detailMarkers[marker].GetType() == typeof(LandMark))
                        {
                            //TODO : SetControl Here (Titan to Toon)
                            //MessageBox.Show("LandMark View", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ControlViews.Marker.SetControl(ControlViews.LandmarkView);
                            ControlViews.LandmarkView.setLandMarkInfo(marker, rect);
                        }
                        else if (detailMarkers[marker].GetType() == typeof(VitalAsset))
                        {
                            //TODO : SetControl Here (Titan to Toon)
                            //MessageBox.Show("ViralAsset View", "TEST", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ControlViews.Marker.SetControl(ControlViews.VitalAssetView);
                            ControlViews.VitalAssetView.setVitalAssetInfo(marker, rect);
                        }
                        else
                        {
                            //This condition to other error case
                            MessageBox.Show("Invalid Data", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        
                        //markerOverlay.Markers.Remove(rect);
                        //markerOverlay.Markers.Remove(marker);
                    }
                }
            }

            MarkerCurrent?.Invoke(this, item);
        }
        private void mainMap_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var polygonManager = new ObjectsManager.PolygonManager();
                var polygonCollectionManagerWrap = Activator.CreateInstance(null, "AADS.ObjectsManager.PolygonCollectionManager");
                var polygonCollectionManager = (ObjectsManager.PolygonCollectionManager)polygonCollectionManagerWrap.Unwrap();
                Debug.WriteLine("Click on Polygon ID " + (string) polygonCollectionManager.FindId(item));
                if ((string) polygonCollectionManager.FindId(item).Substring(0,2) == "ra")
                {
                    this.isRaClicked = true;
                }
                else if ((string) polygonCollectionManager.FindId(item).Substring(0, 2) == "rd")
                {
                    this.isRdClicked = true;
                }
                else if ((string) polygonCollectionManager.FindId(item).Substring(0, 2) == "geo")
                {
                    this.isGeoClicked = true;
                }
                if (isRdClicked)
                {
                    var rdCreation = Views.Polygon.ResourceCreation.GetInstance();
                    rdCreation.SetPolygon(item);
                }
                polygonManager.View(item);
            }
        }
        bool isMouseDown = false;
        bool isRightClick = false;
        Point lastLocation;
        void mainMap_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        void mainMap_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            isRightClick = e.Button == MouseButtons.Right;
        }

        void moveCurrentMarker(PointLatLng point)
        {

            currentMarker.Position = point;
            //MessageBox.Show(currentMarker.Position.ToString());
        }
        void mainMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointLatLng pnew = mainMap.FromLocalToLatLng(e.X, e.Y);
            lastLocation = e.Location;
            if (isMouseDown && !isRightClick)
            {
                moveCurrentMarker(pnew);
                alternateMarker.Position = pnew;
            }
        }

        public static bool isPolygonFuncClicked { get; set; } = false;
        public void SetPolygonFuncClick(bool isClicked)
        {
            isPolygonFuncClicked = isClicked;
        }
        public bool isRdClicked { get; set; } = false;
        public bool isGeoClicked { get; set; } = false;
        public bool isRaClicked { get; set; } = false;
        public List<PointLatLng> _pointsPoly = new List<PointLatLng>();
        void mainMap_MouseClick(object sender, MouseEventArgs e)
        {
            PointLatLng pnew = mainMap.FromLocalToLatLng(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                moveCurrentMarker(pnew);
            }
            if (isPolygonFuncClicked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    var polygonManager = new ObjectsManager.PolygonManager();
                    _pointsPoly.Add(mainMap.FromLocalToLatLng(e.X, e.Y));
                    polygonManager.Preview(_pointsPoly);
                }

            }
        }

        private void updateCmbMapMode()
        {
            cmbMapMode.Items.Clear();
            foreach (MapMode mapMode in mapModes)
            {
                cmbMapMode.Items.Add(mapMode.Name);
            }
        }
        private Point label27Location;

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1600, 900);
            timeNow.Start();
            timerCheckConnection.Start();
            timerFakerSimulate.Start();
            updateMinMap();
            updateCmbMapMode();
            cmbMapMode.SelectedIndex = 0;
            panelRight.Height = this.Height - panelControl.Height - panelTop.Height - panelBottom.Height;
            panelRight.Location = new Point(1950,93);
            label27Location = new Point(this.Width - label27.Width, label27.Location.Y);
            mainMap.Overlays.Add(previewOverlay);
            mainMap.Overlays.Add(polygonOverlay);
            radarHandler.Create(new RadarSite
            {
                Name = "TRML",
                Type = RadarSiteType.TRML,
                Position = new PointLatLng(14.94561195, 102.0929003),
                Radius = 240
            });
            radarHandler.Create(new RadarSite
            {
                Name = "DR172ADV",
                Type = RadarSiteType.DR172ADV,
                Position = new PointLatLng(14.2255556, 100.7208333),
                Radius = 240
            });

            
                try
                {
                    com.Open();
                    com.Close();
                }
                catch (MySqlException)
                {
                    isConnected = false;
                }
            if (isConnected) { 
            com.Open();
            string sql = "SELECT max(airport_id) FROM airport " +
                "UNION SELECT max(city_id) FROM city " +
                "UNION SELECT max(fireunit_id) FROM fireunit " +
                "UNION SELECT max(fp_id) FROM fixedpoint" +
                " UNION SELECT max(landmark_id) FROM landmark " +
                "UNION SELECT max(vital_id) FROM vitalasset; ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, com);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        string str = item.ToString();
                        if (str != "")
                        {
                            if (str.Substring(0, 2) == "AP")
                            {
                                AirportCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                            else if (str.Substring(0, 2) == "CI")
                            {
                                CityCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                            else if (str.Substring(0, 2) == "FU")
                            {
                                FireUnitCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                            else if (str.Substring(0, 2) == "FP")
                            {
                                FixedPointCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                            else if (str.Substring(0, 2) == "LM")
                            {
                                LandmarkCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                            else if (str.Substring(0, 2) == "VA")
                            {
                                VitalAssetCreation.AutoIncrementID = Int32.Parse(str.Substring(2));
                            }
                        }  
                    }
                }
            }
            string sqlGet = "SELECT * FROM markers";
            MySqlDataAdapter adapterGet = new MySqlDataAdapter(sqlGet, com);
            DataSet dataSet1 = new DataSet();
            adapterGet.Fill(dataSet1);
            foreach (DataTable table in dataSet1.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row[0].ToString();
                    PointLatLng point = PositionConverter.ParsePointFromString(row[1].ToString() + "," + row[2].ToString());
                    if (str.Substring(0, 2) == "AP")
                    {
                        string sqlGetMK = "SELECT * FROM airport INNER JOIN country ON airport_country = country_id WHERE airport_id = @ID";
                        MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        cmd.Parameters.AddWithValue("ID", str);
                        MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        while (mySqlDataReader.Read())
                        {
                            Image image = Image.FromFile("Images/icon/Airport.png");
                            GMarker marker = new GMarker(point, mySqlDataReader["airport_name"].ToString(), image);
                            GMarkerRect rect = new GMarkerRect(marker);
                            Airport airport = new Airport(mySqlDataReader["airport_id"].ToString(), mySqlDataReader["country_name"].ToString(), mySqlDataReader["airport_name"].ToString(),
                                mySqlDataReader["airport_is_international"].ToString() == "true" ? 1 : 0, mySqlDataReader["airport_icao"].ToString(), mySqlDataReader["airport_iata"].ToString(), point);
                            detailMarkers.Add(marker, airport);
                            markerOverlay.Markers.Add(marker);
                            markerOverlay.Markers.Add(rect);
                        }
                        mySqlDataReader.Close();
                       
                    }
                    else if (str.Substring(0, 2) == "CI")
                    {
                        string sqlGetMK = "SELECT * FROM city WHERE city_id = @ID";
                        MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        cmd.Parameters.AddWithValue("ID", str);
                        MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        while (mySqlDataReader.Read())
                        {
                            Image image = Image.FromFile("Images/icon/City.png");
                            GMarker marker = new GMarker(point, mySqlDataReader["city_name"].ToString(), image);
                            GMarkerRect rect = new GMarkerRect(marker);
                            City city = new City(mySqlDataReader["city_id"].ToString(), mySqlDataReader["city_name"].ToString(), mySqlDataReader["city_label"].ToString(), 
                                row[3].ToString(),point);
                            detailMarkers.Add(marker, city);
                            markerOverlay.Markers.Add(marker);
                            markerOverlay.Markers.Add(rect);
                        }
                        mySqlDataReader.Close();
                    }
                    else if (str.Substring(0, 2) == "FU")
                    {
                        // =============== NOT DONE YET ================
                        //  string sqlGetMK = "SELECT * FROM fireunit INNER JOIN weaponbattery ON weaponbattery_id = weaponbattery.wb_id WHERE fireunit_id = @ID";
                        // MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        // cmd.Parameters.AddWithValue("ID", str);
                        // MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        // while (mySqlDataReader.Read())
                        // {
                        //     Image image = Image.FromFile("Images/icon/FireUnit.png");
                        //     GMarker marker = new GMarker(point, mySqlDataReader["fireunit_id"].ToString(), image);
                        //     GMarkerRect rect = new GMarkerRect(marker);
                        //     City city = new City(mySqlDataReader["city_id"].ToString(), mySqlDataReader["city_name"].ToString(), mySqlDataReader["city_label"].ToString(), 
                        //         row[3].ToString(),point);
                        //     detailMarkers.Add(marker, city);
                        //     markerOverlay.Markers.Add(marker);
                        //     markerOverlay.Markers.Add(rect);
                        // }
                        // mySqlDataReader.Close();
                    }
                    else if (str.Substring(0, 2) == "FP")
                    {
                        // =============== NOT DONE YET ================
                        // string sqlGetMK = "SELECT * FROM fixedpoint WHERE fixedpoint_id = @ID";
                        // MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        // cmd.Parameters.AddWithValue("ID", str);
                        // MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        // while (mySqlDataReader.Read())
                        // {
                        //     Image image = Image.FromFile("Images/icon/FireUnit.png");
                        //     GMarker marker = new GMarker(point, mySqlDataReader["fireunit_id"].ToString(), image);
                        //     GMarkerRect rect = new GMarkerRect(marker);
                        //     City city = new City(mySqlDataReader["city_id"].ToString(), mySqlDataReader["city_name"].ToString(), mySqlDataReader["city_label"].ToString(),
                        //         row[3].ToString(), point);
                        //     detailMarkers.Add(marker, city);
                        //     markerOverlay.Markers.Add(marker);
                        //     markerOverlay.Markers.Add(rect);
                        // }
                        // mySqlDataReader.Close();
                    }
                    else if (str.Substring(0, 2) == "LM")
                    {
                        string sqlGetMK = "SELECT * FROM landmark WHERE landmark_id = @ID";
                        MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        cmd.Parameters.AddWithValue("ID", str);
                        MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        while (mySqlDataReader.Read())
                        {
                            Image image = null;
                            if (mySqlDataReader["landmark_type"].ToString() == "พระราชวัง")
                            {
                                image = Image.FromFile("Images/landmark/040-royal palace.png");
                            }
                            else if (mySqlDataReader["landmark_type"].ToString() == "วัด")
                            {
                                image = Image.FromFile("Images/landmark/039-temple.png");
                            }
                            else if (mySqlDataReader["landmark_type"].ToString() == "สถานีตำรวจ")
                            {
                                image = Image.FromFile("Images/landmark/026-police station.png");
                            }
                            else if (mySqlDataReader["landmark_type"].ToString() == "โรงพยาบาล")
                            {
                                image = Image.FromFile("Images/landmark/014-hospital.png");
                            }
                            else if (mySqlDataReader["landmark_type"].ToString() == "โรงเรียน / มหาวิทยาลัย")
                            {
                                image = Image.FromFile("Images/landmark/011-education.png");
                            }
                            
                            GMarker marker = new GMarker(point, mySqlDataReader["landmark_label"].ToString(), image);
                            GMarkerRect rect = new GMarkerRect(marker);
                            LandMark landMark = new LandMark(mySqlDataReader["landmark_id"].ToString(), mySqlDataReader["landmark_name"].ToString(), mySqlDataReader["landmark_label"].ToString(),
                               point, mySqlDataReader["landmark_type"].ToString(), row[3].ToString());
                            detailMarkers.Add(marker, landMark);
                            markerOverlay.Markers.Add(marker);
                            markerOverlay.Markers.Add(rect);
                        }
                        mySqlDataReader.Close();
                    }
                    else if (str.Substring(0, 2) == "VA")
                    {
                        string sqlGetMK = "SELECT * FROM vitalasset INNER JOIN province ON FK_province_id = province_id INNER JOIN fireunit ON FK_unit_id = fireunit_id WHERE vital_id = @ID";
                        MySqlCommand cmd = new MySqlCommand(sqlGetMK, com);
                        cmd.Parameters.AddWithValue("ID", str);
                        MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                        while (mySqlDataReader.Read())
                        {
                            Image image = Image.FromFile("Images/icon/VitalAsset.png");
                            GMarker marker = new GMarker(point, mySqlDataReader["vital_name"].ToString(), image);
                            GMarkerRect rect = new GMarkerRect(marker);
                            VitalAsset vitalAsset = new VitalAsset(mySqlDataReader["vital_id"].ToString(), mySqlDataReader["vital_name"].ToString(), mySqlDataReader["vital_type"].ToString(),
                                 Int32.Parse(mySqlDataReader["vital_priority"].ToString()), mySqlDataReader["province_name"].ToString(), mySqlDataReader["vital_size"].ToString(),
                                  mySqlDataReader["FK_unit_id"].ToString(), mySqlDataReader["fireunit_status"].ToString(), mySqlDataReader["vital_responses_person"].ToString(), point);
                            detailMarkers.Add(marker, vitalAsset);
                            markerOverlay.Markers.Add(marker);
                            markerOverlay.Markers.Add(rect);
                        }
                        mySqlDataReader.Close();
                    }
                }
            }
            com.Close(); 
            }
        }
        private void closeBox_Click(object sender, EventArgs e)
        {
            timerClose.Start();
        }

        private void maximizeBox_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
            label27Location = new Point(this.Width - label27.Width, label27.Location.Y);
        }

        private void minimizeBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timeNow_Tick(object sender, EventArgs e)
        {
            DateTime Date = DateTime.Now;
            dateLabel.Text = Date.ToString("dd-MMM-yyyy");
            time_label.Text = Date.ToString("HH:mm:ss");
            labelSetTRKS.Text = trackHandler.Tracks.Count.ToString();
        }

        private void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbMapMode.SelectedIndex;
            MapMode mode = mapModes[index];
            if (mainMap.MapProvider != mode.MapProvider)
            {
                mainMap.MapProvider = mode.MapProvider;
                minMap1.MapProvider = mode.MapProvider;
                mainMap.MinZoom = mode.MainMap.MinZoom;
                mainMap.MaxZoom = mode.MainMap.MaxZoom;
                mainMap.Zoom = mode.MainMap.Default;
                minMap1.MinZoom = mode.MiniMap.MinZoom;
                minMap1.MaxZoom = mode.MiniMap.MaxZoom;
                minMap1.Zoom = mode.MiniMap.Default;
            }
        }
        public bool hasPanelRight = false;
        private void label27_Click(object sender, EventArgs e)
        {
            fadePanelRight();
        }
        public void fadePanelRight()
        {
            timer1.Start();
        }

        public UserControl currentControl;
        public void panelRightShowControl(UserControl userControl)
        {
            currentControl = userControl;
            panelRightShow.Controls.Clear();
            panelRightShow.Controls.Add(userControl);
        }
        private void btnShow_Marker_Click(object sender, EventArgs e)
        {
            panelRightShowControl(ControlViews.Marker);
        }

        private void btnShow_Line_Click(object sender, EventArgs e)
        {
            panelRightShowControl(ControlViews.Line);
        }
        private void btnShow_Polygon_Click(object sender, EventArgs e)
        {
            panelRightShowControl(ControlViews.Polygon);
        }
        private void btnShow_Track_Click(object sender, EventArgs e)
        {
            panelRightShowControl(ControlViews.Track);
            ControlViews.TrackCreation.setEditMode(false);
            ControlViews.TrackCreation.clearFields();
            ControlViews.Track.SetControl(ControlViews.TrackCreation);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = panelRight.Location.X;
            int y = 93;
            if (hasPanelRight)
            {
                x = x + (panelRight.Width / 14);
                label27.Location = new Point(x, y);
                panelRight.Location = new Point(x, y);
                if (x >= this.Width)
                {
                    timer1.Stop();
                    hasPanelRight = false;
                    label27.Text = "<<<";
                    label27.Location = label27Location;
                }
            }
            else
            {
                x = x - (panelRight.Width / 14);
                label27.Location = new Point(x - label27.Width, y);
                panelRight.Location = new Point(x, y);
                if (x <= this.Width - panelRight.Width + 14)
                {
                    timer1.Stop();
                    hasPanelRight = true;
                    label27.Text = ">>>";
                }
            }
        }

        
        private void timerOpen_(object sender, EventArgs e)
        {
            Opacity += 0.05;
            if (Opacity >= 1)
            {
                timerOpen.Stop();
            }
        }
        private void timerClose_(object sender, EventArgs e)
        {
            Opacity -= 0.05;
            if (Opacity <= 0)
            {
                Application.Exit();
                timerOpen.Stop();
            }

        }
        private void panelRight_Map()
        {
            if (!panelRightMap.Visible)
                panelRightShow.Controls.Clear();
            panelRightMap.Visible = true;
            panelRightUnit.Visible = false;
        }
        private void panelRight_Unit()
        {
            if (panelRightMap.Visible)
                panelRightShow.Controls.Clear();
            panelRightMap.Visible = false;
            panelRightUnit.Visible = true;
        }
        private void btnUnit_Click(object sender, EventArgs e)
        {
            panelRight_Unit();
        }
        private void btnMap_Click(object sender, EventArgs e)
        {
            panelRight_Map();
        }
        private void timerFakerSimulate_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            foreach (var faker in trackHandler.Tracks)
            {
                PointLatLng point = faker.Position;
                TimeSpan ts = dt - faker.LastUpdated;
                double time = ts.TotalSeconds;
                double speed = faker.Speed / 3600;
                double bearing = faker.Bearing < 0 ? 360 + faker.Bearing : faker.Bearing;
                double distance = speed * time * 1.852;
                faker.Position = FindPointAtDistance(point, bearing, distance);
                faker.LastUpdated = dt;
                trackHandler.Update(faker);
            }
        }
        private Point lastPoint;
        private bool panelControl_isMouseDown = false;

        private void panelControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void panelControl_MouseUp(object sender, MouseEventArgs e)
        {
            panelControl_isMouseDown = true;
        }

        private void panelControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && panelControl_isMouseDown)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panelControl_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            panelControl_isMouseDown = true;
        }

        private void chbHitbox_CheckedChanged(object sender, EventArgs e)
        {
            GMarkerRect.EnableHitbox = chbHitbox.Checked;
            mainMap.Refresh();
        }

        private void mainMap_OnMarkerClick_1(GMapMarker item, MouseEventArgs e)
        {

        }
    }
}
