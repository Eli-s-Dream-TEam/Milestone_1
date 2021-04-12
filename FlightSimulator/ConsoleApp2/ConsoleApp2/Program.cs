using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp2
{
    class Wrapper
    {
        //RegDll Metohds
        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        static public extern IntPtr learn(string train, string test);
        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        static public extern int vecSize(IntPtr ve);
        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        static public extern int getTimeByIndex(IntPtr ve, double index);
        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        public static extern IntPtr CreatestringWrapper(IntPtr ve, double index);

        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        public static extern int len(IntPtr str);

        [DllImport("C:/Users/User/source/repos/ConsoleApp2/AnomalyDll.dll")]
        public static extern char getCharByIndex(IntPtr str, double x);


        public Wrapper()
        {
            string csv = "C:/Users/User/source/repos/ConsoleApp2/reg_flight.csv";
            string csv1 = "C:/Users/User/source/repos/ConsoleApp2/anomaly_flight.csv";
            var combo = new List<Tuple<string, Tuple<int, int>>>();
            try
            {
                IntPtr t = learn(csv, csv1);
                int size = vecSize(t);
                double i = 0;
                int firstTime = 0;
                int previousTime = 0;
                int lastTime;
                string s = "";
                for (i = 0; i < size; i++)
                {
                    s = "";
                    IntPtr str = CreatestringWrapper(t, i);
                    int str_len = len(str);
                    for (double j = 0; j < str_len; j++)
                    {
                        char c = getCharByIndex(str, j);
                        s += c.ToString();
                    }
                    s += "\n";
                    int time = getTimeByIndex(t, i);
                    if (i==0)
                    {
                        previousTime = time;
                        firstTime = time;
                    }
                    else if ((time - previousTime) == 1)
                    {
                        previousTime = time;
                    }
                    else if ((time - previousTime) != 1) {
                        lastTime = previousTime;
                        Tuple<int, int> temp = new Tuple<int, int>(firstTime, lastTime);
                        Tuple<string, Tuple<int, int>> ttemp = new Tuple<string, Tuple<int, int>>(s, temp);
                        combo.Add(ttemp);
                        firstTime = time;
                        previousTime = time;
                    }

                }
                lastTime = previousTime;
                Tuple<int, int> temp1 = new Tuple<int, int>(firstTime, lastTime);
                Tuple<string, Tuple<int, int>> ttemp1 = new Tuple<string, Tuple<int, int>>(s, temp1);
                combo.Add(ttemp1);
                for (int j=0; j<combo.Count; j++)
                {
                    Console.WriteLine(combo[j].Item1);
                    Console.WriteLine(combo[j].Item2);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
         
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Wrapper wrapper = new Wrapper();
            Console.WriteLine("end");
            return;
        }
    }
}
