using Microsoft.AspNetCore.Mvc;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using ControlInventoryManagment.DTOs.Categorie;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IMapper _mapper;

        public CategorieController(ICategorieRepository categorieRepository, IMapper mapper)
        {
            _categorieRepository = categorieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorie>>> GetCategories()
        {
            var categories = await _categorieRepository.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categorie>> GetCategorie(int id)
        {
            var categorie = await _categorieRepository.GetCategorieById(id);

            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }

        [HttpPost]
        public async Task<ActionResult<Categorie>> CreateCategorie(Categorie newCategorie)
        {
            var createdCategorie = await _categorieRepository.CreateCategorie(newCategorie);
            return CreatedAtAction(nameof(GetCategorie), new { id = createdCategorie.Id }, createdCategorie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategorie(int id, CategorieUpdateDTO categorieUpdateDto)
        {
            var categorieFromRepo = await _categorieRepository.GetCategorieById(id);
            if (categorieFromRepo == null)
            {
                return NotFound();
            }

            // Update the existing categorieFromRepo with data from categorieUpdateDto
            _mapper.Map(categorieUpdateDto, categorieFromRepo);

            await _categorieRepository.UpdateCategorie(categorieFromRepo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categorie = await _categorieRepository.GetCategorieById(id);

            if (categorie == null)
            {
                return NotFound();
            }

            await _categorieRepository.DeleteCategorie(categorie);

            return NoContent();
        }
    }
}