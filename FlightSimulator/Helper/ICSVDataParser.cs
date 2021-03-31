using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Helper
{
    public interface ICSVDataParser
    {
        int getDataInTime(string feat, int timeStamp);

        int[] getFeatureData(string feat);

        string getFeatMostCorrFeature(string feat);

        List<string> getFeatures();
    }
}
