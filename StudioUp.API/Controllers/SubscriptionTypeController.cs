using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly IRepository<SubscriptionTypeDTO> _repository;
        private readonly ILogger<SubscriptionTypeController> _logger;


        public SubscriptionTypeController(IRepository<SubscriptionTypeDTO> repsitory, ILogger<SubscriptionTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }
        [HttpGet("GetSubscriptionTypes")]
        public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
        {
            try
            {
                var subscriptionTypes = await _repository.GetAllAsync();
                return Ok(subscriptionTypes);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in SubscriptionTypeController/GetSubscriptionTypes");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpGet("GetSubscriptionTypeById/{id}")]
        public async Task<ActionResult<SubscriptionTypeDTO>> GetSubscriptionTypeById(int id)
        {
            try
            {
                var subscriptionType = await _repository.GetByIdAsync(id);
                if (subscriptionType == null)
                {
                    return NotFound("subscriptionType not found by ID");
                }
                return Ok(subscriptionType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/GetSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateSubscriptionType")]
        public async Task<ActionResult<SubscriptionTypeDTO>> UpdateSubscriptionType(SubscriptionTypeDTO subscriptionType)
        {
            if (subscriptionType == null)
            {
                return BadRequest("The subscriptionType field is null.");
            }
          
            try
            {
                await _repository.UpdateAsync(subscriptionType);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/PutSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateSubscriptionType")]
        public async Task<ActionResult<SubscriptionTypeDTO>> CreateSubscriptionType(SubscriptionTypeDTO subscriptionType)
        {
            if (subscriptionType == null)
            {
                return BadRequest("The subscriptionType field is null.");
            }
            try
            {
               var s= await _repository.AddAsync(subscriptionType);
                return Ok(s);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/PostSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteSubscriptionType/{id}")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
             try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in SubscriptionTypeController/DeleteSubscriptionType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
