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
        public async Task<bool> DoesMusicianExist(int idMusician)
        {
            return await _context.Musicians.AnyAsync(e => e.IdMusician == idMusician);
        }

        public async  Task<IEnumerable<Track>> GetMusiciansTracks(int idMusician)
        {
            return await _context.MusicianTracks.Where(e => e.IdMusician == idMusician)
                .Include(e => e.Track).Select(e => e.Track).ToListAsync();
        }
    }
}
