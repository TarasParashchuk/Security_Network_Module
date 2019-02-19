using System.Collections.Generic;

namespace Security_network_module.Model
{
    class NetworkParameter
    {
        public string Name { get; set; }
        public List<double> Array_coordinates { get; set; }
    }

    class DataGridNetworkParameter
    {
        public string Name { get; set; }
        public string Array_coordinates { get; set; }
    }
}
