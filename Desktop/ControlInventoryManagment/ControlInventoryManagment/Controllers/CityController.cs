using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlInventoryManagment.DTOs.City;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.Services;
using ControlInventoryManagment.Exceptions;

namespace ControlInventoryManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityReadDTO>> GetCityById(int id)
        {
            try
            {
                var city = await _cityService.GetCityById(id);
                if (city == null)
                {
                    return NotFound("City not found.");
                }
                return Ok(city);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<CityReadDTO>> GetCityByName(string name)
        {
            try
            {
                var city = await _cityService.GetCityByName(name);
                if (city == null)
                {
                    return NotFound("City not found.");
                }
                return Ok(city);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CityReadDTO>> CreateCity(CityCreateDTO newCityDTO)
        {
            try
            {
                var createdCity = await _cityService.CreateCity(newCityDTO);
                return CreatedAtAction(nameof(GetCityById), new { id = createdCity.Id }, createdCity);
            }
            catch (NotSavedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDTO updatedCityDTO)
        {
            if (id != updatedCityDTO.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                await _cityService.UpdateCity(updatedCityDTO);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotUpdatedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
public async Task<IActionResult> DeleteCity(int id)
{
    try
    {
        var city = await _cityService.GetCityById(id);
        if (city == null)
        {
            return NotFound("City not found.");
        }

        await _cityService.DeleteCity(city);

        return NoContent();
    }
    catch (NotDeletedException ex)
    {
        return BadRequest(ex.Message);
    }
}

    }
}
