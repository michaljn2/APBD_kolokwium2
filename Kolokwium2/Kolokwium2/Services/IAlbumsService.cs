using Kolokwium2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public interface IAlbumsService
    {
        public Task<bool> DoesAlbumExist(int idAlbum);

        public Task<Album> GetAlbum(int idAlbum);
    }
}
