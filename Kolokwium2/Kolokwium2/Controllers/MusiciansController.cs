using Kolokwium2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

            if (!await _service.IsMusicianValidDoDelete(idMusician))
                return NotFound("Cannot delete this musician");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _service.DeleteMusiciansTracks(idMusician);
                    await _service.SaveDatabase();
                    await _service.DeleteMusician(idMusician);
                    scope.Complete();
                }catch (Exception)
                {
                    return Problem("Unexpected problem with database");
                }
            }
            await _service.SaveDatabase();
            return NoContent();
        }
    }
}
