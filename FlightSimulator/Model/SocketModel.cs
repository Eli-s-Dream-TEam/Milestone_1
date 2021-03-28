using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class SocketModel 
    {
        private string ip;
        private int port;
        private Socket socket;

        public SocketModel(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.socket = null;
        }

        public void connect()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ip);
                this.socket = socket;
            } catch (Exception err)
            {
                Console.WriteLine("Connection Error");
                this.socket = null;
                throw err;
            }
        }



        public void disconnect()
        {
            if (this.socket == null)
            {
                return;
            }

            this.socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public string recieve()
        {
           if (this.socket == null)
            {
                return "";
            }

            try
            {
                byte[] byteData = new byte[1024];
                socket.Receive(byteData);
                string data = BitConverter.ToString(byteData);
                return data;
            } 
            
            catch (Exception err)
            {
                throw err;
            }
        }
        public void send(string data)
        {
            if (this.socket == null)
            {
                return;
            }

            try
            {
                byte[] byteData = System.Text.Encoding.ASCII.GetBytes(data);
                socket.Send(byteData);
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }

            catch (SocketException se)
            {
                throw se;
            }

            catch (Exception err)
            {
                throw err;
            }

        }
    }
}
