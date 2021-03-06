using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models.DTOs
{
    public class AlbumGet
    {
    
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        public List<Track> Tracks { get; set; }

    }
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public double Duration { get; set; }
    }
}
