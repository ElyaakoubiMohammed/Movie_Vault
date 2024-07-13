using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalsController : ControllerBase
    {
        private readonly ILocalRepository _localRepository;

        public LocalsController(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        // GET: api/locals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocals()
        {
            var locals = await _localRepository.GetAlllocal();
            return Ok(locals);
        }

        // GET: api/locals/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Local>> GetLocal(int id)
        {
            var local = await _localRepository.GetlocalById(id);

            if (local == null)
            {
                return NotFound();
            }

            return Ok(local);
        }

        // POST: api/locals
        [HttpPost]
        public async Task<ActionResult<Local>> CreateLocal(Local newLocal)
        {
            if (newLocal == null)
            {
                return BadRequest();
            }

            var local = await _localRepository.Createlocal(newLocal);
            return CreatedAtAction(nameof(GetLocal), new { id = local.Id }, local);
        }

        // PUT: api/locals/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocal(int id, Local updatedLocal)
        {
            if (id != updatedLocal.Id)
            {
                return BadRequest();
            }

            var existingLocal = await _localRepository.GetlocalById(id);
            if (existingLocal == null)
            {
                return NotFound();
            }

            await _localRepository.UpdateLocal(updatedLocal);
            return NoContent();
        }

        // DELETE: api/locals/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(int id)
        {
            var local = await _localRepository.GetlocalById(id);
            if (local == null)
            {
                return NotFound();
            }

            await _localRepository.Deletelocal(local);
            return NoContent();
        }
    }
}
