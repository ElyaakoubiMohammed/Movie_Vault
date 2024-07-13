using ControlInventoryManagment.Models;
using ControlInventoryManagment.ServicesContract.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlInventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationRepository _operationRepository;

        public OperationsController(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        // GET: api/operations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperations()
        {
            var operations = await _operationRepository.GetAllOperations();
            return Ok(operations);
        }

        // GET: api/operations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Operation>> GetOperation(int id)
        {
            var operation = await _operationRepository.GetOperationById(id);

            if (operation == null)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        // POST: api/operations
        [HttpPost]
        public async Task<ActionResult<Operation>> CreateOperation(Operation newOperation)
        {
            if (newOperation == null)
            {
                return BadRequest();
            }

            var operation = await _operationRepository.CreateOperation(newOperation);
            return CreatedAtAction(nameof(GetOperation), new { id = operation.Id }, operation);
        }

        // PUT: api/operations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperation(int id, Operation updatedOperation)
        {
            if (id != updatedOperation.Id)
            {
                return BadRequest();
            }

            var existingOperation = await _operationRepository.GetOperationById(id);
            if (existingOperation == null)
            {
                return NotFound();
            }

            await _operationRepository.UpdateOperation(updatedOperation);
            return NoContent();
        }

        // DELETE: api/operations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            var operation = await _operationRepository.GetOperationById(id);
            if (operation == null)
            {
                return NotFound();
            }

            await _operationRepository.DeleteOperation(operation);
            return NoContent();
        }
    }
}
