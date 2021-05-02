using GMap.NET;
using GMap.NET.WindowsForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class Airport
    {
        private static MainForm mainInstance = MainForm.GetInstance();

        private String AirportCountry { get; set; }
        private String AirportName { get; set; }
        private int AirportInternational { get; set; }
        private String AirportICAO { get; set; }
        private String AirportIATA { get; set; }
        private String AirportID { get; set; }
        private PointLatLng AirportPoint { get; set; }

        public Airport(string AirportID, string AirportCountry, string AirportName, int AirportInternational, string AirportICAO, string AirportIATA, PointLatLng AirportPoint)
        {
            this.AirportCountry = AirportCountry;
            this.AirportName = AirportName;
            this.AirportInternational = AirportInternational;
            this.AirportICAO = AirportICAO;
            this.AirportIATA = AirportIATA;
            this.AirportID = AirportID;
            this.AirportPoint = AirportPoint;
        }
        public String GetCountry()
        {
            return AirportCountry;
        }
        public String GetName()
        {
            return AirportName;
        }
        public int GetAirportInternational()
        {
            return AirportInternational;
        }
        public String GetAirportICAO()
        {
            return AirportICAO;
        }
        public String GetAirportIATA()
        {
            return AirportIATA;
        }
        public PointLatLng GetAirportPoint()
        {
            return AirportPoint;
        }
        public String GetAirportID()
        {
            return AirportID;
        }
        public void Insert()
        {
            MainForm.com.Open();
            string sqlINS = "INSERT INTO airport SET "+
            "airport_id = @ID,"+
            "airport_iata = @IATA," +
            "airport_icao = @ICAO," +
            "airport_name = @Name," +
            "airport_country = (SELECT country_id FROM country WHERE country_name = @Country),"+
            "airport_is_international = @International";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.AirportID);
            cmdINS.Parameters.AddWithValue("IATA", this.AirportIATA);
            cmdINS.Parameters.AddWithValue("ICAO", this.AirportICAO);
            cmdINS.Parameters.AddWithValue("Name", this.AirportName);
            cmdINS.Parameters.AddWithValue("Country", this.AirportCountry);
            cmdINS.Parameters.AddWithValue("International", this.AirportInternational);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "INSERT INTO markers VALUES (@ID , @lat , @lng,null)";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("ID", this.AirportID);
            cmdMarkers.Parameters.AddWithValue("lat", this.AirportPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.AirportPoint.Lng);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Edit()
        {
            MainForm.com.Open();
            string sqlINS = "UPDATE airport SET " +
            "airport_iata = @IATA," +
            "airport_icao = @ICAO," +
            "airport_name = @Name," +
            "airport_country = (SELECT country_id FROM country WHERE country_name = @Country)," +
            "airport_is_international = @International "+
            "WHERE airport_id = @ID";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.AirportID);
            cmdINS.Parameters.AddWithValue("IATA", this.AirportIATA);
            cmdINS.Parameters.AddWithValue("ICAO", this.AirportICAO);
            cmdINS.Parameters.AddWithValue("Name", this.AirportName);
            cmdINS.Parameters.AddWithValue("Country", this.AirportCountry);
            cmdINS.Parameters.AddWithValue("International", this.AirportInternational);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "UPDATE markers SET marker_point_lat = @lat , marker_point_lng = @lng  WHERE marker_id = @ID";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("ID", this.AirportID);
            cmdMarkers.Parameters.AddWithValue("lat", this.AirportPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.AirportPoint.Lng);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Delete()
        {
            MainForm.com.Open();
            string sqlDel = "DELETE FROM airport WHERE airport_id = @ID";
            MySqlCommand cmdDel = new MySqlCommand(sqlDel, MainForm.com);
            cmdDel.Parameters.Clear();
            cmdDel.Parameters.AddWithValue("ID", this.AirportID);
            cmdDel.ExecuteNonQuery();

            string sqlDelMarker = "DELETE FROM markers WHERE marker_id = @ID";
            MySqlCommand cmdDelMarker = new MySqlCommand(sqlDelMarker,MainForm. com);
            cmdDelMarker.Parameters.Clear();
            cmdDelMarker.Parameters.AddWithValue("ID", this.AirportID);
            cmdDelMarker.ExecuteNonQuery();
            MainForm.com.Close();
        }
    }

}

