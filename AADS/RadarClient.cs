using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AADS
{
    public class RadarClient
    {
        private static MainForm main = MainForm.GetInstance();
        public static Socket ClientSocket;
        public static string ServerAddress { get; set; }
        public static int ServerPort = 8888;
        private static byte[] buffer;
        public static bool Connected
        {
            get => ClientSocket != null && ClientSocket.Connected;
        }
        public static void CloseConnection()
        {
            main.trackHandler.Clear();
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            ClientSocket = null;
        }
        public static bool ConnectToServer(IPAddress ipAddr)
        {
            int attempts = 0;
            bool connected = false;
            if (!Connected)
            {
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                while (!Connected && attempts < 1)
                {
                    try
                    {
                        attempts++;
                        ClientSocket.Connect(ipAddr, ServerPort);
                        connected = true;
                    }
                    catch (SocketException e)
                    {
                        Debug.WriteLine(e);
                    }
                }
                if (connected)
                {
                    buffer = new byte[1];
                    ClientSocket.Receive(buffer, 0, 1, SocketFlags.None);
                    if (buffer[0] == 1)
                    {
                        buffer = new byte[4];
                        ClientSocket.Receive(buffer, 0, 4, SocketFlags.None);
                        int length = BitConverter.ToInt32(buffer, 0);
                        buffer = new byte[length];
                        ClientSocket.Receive(buffer, 0, length, SocketFlags.None);
                        byte[] dataSent = new byte[length];
                        Array.Copy(buffer, dataSent, length);
                        string text = Encoding.ASCII.GetString(dataSent);
                        ClientRegistration.Registration = JsonSerializer.Deserialize<ClientRegistration>(text);
                        ServerAddress = ipAddr.ToString();
                        SendString(JsonSerializer.Serialize(TrackCommand.SyncCommand.Wrap()));
                        buffer = new byte[4];
                        ClientSocket.BeginReceive(buffer, 0, 4, SocketFlags.None, ReceiveCallback, ClientSocket);
                    }
                    else
                    {
                        CloseConnection();
                        connected = false;
                    }
                }
            }
            return connected;
        }
        private static bool IsTypeof(string strType, Type type)
        {
            var fname = type.Namespace + "." + strType;
            return Type.GetType(fname) == type;
        }
        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            MainForm form = MainForm.GetInstance();
            TrackManager trackHandler = form.trackHandler;
            try
            {
                received = current.EndReceive(AR);
                int length = BitConverter.ToInt32(buffer, 0);
                buffer = new byte[length];
                current.Receive(buffer, 0, length, SocketFlags.None);
                byte[] dataSent = new byte[length];
                Array.Copy(buffer, dataSent, length);
                string text = Encoding.ASCII.GetString(dataSent);
                var wrapper = JsonSerializer.Deserialize<RecvCommandWrapper>(text);
                Console.WriteLine(text);
                if (IsTypeof(wrapper.Type, typeof(TrackCommand)))
                {
                    var command = JsonSerializer.Deserialize<TrackCommand>(wrapper.Command.ToString());
                    if (command.Operation == RadarOperation.Create)
                    {
                        foreach (var track in command.Tracks)
                        {
                            trackHandler.CreateTrack(track);
                        }
                    }
                    else if (command.Operation == RadarOperation.Update)
                    {
                        foreach (var track in command.Tracks)
                        {
                            trackHandler.UpdateTrack(track);
                        }
                    }
                    else if (command.Operation == RadarOperation.Remove)
                    {
                        foreach (var track in command.Tracks)
                        {
                            trackHandler.RemoveTrack(track.Key, track.Faker);
                        }
                    }
                    else if (command.Operation == RadarOperation.Clear)
                    {
                        trackHandler.Clear();
                    }
                    else if (command.Operation == RadarOperation.Sync)
                    {
                        trackHandler.Clear();
                        foreach (var track in command.Tracks)
                        {
                            trackHandler.CreateTrack(track);
                        }
                    }
                }
                buffer = new byte[4];
                ClientSocket.BeginReceive(buffer, 0, 4, SocketFlags.None, ReceiveCallback, ClientSocket);
            }
            catch (SocketException)
            {
                CloseConnection();
            }
            catch (ObjectDisposedException)
            {

            }
        }

        /// <summary>
        /// Close socket and exit program.
        /// </summary>
        public static void Exit()
        {
            MainForm main = MainForm.GetInstance();
            List<TrackData> fakers = main.trackHandler.Fakers;
            SendString(JsonSerializer.Serialize(TrackCommand.GetMultiple(RadarOperation.Remove, fakers).Wrap()));
            SendString(JsonSerializer.Serialize(RadarCommand.GetCommand(RadarOperation.Disconnect).Wrap())); // Tell the server we are exiting
            CloseConnection();
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        public static void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            var header = BitConverter.GetBytes(buffer.Length);
            ClientSocket.Send(header, 0, header.Length, SocketFlags.None);
            ClientSocket.BeginSend(buffer, 0, buffer.Length, 0, SendCallback, ClientSocket);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                try
                {
                    handler.EndSend(ar);
                }
                catch (SocketException)
                {
                    CloseConnection();
                }
                catch (ObjectDisposedException)
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
