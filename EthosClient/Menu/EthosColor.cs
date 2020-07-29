using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthosClient.Settings
{
    public class EthosColor
    {
        public float R { get; set; }

        public float G { get; set; }

        public float B { get; set; }

        public EthosColor(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
