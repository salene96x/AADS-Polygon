using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public abstract class BaseRadarCommand
    {
        public RadarOperation Operation { get; set; }
    }
}
