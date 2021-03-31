using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Helper
{
    public interface ICSVDataParser
    {
        float getDataInTime(string feat, int timeStamp);

        float[] getFeatureData(string feat);

        string getFeatMostCorrFeature(string feat);

        List<string> getFeatures();
    }
}
