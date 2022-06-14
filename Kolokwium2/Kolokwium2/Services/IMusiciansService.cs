using Kolokwium2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public interface IMusiciansService
    {
        public Task<bool> DoesMusicianExist(int idMusician);

        public Task<IEnumerable<Track>> GetTracks(int idMusician);
        public Task<IEnumerable<MusicianTrack>> GetMusicianTracks(int idMusician);
        public Task<bool> IsMusicianValidDoDelete(int idMusician);

        public Task DeleteMusiciansTracks(int idMusician);
        public Task DeleteMusician(int idMusician);
        public Task SaveDatabase();
    }
}
