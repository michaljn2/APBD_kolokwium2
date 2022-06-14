using Kolokwium2.Models.DTOs;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _service;

        public AlbumsController(IAlbumsService service)
        { 
            _service = service;
        }

        [HttpGet("{idAlbum}")]
        public async Task<IActionResult> GetAlbum(int idAlbum)
        {
            if (!await _service.DoesAlbumExist(idAlbum))
                return NotFound("Album does not exist");

            var album = await _service.GetAlbum(idAlbum);

            return Ok(new AlbumGet
            {
                AlbumName = album.AlbumName,
                PublishDate = album.PublishDate,
                Tracks = album.Tracks.OrderBy(e => e.Duration).Select(e => new Track
                {
                    IdTrack = e.IdTrack,
                    TrackName = e.TrackName,
                    Duration = e.Duration
                }).ToList()
            });
        }
       

    }
}
