using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class DataSource
    {
        public static DataSource Self
        {
            get => new DataSource(ClientRegistration.Registration.Address, ClientRegistration.Registration.CurrentPort);
        }
        public static DataSource Server
        {
            get => new DataSource(RadarClient.ServerAddress, RadarClient.ServerPort);
        }
        public string Address { get; set; }
        public int Port { get; set; }
        public DataSource(string address, int port)
        {
            Address = address;
            Port = port;
        }
        public bool IsSelf
        {
            get => Address == ClientRegistration.Registration.Address && Port == ClientRegistration.Registration.CurrentPort;
        }
        public bool IsServer
        {
            get => Address == RadarClient.ServerAddress && Port == RadarClient.ServerPort;
        }
    }
}
