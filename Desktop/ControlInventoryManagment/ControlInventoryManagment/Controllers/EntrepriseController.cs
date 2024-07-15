using ControlInventoryManagment.DTOs;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrepriseController : ControllerBase
    {
        private readonly EntrepriseService _entrepriseService;

        public EntrepriseController(EntrepriseService entrepriseService)
        {
            _entrepriseService = entrepriseService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntrepriseReadDTO>> GetEntrepriseById(int id)
        {
            try
            {
                var entreprise = await _entrepriseService.GetEntrepriseById(id);
                return Ok(entreprise);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<EntrepriseReadDTO>> GetEntrepriseByName(string name)
        {
            try
            {
                var entreprise = await _entrepriseService.GetEntrepriseByName(name);
                return Ok(entreprise);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EntrepriseReadDTO>> CreateEntreprise(EntrepriseCreateDTO newEntrepriseDTO)
        {
            try
            {
                var createdEntreprise = await _entrepriseService.CreateEntreprise(newEntrepriseDTO);
                return CreatedAtAction(nameof(GetEntrepriseById), new { id = createdEntreprise.Id }, createdEntreprise);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntreprise(int id, EntrepriseUpdateDTO updatedEntrepriseDTO)
        {
            try
            {
                if (id != updatedEntrepriseDTO.Id)
                {
                    return BadRequest("ID mismatch between route parameter and payload data.");
                }

                await _entrepriseService.UpdateEntreprise(updatedEntrepriseDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntreprise(int id)
        {
            try
            {
                await _entrepriseService.DeleteEntreprise(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
