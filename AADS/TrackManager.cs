using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS
{
    public delegate void TrackClear(TrackFakerType type);
    public delegate void TrackCreate(TrackData item);
    public delegate void TrackUpdate(TrackData item);
    public delegate void TrackRemove(TrackData item);
    public class TrackManager
    {
        private Dictionary<string, TrackData> tracks = new Dictionary<string, TrackData>();
        public Dictionary<string, GMarkerTrack> trackMarkers = new Dictionary<string, GMarkerTrack>();
        private Dictionary<string, TrackData> fakers = new Dictionary<string, TrackData>();
        public Dictionary<string, GMarkerTrack> fakerMarkers = new Dictionary<string, GMarkerTrack>();
        public event TrackClear OnTrackClear;
        public event TrackCreate OnTrackCreate;
        public event TrackUpdate OnTrackUpdate;
        public event TrackRemove OnTrackRemove;
        public void Clear()
        {
            OnTrackClear?.Invoke(TrackFakerType.None);
            this.tracks.Clear();
            trackMarkers.Clear();
        }
        public void ClearFaker()
        {
            OnTrackClear?.Invoke(TrackFakerType.Client);
            fakers.Clear();
            fakerMarkers.Clear();
        }
        public List<TrackData> Tracks
        {
            get => new List<TrackData>(tracks.Values);
        }
        public List<TrackData> Fakers
        {
            get => new List<TrackData>(fakers.Values);
        }
        public TrackData GetTrack(string key)
        {
            if (tracks.ContainsKey(key))
            {
                return tracks[key];
            }
            return null;
        }
        public TrackCommandResponse CreateTrack(TrackData track)
        {
            var key = track.Key;
            TrackCommandResponse response = new TrackCommandResponse
            {
                Code = TrackCommandResponseCode.Error
            };
            if (track.Faker == TrackFakerType.None || track.Faker == TrackFakerType.Server)
            {
                if (tracks.ContainsKey(key))
                {
                    response.Message = "DUPLICATE_KEY";
                }
                else
                {
                    tracks.Add(key, track);
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackCreate?.Invoke(track);
                }
            }
            else
            {
                if (fakers.ContainsKey(key))
                {
                    response.Message = "DUPLICATE_KEY";
                }
                else
                {
                    fakers.Add(key, track);
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackCreate?.Invoke(track);
                }
            }
            return response;
        }
        public TrackCommandResponse UpdateTrack(TrackData track)
        {
            var key = track.Key;
            TrackCommandResponse response = new TrackCommandResponse
            {
                Code = TrackCommandResponseCode.Error
            };
            if (track.Faker == TrackFakerType.None || track.Faker == TrackFakerType.Server)
            {
                if (tracks.ContainsKey(key))
                {
                    tracks[key] = track;
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackUpdate?.Invoke(track);
                }
                else
                {
                    response.Message = "KEY_NOT_FOUND";
                }
            }
            else
            {
                if (fakers.ContainsKey(key))
                {
                    fakers[key] = track;
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackUpdate?.Invoke(track);
                }
                else
                {
                    response.Message = "KEY_NOT_FOUND";
                }
            }
            return response;
        }
        public TrackCommandResponse CreateOrUpdateTrack(TrackData track)
        {
            var key = track.Key;
            if (track.Faker == TrackFakerType.None || track.Faker == TrackFakerType.Server)
            {
                if (tracks.ContainsKey(key))
                {
                    return UpdateTrack(track);
                }
                else
                {
                    return CreateTrack(track);
                }
            }
            return new TrackCommandResponse
            {
                Code = TrackCommandResponseCode.Error
            };
        }
        public TrackCommandResponse RemoveTrack(string key, TrackFakerType type)
        {
            TrackCommandResponse response = new TrackCommandResponse
            {
                Code = TrackCommandResponseCode.Error
            };
            if (type == TrackFakerType.None || type == TrackFakerType.Server)
            {
                if (tracks.ContainsKey(key))
                {
                    var track = tracks[key];
                    tracks.Remove(key);
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackRemove?.Invoke(track);
                }
                else
                {
                    response.Message = "KEY_NOT_FOUND";
                }
            }
            else
            {
                if (fakers.ContainsKey(key))
                {
                    var faker = fakers[key];
                    fakers.Remove(key);
                    response.Code = TrackCommandResponseCode.Success;
                    OnTrackRemove?.Invoke(faker);
                }
                else
                {
                    response.Message = "KEY_NOT_FOUND";
                }
            }
            return response;
        }
    }
}
