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
        private List<Tuple<int, string>> anomalies;
       public DLLModel() { }

        /**
         * Delegates for functions in C#
         **/

        // learn()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr Learn(string train, string test);


        // vecSize()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int VecSize(IntPtr ve);

        // getTimeByIndex()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetTimeByIndex(IntPtr ve, double index);

        // CreatestringWrapper()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr CreateStringWrapper(IntPtr ve, double index);

        // len()
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Len(IntPtr str);

        // getCharByIndex();        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate char GetCharByIndex(IntPtr str, double x);


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
        public void handleDLLFileUpload(string dll, string ttrain, string ttest)
        {
            string train = @"C:\Users\grano\Desktop\Milestone_1\FlightSimulator\public\reg_flight.csv";
            string test = @"C:\Users\grano\Desktop\Milestone_1\FlightSimulator\public\anomaly_flight.csv";

            List<Tuple<int, string>> anomalies = new List<Tuple<int, string>>();
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

            procaddr = GetProcAddress(mydll, "vecSize");

            if (procaddr == IntPtr.Zero)
            {
                Console.WriteLine("Can't find function");
                return;
            }

            // Delegate vector size
            VecSize vecSize = (VecSize)Marshal.GetDelegateForFunctionPointer(procaddr, typeof(VecSize));
            int size = vecSize(ve);
            
            // Get proc of remaining functions to use inside the loop
            IntPtr procCreateStringWrapper = GetProcAddress(mydll, "CreatestringWrapper");
            IntPtr procGetCharByIndex = GetProcAddress(mydll, "getCharByIndex");
            IntPtr procGetTimeByIndex = GetProcAddress(mydll, "getTimeByIndex");
            IntPtr procLen = GetProcAddress(mydll, "len");

            // Validate them
            if (procCreateStringWrapper == IntPtr.Zero  || procGetCharByIndex == IntPtr.Zero || procGetTimeByIndex == IntPtr.Zero || procLen == IntPtr.Zero)
            {
                Console.WriteLine("One of the functions can not be loaded probably");
                return;
            }

            // Get delegates
            CreateStringWrapper createStringWrapper = (CreateStringWrapper)Marshal.GetDelegateForFunctionPointer(procCreateStringWrapper, typeof(CreateStringWrapper));
            GetCharByIndex getCharByIndex = (GetCharByIndex)Marshal.GetDelegateForFunctionPointer(procGetCharByIndex, typeof(GetCharByIndex));
            GetTimeByIndex getTimeByIndex = (GetTimeByIndex)Marshal.GetDelegateForFunctionPointer(procGetTimeByIndex, typeof(GetTimeByIndex));
            Len len = (Len)Marshal.GetDelegateForFunctionPointer(procLen, typeof(Len));

            // Validate them

            if (createStringWrapper == null || getCharByIndex == null || getTimeByIndex == null || len == null)
            {
                Console.WriteLine("One of the delegates was not loaded properly");
                return;
            }



            for (int i = 0; i < size; ++i)
            {
                string s = "";
                IntPtr str = createStringWrapper(ve, i);
                int strLength = len(str);

                for (double j = 0; j < strLength; ++j)
                {
                    char c = getCharByIndex(str, j);
                    s += c.ToString();
                }

                int timestamp = getTimeByIndex(ve, i);

                anomalies.Add(new Tuple<int, string>(timestamp, s));
            }

            this.anomalies = anomalies;
            NotifyPropertyChanged("Anomalies");

            // Set library free;
            FreeLibrary(mydll);
        }

        public List<Tuple<int, string>> Anomalies
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
