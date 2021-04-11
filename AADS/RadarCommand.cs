using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class RadarCommand : BaseRadarCommand
    {
        public SendCommandWrapper<RadarCommand> Wrap()
        {
            SendCommandWrapper<RadarCommand> wrapper = new SendCommandWrapper<RadarCommand>();
            wrapper.Command = this;
            return wrapper;
        }
        public static RadarCommand GetCommand(RadarOperation operation)
        {
            return new RadarCommand
            {
                Operation = operation
            };
        }
    }
}
