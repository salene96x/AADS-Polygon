using GMap.NET;
using MySql.Data.MySqlClient;
using System;

namespace AADS
{
    public class City
    {
        private String CityID { get; set; }
        private String CityName { get; set; }
        public String CityLabel { get; set; }//don't know yet what type it is
        public String CityRemark { get; set; }
        public PointLatLng CityPoint { get; set; }
        
        public City(string CityID, string CityName, string CityLabel, string CityRemark, PointLatLng CityPoint)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CityRemark = CityRemark;
            this.CityLabel = CityLabel;
            this.CityPoint = CityPoint;
        }

        public String GetCityID()
        {
            return CityID;
        }

        public String GetCityName()
        {
            return CityName;
        }

        public String GetCityRemark()
        {
            return CityRemark;
        }

        public String GetCityLabel()
        {
            return CityLabel;
        }

        public PointLatLng GetCityPoint()
        {
            return CityPoint;
        }

        public void Insert()
        {
            MainForm.com.Open();
            string sqlINS = "INSERT INTO city VALUES (@ID , @name , @label)";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID",this.CityID);
            cmdINS.Parameters.AddWithValue("name", this.CityName);
            cmdINS.Parameters.AddWithValue("label", this.CityLabel);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "INSERT INTO markers VALUES (@ID , @lat , @lng , @remark)";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("ID", this.CityID);
            cmdMarkers.Parameters.AddWithValue("lat", this.CityPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.CityPoint.Lng);
            cmdMarkers.Parameters.AddWithValue("remark", this.CityRemark);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Edit()
        {
            MainForm.com.Open();
            string sqlINS = "UPDATE city SET city_name = @name , city_label = @label WHERE city_id = @ID";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("name", this.CityName);
            cmdINS.Parameters.AddWithValue("label", this.CityLabel);
            cmdINS.Parameters.AddWithValue("ID", this.CityID);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "UPDATE markers SET marker_point_lat = @lat , marker_point_lng = @lng , marker_remark = @remark WHERE marker_id = @ID";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("lat", this.CityPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.CityPoint.Lng);
            cmdMarkers.Parameters.AddWithValue("remark", this.CityRemark);
            cmdMarkers.Parameters.AddWithValue("ID", this.CityID);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Delete()
        {
            MainForm.com.Open();
            string sqlDel = "DELETE FROM city WHERE city_id = @ID";
            MySqlCommand cmdDel = new MySqlCommand(sqlDel, MainForm.com);
            cmdDel.Parameters.Clear();
            cmdDel.Parameters.AddWithValue("ID", this.CityID);
            cmdDel.ExecuteNonQuery();

            string sqlDelMarker = "DELETE FROM markers WHERE marker_id = @ID";
            MySqlCommand cmdDelMarker = new MySqlCommand(sqlDelMarker, MainForm.com);
            cmdDelMarker.Parameters.Clear();
            cmdDelMarker.Parameters.AddWithValue("ID", this.CityID);
            cmdDelMarker.ExecuteNonQuery();
            MainForm.com.Close();
        }
    }
}