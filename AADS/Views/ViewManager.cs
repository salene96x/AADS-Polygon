using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADS.Views
{
    public class ViewManager
    {
        MainForm main = MainForm.GetInstance();
        public Track.TrackTable trackTable = new Track.TrackTable();
        public Track.FakerCreation fakerCreation = new Track.FakerCreation();
        private static ViewManager _Instance;
        public static ViewManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ViewManager();
                }
                return _Instance;
            }
        }
        public ViewManager()
        {
            main.gMap.OnMarkerClick += trackTable.GMap_MarkerClick;
            main.gMap.MouseClick += fakerCreation.GMap_MouseClick;
        }
    }
}
