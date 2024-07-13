using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtagesController : ControllerBase
    {
        private readonly IEtageRepository _etageRepository;

        public EtagesController(IEtageRepository etageRepository)
        {
            _etageRepository = etageRepository;
        }

        // GET: api/etages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etage>>> GetEtages()
        {
            var etages = await _etageRepository.GetAllEtage();
            return Ok(etages);
        }

        // GET: api/etages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Etage>> GetEtage(int id)
        {
            var etage = await _etageRepository.GetEtageById(id);

            if (etage == null)
            {
                return NotFound();
            }

            return Ok(etage);
        }

        // POST: api/etages
        [HttpPost]
        public async Task<ActionResult<Etage>> CreateEtage(Etage newEtage)
        {
            if (newEtage == null)
            {
                return BadRequest();
            }

            var etage = await _etageRepository.CreateEtage(newEtage);
            return CreatedAtAction(nameof(GetEtage), new { id = etage.Id }, etage);
        }

        // PUT: api/etages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEtage(int id, Etage updatedEtage)
        {
            if (id != updatedEtage.Id)
            {
                return BadRequest();
            }

            var existingEtage = await _etageRepository.GetEtageById(id);
            if (existingEtage == null)
            {
                return NotFound();
            }

            await _etageRepository.UpdateEtage(updatedEtage);
            return NoContent();
        }

        // DELETE: api/etages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtage(int id)
        {
            var etage = await _etageRepository.GetEtageById(id);
            if (etage == null)
            {
                return NotFound();
            }

            await _etageRepository.DeleteEtage(etage);
            return NoContent();
        }
    }
}
