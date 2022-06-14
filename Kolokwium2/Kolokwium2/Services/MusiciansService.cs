using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public class MusiciansService : IMusiciansService
    {
        private readonly MusicDbContext _context;
        public MusiciansService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task DeleteMusician(int idMusician)
        {
            var musician = await _context.Musicians.Where(e => e.IdMusician == idMusician).FirstOrDefaultAsync();
            _context.Entry(musician).State = EntityState.Deleted;
        }

        public async Task DeleteMusiciansTracks(int idMusician)
        {
            var musicianTracks = await GetMusicianTracks(idMusician);
            foreach(MusicianTrack musicianTrack in musicianTracks)
            {
                _context.Entry(musicianTrack).State = EntityState.Deleted;
            }
            
        }

        public async Task<bool> DoesMusicianExist(int idMusician)
        {
            return await _context.Musicians.AnyAsync(e => e.IdMusician == idMusician);
        }

        public async  Task<IEnumerable<Track>> GetTracks(int idMusician)
        {
            return await _context.MusicianTracks.Where(e => e.IdMusician == idMusician)
                .Include(e => e.Track).Select(e => e.Track).ToListAsync();
        }

        public async Task<bool> IsMusicianValidDoDelete(int idMusician)
        {
            var tracks = await GetTracks(idMusician);

            foreach(Track track in tracks)
            {
                if (track.IdMusicAlbum != null)
                    return false;
            }
            return true;
        }

        public async Task<IEnumerable<MusicianTrack>> GetMusicianTracks(int idMusician)
        {
            return await _context.MusicianTracks.Where(e => e.IdMusician == idMusician).ToListAsync();
        }

        public async Task SaveDatabase()
        {
            await _context.SaveChangesAsync();
        }
    }
}
