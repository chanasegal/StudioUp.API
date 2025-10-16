using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerHMOSController : ControllerBase
    {
        private readonly ICustomerHMOSRepository _customerHMOSRepository;
        private readonly ILogger<CustomerHMOSController> _logger;

        public CustomerHMOSController(ICustomerHMOSRepository customerHMOSRepository, ILogger<CustomerHMOSController> logger)
        {
            _customerHMOSRepository = customerHMOSRepository;
            _logger = logger;
        }

        [HttpGet("GetAllCustomerHMOS")]
        public async Task<ActionResult<IEnumerable<CustomerHMOSDTO>>> GetAllCustomerHMOS()
        {
            try
            {
                var customersHMOs = await _customerHMOSRepository.GetAllAsync();
                return Ok(customersHMOs);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerHOMSController/GetAllCustomerHMOS");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCustomerHMOSByID/{id}")]
        public async Task<ActionResult<CustomerHMOSDTO>> GetCustomerHMOSByID(int id)
        {
            try
            {
                var customerHMOS = await _customerHMOSRepository.GetByIdAsync(id);
                if (customerHMOS == null)
                    return NotFound($"CustomerHMOS with ID {id} not found.");
                return Ok(customerHMOS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerHOMSController/GetCustomerHMOSByID");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddCustomerHMO")]
        public async Task<ActionResult<int>> AddCustomerHMO([FromBody] CustomerHMOSDTO customerHMOSDTO)
        {
            try
            {
                if (customerHMOSDTO == null)
                    return BadRequest();

                var newCustomerId = await _customerHMOSRepository.AddAsync(customerHMOSDTO);
                return CreatedAtAction(nameof(GetCustomerHMOSByID), new { id = newCustomerId }, newCustomerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerHOMSController/AddCustomerHMO");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateCustomerHMOS")]
        public async Task<IActionResult> UpdateCustomerHMOS([FromBody] CustomerHMOSDTO customerHMOSDTO)
        {
            try
            {
                if (customerHMOSDTO == null)
                    return BadRequest();

                await _customerHMOSRepository.UpdateAsync(customerHMOSDTO);


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerHOMSController/UpdateCustomerHMOS");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCustomerHMOS/{id}")]
        public async Task<IActionResult> DeleteCustomerHMOS(int id)
        {
            try
            {
                await _customerHMOSRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerHOMSController/DeleteCustomerHMOS");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
