using Newtonsoft.Json;

namespace Examen.Models
{
    public class DeviceData
    {
        public int codigoParametro { get; set; }
        public string nombreParametro { get; set; }
        public string unidadParametro { get; set; }
        public string abreviacionParametro { get; set; }
        public Values values { get; set; }
    }

    public class Values
    {
        public double[] avgData { get; set; }
        public double[] minData { get; set; }
        public double[] maxData { get; set; }
    }

    public class ChartData
    {
        public string[] device_dates { get; set; }
        public DeviceData[] device_data { get; set; }
    }

}
