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
    public class MusiciansController : ControllerBase
    {
        private readonly IMusiciansService _service;
        public MusiciansController(IMusiciansService service)
        {
            _service = service;
        }

        [HttpDelete("{idMusician}")]
        public async Task<IActionResult> DeleteMusician(int idMusician)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _service.DoesMusicianExist(idMusician))
                return NotFound("Musician does not exist");


        }
    }
}
