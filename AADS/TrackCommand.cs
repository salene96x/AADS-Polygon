using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public class TrackCommand : BaseRadarCommand
    {
        public List<TrackData> Tracks { get; set; }
        public SendCommandWrapper<TrackCommand> Wrap()
        {
            SendCommandWrapper<TrackCommand> wrapper = new SendCommandWrapper<TrackCommand>();
            wrapper.Command = this;
            return wrapper;
        }
        public static TrackCommand SyncCommand
        {
            get => new TrackCommand
            {
                Operation = RadarOperation.Sync
            };
        }
        public static TrackCommand GetSingle(RadarOperation operation, TrackData track)
        {
            return GetMultiple(operation, new List<TrackData>
            {
                track
            });
        }
        public static TrackCommand GetMultiple(RadarOperation operation, List<TrackData> tracks)
        {
            TrackCommand command = new TrackCommand
            {
                Operation = operation,
                Tracks = tracks
            };
            return command;
        }
    }
}
