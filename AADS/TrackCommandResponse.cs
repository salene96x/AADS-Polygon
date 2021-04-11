using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public enum TrackCommandResponseCode
    {
        Success, Warning, Error
    }
    public class TrackCommandResponse
    {
        public TrackCommandResponseCode Code;
        public string Message;
    }
}
