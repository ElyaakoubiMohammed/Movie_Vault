using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlInventoryManagment.DTOs.Categorie;
using ControlInventoryManagment.Services;
using ControlInventoryManagment.Exceptions;

namespace ControlInventoryManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategorieService _categorieService;

        public CategoriesController(CategorieService categorieService)
        {
            _categorieService = categorieService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategorieReadDTO>> GetCategorieById(int id)
        {
            try
            {
                var categorie = await _categorieService.GetCategorieById(id);
                if (categorie == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(categorie);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<CategorieReadDTO>> GetCategorieByName(string name)
        {
            try
            {
                var categorie = await _categorieService.GetCategorieByName(name);
                if (categorie == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(categorie);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategorieReadDTO>> CreateCategorie(CategorieCreateDTO newCategorieDTO)
        {
            try
            {
                var createdCategorie = await _categorieService.CreateCategorie(newCategorieDTO);
                return CreatedAtAction(nameof(GetCategorieById), new { id = createdCategorie.Id }, createdCategorie);
            }
            catch (NotSavedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateCategorie(CategorieUpdateDTO updatedCategorieDTO)
        {
            try
            {
                await _categorieService.UpdateCategorie(updatedCategorieDTO);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (NotUpdatedException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            try
            {
                var categorie = await _categorieService.GetCategorieById(id);
                if (categorie == null)
                {
                    return NotFound("Category not found.");
                }

                await _categorieService.DeleteCategorie(categorie);
                return NoContent();
            }
            catch (NotDeletedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
