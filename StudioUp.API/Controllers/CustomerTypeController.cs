using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeRepository _repository;
        private readonly ILogger<CustomerTypeController> _logger;


        public CustomerTypeController(ICustomerTypeRepository repsitory, ILogger<CustomerTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        [HttpGet("GetAllCustomerTypes")]
        public async Task<ActionResult<IEnumerable<CustomerType>>> GetAllCustomerTypes()
        {
            try
            {
                var customerTypes = await _repository.GetAllAsync();
                return Ok(customerTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/GetCustomerTypes");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetCustomerTypeById/{id}")]
        public async Task<ActionResult<CustomerType>> GetCustomerTypeById(int id)
        {
            try
            {
                var customerType = await _repository.GetByIdAsync(id);
                if (customerType == null)
                {
                    return NotFound("customer not found by ID");
                }
                return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/GetCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("updateCustomerType")]
        public async Task<IActionResult> UpdateCustomerType(CustomerTypeDTO customerType)
        {
            if (customerType == null)
            {
                return BadRequest("The content field is null.");
            }

            try
            {
                await _repository.UpdateAsync(customerType);
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in CustomerTypeController/PutCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("addCustomerType")]
        public async Task<ActionResult<CustomerType>> addCustomerType(CustomerTypeDTO customerType)
        {
            try
            {
                var c = await _repository.AddAsync(customerType);
                if (c == null)
                {
                    return BadRequest("customer field is null.");
                }
                return Ok(c);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerController/AddCustomer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCustomerType/{id}")]
        public async Task<IActionResult> DeleteCustomerType(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerTypeController/DeleteCustomerType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
