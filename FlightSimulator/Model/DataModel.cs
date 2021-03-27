using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class DataModel : INotifyPropertyChanged
    {
      private Boolean stop = false;
        private string ip = "127.0.0.1";
      private int in_port = 5006;
      private int out_port = 5004;
      private string file;
        private SocketModel in_socket;
      private SocketModel out_socket;
      public event PropertyChangedEventHandler PropertyChanged;

        public DataModel()
        {
            this.in_socket = new SocketModel(ip, in_port);
            this.out_socket = new SocketModel(ip, out_port);
        }

   
        // File Property
        public string File {
           get { return this.file; }
           set
            {
                if (this.file != value)
                {
                    this.file = value;
                    this.start();
                    NotifyPropertyChanged("File");
                }
            }
        }


        public void start()
        {
            try
            {
                out_socket.disconnect();
                out_socket.connect();
                new Thread(delegate ()
                {
                    string[] lines = System.IO.File.ReadAllLines(this.file);
                    int i = 0;
                    while (!stop || i < lines.Length)
                    {
                        
                        Console.WriteLine(lines[i]);
                        out_socket.send(lines[i]);
                        i++;

                        Thread.Sleep(250);
                    }
                }).Start();
            } 
            catch (Exception e)
            {
                Console.Write("Error while starting connection");
            }
            
        }


        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }

}
