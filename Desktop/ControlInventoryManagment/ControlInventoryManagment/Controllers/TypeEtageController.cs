using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeEtagesController : ControllerBase
    {
        private readonly ITypeEtagenRepository _typeEtageRepository;

        public TypeEtagesController(ITypeEtagenRepository typeEtageRepository)
        {
            _typeEtageRepository = typeEtageRepository;
        }

        // GET: api/typeetages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEtage>>> GetTypeEtages()
        {
            var typeEtages = await _typeEtageRepository.GetAllTypeEtage();
            return Ok(typeEtages);
        }

        // GET: api/typeetages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeEtage>> GetTypeEtage(int id)
        {
            var typeEtage = await _typeEtageRepository.GetTypeEtageById(id);

            if (typeEtage == null)
            {
                return NotFound();
            }

            return Ok(typeEtage);
        }

        // POST: api/typeetages
        [HttpPost]
        public async Task<ActionResult<TypeEtage>> CreateTypeEtage(TypeEtage newTypeEtage)
        {
            if (newTypeEtage == null)
            {
                return BadRequest();
            }

            var typeEtage = await _typeEtageRepository.CreateTypeEtage(newTypeEtage);
            return CreatedAtAction(nameof(GetTypeEtage), new { id = typeEtage.Id }, typeEtage);
        }

        // PUT: api/typeetages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTypeEtage(int id, TypeEtage updatedTypeEtage)
        {
            if (id != updatedTypeEtage.Id)
            {
                return BadRequest();
            }

            var existingTypeEtage = await _typeEtageRepository.GetTypeEtageById(id);
            if (existingTypeEtage == null)
            {
                return NotFound();
            }

            await _typeEtageRepository.UpdateTypeEtage(updatedTypeEtage);
            return NoContent();
        }

        // DELETE: api/typeetages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeEtage(int id)
        {
            var typeEtage = await _typeEtageRepository.GetTypeEtageById(id);
            if (typeEtage == null)
            {
                return NotFound();
            }

            await _typeEtageRepository.DeleteTypeEtage(typeEtage);
            return NoContent();
        }
    }
}
