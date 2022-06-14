using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public double Duration { get; set; }
        public int? IdMusicAlbum  { get; set; }
        public virtual Album Album { get; set; }
        public IEnumerable<MusicianTrack> MusicianTracks { get; set; }
    }
}
