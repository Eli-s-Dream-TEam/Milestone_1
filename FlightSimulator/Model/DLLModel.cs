using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace FlightSimulator.Model
{
    class DLLModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<Tuple<Tuple<int, int>, string>> anomalies;
       public DLLModel() { }

        /**
         * Delegates for functions in C#
         **/

        // learn()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr Learn(string train, string test);


        // getStringByIndex()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringByIndex(IntPtr ve, double index);

        // getTimestampByIndex()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetTimestampByIndex(IntPtr ve, double index);

        // vectorSize()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int VectorSize(IntPtr ve);


        /**
         * kernel32 implementations of loadlibrary, freelibrary and getprocaddress
         **/

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern IntPtr LoadLibrary(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(IntPtr hModule,
            [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll")]
        static extern bool FreeLibrary(IntPtr IntPtr_Module);

        /**
         * Gets dll file, test file and train file
         * Dynamically loads dll, sends test and train
         **/
        public void handleDLLFileUpload(string dll, string train, string test)
        {
            if (this.Anomalies != null)
            {
                this.anomalies.Clear();
            }

            else
            {
                this.anomalies = new List<Tuple<Tuple<int, int>, string>>();
            }

            // Load dll
            IntPtr mydll = LoadLibrary(dll);


            // Bail if dll is not loaded
            if (mydll == IntPtr.Zero)
            {
                Console.WriteLine("DLL Loading Failed!");
                return;
            }


            // Load learn() function
            IntPtr procaddr = GetProcAddress(mydll, "learn");

            // Bail if function not found
            if (procaddr == IntPtr.Zero)
            {
                Console.WriteLine("Can't find function");
                return;
            }

            // Delegate function
            Learn learn = (Learn)Marshal.GetDelegateForFunctionPointer(procaddr, typeof(Learn));
            IntPtr ve = learn(train, test);

            procaddr = GetProcAddress(mydll, "vectorSize");

            if (procaddr == IntPtr.Zero)
            {
                Console.WriteLine("Can't find function");
                return;
            }

            // Delegate vector size
            VectorSize vectorSize = (VectorSize)Marshal.GetDelegateForFunctionPointer(procaddr, typeof(VectorSize));
            int size = vectorSize(ve);


            // stringByIndex & Timestamp by index Procs

            IntPtr procStringByIndex = GetProcAddress(mydll, "getStringByIndex");
            IntPtr procTimestampByIndex = GetProcAddress(mydll, "getTimestampByIndex");

            if (procStringByIndex == IntPtr.Zero || procTimestampByIndex == IntPtr.Zero)
            {
                Console.WriteLine("Can't find function - string/timestamp by index");
                return;
            }

            // Delegates for string & timestamps


            GetStringByIndex getStringByIndex = (GetStringByIndex)Marshal.GetDelegateForFunctionPointer(procStringByIndex, typeof(GetStringByIndex));
            GetTimestampByIndex getTimestampByIndex = (GetTimestampByIndex)Marshal.GetDelegateForFunctionPointer(procTimestampByIndex, typeof(GetTimestampByIndex));


            if (getStringByIndex == null || getTimestampByIndex == null)
            {
                Console.WriteLine("One of the delegates was not loaded properly");
                return;
            }

            int firstTimestamp = 0;
            int previousTimestamp = 0;
            int lastTimestamp = 0;


            double i = 0;


            IntPtr ptr_str = getStringByIndex(ve, i);
            string str = Marshal.PtrToStringAnsi(ptr_str);
            for (; i < size; ++i)
            {
                int timestamp = getTimestampByIndex(ve, i);

                if (i == 0)
                {
                    previousTimestamp = timestamp;
                    firstTimestamp = timestamp;
                }

                else if ((timestamp - previousTimestamp) == 1)
                {
                    previousTimestamp = timestamp;
                }

                else if ((timestamp - previousTimestamp) != 1)
                {
                    lastTimestamp = previousTimestamp;

                    Tuple<int, int> inner_bounds = new Tuple<int, int>(firstTimestamp, lastTimestamp);
                    Tuple<Tuple<int, int>, string> inner_entry = new Tuple<Tuple<int, int>, string>(inner_bounds, str);

                    anomalies.Add(inner_entry);

                    ptr_str = getStringByIndex(ve, i + 1);
                    str = Marshal.PtrToStringAnsi(ptr_str);
                    firstTimestamp = timestamp;
                    previousTimestamp = timestamp;
                }

            }


            lastTimestamp = previousTimestamp;
            Tuple<int, int> bounds = new Tuple<int, int>(firstTimestamp, lastTimestamp);
            Tuple<Tuple<int, int>, string> entry = new Tuple<Tuple<int, int>, string>(bounds, str);
            anomalies.Add(entry);

            // Set library free;
            FreeLibrary(mydll);

           

             NotifyPropertyChanged("Anomalies");
        }

        public List<Tuple<Tuple<int,int>, string>> Anomalies
        {
            get { return this.anomalies; }
        }

        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
