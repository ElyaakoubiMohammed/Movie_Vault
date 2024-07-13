using Microsoft.AspNetCore.Mvc;
using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using ControlInventoryManagment.DTOs.City;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _cityRepository.GetAllCities();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityRepository.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(CityCreateDTO cityCreateDto)
        {
            var newCity = _mapper.Map<City>(cityCreateDto);
            var createdCity = await _cityRepository.CreateCity(newCity);
            return CreatedAtAction(nameof(GetCity), new { id = createdCity.Id }, createdCity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDTO cityUpdateDto)
        {
            var cityFromRepo = await _cityRepository.GetCityById(id);
            if (cityFromRepo == null)
            {
                return NotFound();
            }

            // Update the existing cityFromRepo with data from cityUpdateDto
            _mapper.Map(cityUpdateDto, cityFromRepo);

            await _cityRepository.UpdateCity(cityFromRepo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _cityRepository.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            await _cityRepository.DeleteCity(city);

            return NoContent();
        }
    }
}
