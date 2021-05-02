using GMap.NET;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    class VitalAsset
    {

        private static MainForm mainInstance = MainForm.GetInstance();

        private String VitalAssetID { get; set; }
        private String VitalAssetName { get; set; }
        private String VitalAssetType { get; set; }
        private int VitalAssetPriority { get; set; }
        private String VitalAssetProvince { get; set; }
        private String VitalAssetAssetSize { get; set; }
        private String VitalAssetUnitResponsible { get; set; }
        private String VitalAssetUnitStatus { get; set; }
        private String VitalAssetResponsePerson { get; set; }
        private PointLatLng VitalAssetPoint { get; set; }

        public VitalAsset(string VitalAssetID, string VitalAssetName, string VitalAssetType, int VitalAssetPriority, string VitalAssetProvince, string VitalAssetAssetSize, string VitalAssetUnitResponsible, string VitalAssetUnitStatus, string VitalAssetResponsePerson, PointLatLng VitalAssetPoint)
        {
            this.VitalAssetID = VitalAssetID;
            this.VitalAssetName = VitalAssetName;
            this.VitalAssetType = VitalAssetType;
            this.VitalAssetPriority = VitalAssetPriority;
            this.VitalAssetProvince = VitalAssetProvince;
            this.VitalAssetAssetSize = VitalAssetAssetSize;
            this.VitalAssetUnitResponsible = VitalAssetUnitResponsible;
            this.VitalAssetUnitStatus = VitalAssetUnitStatus;
            this.VitalAssetResponsePerson = VitalAssetResponsePerson;
            this.VitalAssetPoint = VitalAssetPoint;
        }
        public String GetVitalAssetID()
        {
            return VitalAssetID;
        }
        public String GetVitalAssetName()
        {
            return VitalAssetName;
        }
        public String GetVitalAssetType()
        {
            return VitalAssetType;
        }
        public String GetVitalAssetProvince()
        {
            return VitalAssetProvince;
        }
        public String GetVitalAssetUnitResponsible()
        {
            return VitalAssetUnitResponsible;
        }
        public PointLatLng GetVitalAssetPoint()
        {
            return VitalAssetPoint;
        }
        public String GetVitalAssetPriority()
        {
            return VitalAssetPriority.ToString();
        }
        public String GetVitalAssetAssetSize()
        {
            return VitalAssetAssetSize;
        }
        public String GetVitalAssetUnitStatus()
        {
            return VitalAssetUnitStatus;
        }
        public String GetVitalAssetResponsePerson()
        {
            return VitalAssetResponsePerson;
        }
        public void Insert()
        {
            MainForm.com.Open();
            string sqlINS = "INSERT INTO vitalasset SET" +
            "vital_id = @ID," +
            "vital_name = @name," +
            "vital_type  = @type," +
            "vital_priority   = @priority," +
            "FK_province_id  = (SELECT province_id FROM province WHERE province_name = @province)," +
            "vital_size  = @size," +
            "FK_unit_id   = (SELECT fireunit_id FROM fireunit WHERE fireunit_name = @unit)," +
            "vital_response_person    = @person";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdINS.Parameters.AddWithValue("name", this.VitalAssetName);
            cmdINS.Parameters.AddWithValue("priority", this.VitalAssetPriority);
            cmdINS.Parameters.AddWithValue("type", this.VitalAssetType);
            cmdINS.Parameters.AddWithValue("province", this.VitalAssetProvince);
            cmdINS.Parameters.AddWithValue("size", this.VitalAssetAssetSize);
            cmdINS.Parameters.AddWithValue("unit", this.VitalAssetUnitResponsible);
            cmdINS.Parameters.AddWithValue("person", this.VitalAssetResponsePerson);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "INSERT INTO markers VALUES (@ID , @lat , @lng , null)";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdMarkers.Parameters.AddWithValue("lat", this.VitalAssetPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.VitalAssetPoint.Lng);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Edit()
        {
            MainForm.com.Open();
            string sqlINS = "UPDATE vitalasset SET" +
            "vital_name = @name," +
            "vital_type  = @type," +
            "vital_priority   = @priority," +
            "FK_province_id  = (SELECT province_id FROM province WHERE province_name = @province)," +
            "vital_size  = @size," +
            "FK_unit_id  = (SELECT fireunit_id FROM fireunit WHERE fireunit_name = @unit)," +
            "vital_response_person    = @person" +
            "WHERE vital_id = @ID";
            MySqlCommand cmdINS = new MySqlCommand(sqlINS, MainForm.com);
            cmdINS.Parameters.Clear();
            cmdINS.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdINS.Parameters.AddWithValue("name", this.VitalAssetName);
            cmdINS.Parameters.AddWithValue("priority", this.VitalAssetPriority);
            cmdINS.Parameters.AddWithValue("type", this.VitalAssetType);
            cmdINS.Parameters.AddWithValue("province", this.VitalAssetProvince);
            cmdINS.Parameters.AddWithValue("size", this.VitalAssetAssetSize);
            cmdINS.Parameters.AddWithValue("unit", this.VitalAssetUnitResponsible);
            cmdINS.Parameters.AddWithValue("person", this.VitalAssetResponsePerson);
            cmdINS.ExecuteNonQuery();

            string sqlMarkers = "UPDATE markers SET marker_point_lat = @lat , marker_point_lng = @lng WHERE marker_id = @ID";
            MySqlCommand cmdMarkers = new MySqlCommand(sqlMarkers, MainForm.com);
            cmdMarkers.Parameters.Clear();
            cmdMarkers.Parameters.AddWithValue("lat", this.VitalAssetPoint.Lat);
            cmdMarkers.Parameters.AddWithValue("lng", this.VitalAssetPoint.Lng);
            cmdMarkers.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdMarkers.ExecuteNonQuery();

            MainForm.com.Close();
        }
        public void Delete()
        {
            MainForm.com.Open();
            string sqlDel = "DELETE FROM VitalAsset WHERE VitalAsset_id = @ID";
            MySqlCommand cmdDel = new MySqlCommand(sqlDel, MainForm.com);
            cmdDel.Parameters.Clear();
            cmdDel.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdDel.ExecuteNonQuery();

            string sqlDelMarker = "DELETE FROM markers WHERE marker_id = @ID";
            MySqlCommand cmdDelMarker = new MySqlCommand(sqlDelMarker, MainForm.com);
            cmdDelMarker.Parameters.Clear();
            cmdDelMarker.Parameters.AddWithValue("ID", this.VitalAssetID);
            cmdDelMarker.ExecuteNonQuery();
            MainForm.com.Close();
        }
    }
}
