using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly MusicDbContext _context;
        public AlbumsService(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesAlbumExist(int idAlbum)
        {
            return await _context.Albums.AnyAsync(e => e.IdAlbum == idAlbum);
        }

        public async Task<Album> GetAlbum(int idAlbum)
        {
            return await _context.Albums.Where(e => e.IdAlbum == idAlbum).Include(e => e.Tracks).FirstOrDefaultAsync();
        }
    }
}
