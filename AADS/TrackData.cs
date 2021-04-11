using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AADS
{
    public enum TrackFakerType
    {
        None, Server, Client
    }
    public class TrackData
    {
        public string Key
        {
            get
            {
                if (Source != null && Source.IsServer)
                {
                    return Radar.Name + Number.ToString("000") + (Faker == TrackFakerType.None ? "A" : "X");
                }
                else
                {
                    return "AADS" + Number.ToString("000") + "X";
                }
            }
        }
        public DataSource Source { get; set; }
        public RadarSite Radar { get; set; }
        public TrackFakerType Faker { get; set; }
        public int Number { get; set; }
        public PointLatLng Position { get; set; }
        public double Speed { get; set; }
        public double Bearing { get; set; }
        public double? Height { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
