using GMap.NET;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    class LandMark
    {
        private static MainForm mainInstance = MainForm.GetInstance();

        private String LandMarkName { get; set; }
        private String LandMarkLabel { get; set; }
        private String LandMarkID { get; set; }
        private PointLatLng LandmarkPoint { get; set; }
        private string LandMarkType { get; set; }
        private String LandMarkRemark { get; set; }

        public LandMark(string LandMarkID, string LandMarkName, string LandMarkLabel, PointLatLng LandmarkPoint, string LandMarkType, string LandMarkRemark)
        {
            this.LandMarkID = LandMarkID;
            this.LandMarkName = LandMarkName;
            this.LandMarkLabel = LandMarkLabel;
            this.LandmarkPoint = LandmarkPoint;
            this.LandMarkType = LandMarkType;
            this.LandMarkRemark = LandMarkRemark;
        }
        public String GetLandMarkID()
        {
            return LandMarkID;
        }
        public String GetLandMarkName()
        {
            return LandMarkName;
        }
        public String GetLandMarkLabel()
        {
            return LandMarkLabel;
        }
        public PointLatLng GetLandmarkPoint()
        {
            return LandmarkPoint;
        }
        public String GetLandMarkType()
        {
            return LandMarkType;
        }
        public String GetLandMarkRemark()
        {
            return LandMarkRemark;
        }
        public void Insert()
        {
            MainForm.com.Open();
            string sqlINS = "INSERT INTO landmark VALUES (@ID, @name, @label, @type)";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdINS.Parameters.AddWithValue("name", this.LandMarkName);
            cmdINS.Parameters.AddWithValue("label", this.LandMarkLabel);
            cmdINS.Parameters.AddWithValue("type", this.LandMarkType);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "INSERT INTO markers VALUES (@ID , @lat , @lng , @remark)";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdMarkers.Parameters.AddWithValue("lat", this.LandmarkPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.LandmarkPoint.Lng);
            cmdMarkers.Parameters.AddWithValue("remark", this.LandMarkRemark);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Edit()
        {
            MainForm.com.Open();
            string sqlINS = "UPDATE landmark SET landmark_name = @name , landmark_label = @label, landmark_type = @type WHERE landmark_id = @ID";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdINS.Parameters.AddWithValue("name", this.LandMarkName);
            cmdINS.Parameters.AddWithValue("label", this.LandMarkLabel);
            cmdINS.Parameters.AddWithValue("type", this.LandMarkType);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "UPDATE markers SET marker_point_lat = @lat , marker_point_lng = @lng , marker_remark = @remark WHERE marker_id = @ID";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("lat", this.LandmarkPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.LandmarkPoint.Lng);
            cmdMarkers.Parameters.AddWithValue("remark", this.LandMarkRemark);
            cmdMarkers.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Delete()
        {
            MainForm.com.Open();
            string sqlDel = "DELETE FROM landmark WHERE landmark_id = @ID";
            MySqlCommand cmdDel = new MySqlCommand(sqlDel, MainForm.com);
            cmdDel.Parameters.Clear();
            cmdDel.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdDel.ExecuteNonQuery();

            string sqlDelMarker = "DELETE FROM markers WHERE marker_id = @ID";
            MySqlCommand cmdDelMarker = new MySqlCommand(sqlDelMarker, MainForm.com);
            cmdDelMarker.Parameters.Clear();
            cmdDelMarker.Parameters.AddWithValue("ID", this.LandMarkID);
            cmdDelMarker.ExecuteNonQuery();
            MainForm.com.Close();
        }
    }
}
