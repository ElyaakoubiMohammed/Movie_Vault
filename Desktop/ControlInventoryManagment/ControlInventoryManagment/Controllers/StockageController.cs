using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockagesController : ControllerBase
    {
        private readonly IStockageRepository _stockageRepository;

        public StockagesController(IStockageRepository stockageRepository)
        {
            _stockageRepository = stockageRepository;
        }

        // GET: api/stockages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stockage>>> GetStockages()
        {
            var stockages = await _stockageRepository.GetAllStockage();
            return Ok(stockages);
        }

        // GET: api/stockages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Stockage>> GetStockage(int id)
        {
            var stockage = await _stockageRepository.GetStockageById(id);

            if (stockage == null)
            {
                return NotFound();
            }

            return Ok(stockage);
        }

        // POST: api/stockages
        [HttpPost]
        public async Task<ActionResult<Stockage>> CreateStockage(Stockage newStockage)
        {
            if (newStockage == null)
            {
                return BadRequest();
            }

            var stockage = await _stockageRepository.CreateStockage(newStockage);
            return CreatedAtAction(nameof(GetStockage), new { id = stockage.Id }, stockage);
        }

        // PUT: api/stockages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockage(int id, Stockage updatedStockage)
        {
            if (id != updatedStockage.Id)
            {
                return BadRequest();
            }

            var existingStockage = await _stockageRepository.GetStockageById(id);
            if (existingStockage == null)
            {
                return NotFound();
            }

            await _stockageRepository.UpdateStockage(updatedStockage);
            return NoContent();
        }

        // DELETE: api/stockages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockage(int id)
        {
            var stockage = await _stockageRepository.GetStockageById(id);
            if (stockage == null)
            {
                return NotFound();
            }

            await _stockageRepository.DeleteStockage(stockage);
            return NoContent();
        }
    }
}
