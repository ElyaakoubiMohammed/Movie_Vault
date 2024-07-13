using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntreprisesController : ControllerBase
    {
        private readonly IEntrepriseRepository _entrepriseRepository;

        public EntreprisesController(IEntrepriseRepository entrepriseRepository)
        {
            _entrepriseRepository = entrepriseRepository;
        }

        // GET: api/entreprises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetEntreprises()
        {
            var entreprises = await _entrepriseRepository.GetAllEntreprise();
            return Ok(entreprises);
        }

        // GET: api/entreprises/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Entreprise>> GetEntreprise(int id)
        {
            var entreprise = await _entrepriseRepository.GetEntrepriseById(id);

            if (entreprise == null)
            {
                return NotFound();
            }

            return Ok(entreprise);
        }

        // POST: api/entreprises
        [HttpPost]
        public async Task<ActionResult<Entreprise>> CreateEntreprise(Entreprise newEntreprise)
        {
            if (newEntreprise == null)
            {
                return BadRequest();
            }

            var entreprise = await _entrepriseRepository.CreateEntreprise(newEntreprise);
            return CreatedAtAction(nameof(GetEntreprise), new { id = entreprise.Id }, entreprise);
        }

        // PUT: api/entreprises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntreprise(int id, Entreprise updatedEntreprise)
        {
            if (id != updatedEntreprise.Id)
            {
                return BadRequest();
            }

            var existingEntreprise = await _entrepriseRepository.GetEntrepriseById(id);
            if (existingEntreprise == null)
            {
                return NotFound();
            }

            await _entrepriseRepository.UpdateEntreprise(updatedEntreprise);
            return NoContent();
        }

        // DELETE: api/entreprises/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntreprise(int id)
        {
            var entreprise = await _entrepriseRepository.GetEntrepriseById(id);
            if (entreprise == null)
            {
                return NotFound();
            }

            await _entrepriseRepository.DeleteEntreprise(entreprise);
            return NoContent();
        }
    }
}
