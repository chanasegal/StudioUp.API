using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSubscriptionsController : ControllerBase
    {
        private readonly ICustomerSubscriptionRepository _repository;
        private readonly ILogger<CustomerSubscriptionsController> _logger;

        public CustomerSubscriptionsController(ICustomerSubscriptionRepository repository, ILogger<CustomerSubscriptionsController> logger)
        {
            _repository = repository;
            _logger= logger;    
        }
        [HttpGet("GetAllCustomerSubscriptions")]
        public async Task<ActionResult<IEnumerable<CustomerSubscriptionDTO>>> GetAllCustomerSubscriptions()
        {
            try
            {
                var subscriptions = await _repository.GetAllCustomerSubscriptionsAsync();
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetAllCustomerSubscriptions");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetCustomerSubscriptionsByCustomerId/{customerId}")]
        public async Task<ActionResult<CustomerSubscriptionDTO>> GetCustomerSubscriptionsByCustomerId(int customerId)
        {
            try
            {
                var subscriptions = await _repository.GetCustomerSubscriptionsByCustomerIdAsync(customerId);
                if (subscriptions == null)
                {
                    return NotFound();
                }
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetCustomerSubscriptionsByCustomerId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCustomerSubscriptionById/{id}")]
        public async Task<ActionResult<CustomerSubscriptionDTO>> GetCustomerSubscriptionById(int id)
        {
            try
            {
                var subscription = await _repository.GetCustomerSubscriptionByIdAsync(id);
                if (subscription == null)
                {
                    return NotFound();
                }
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/GetCustomerSubscriptionById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
            [HttpPost("AddCustomerSubscription")]
        public async Task<ActionResult> AddCustomerSubscription(CustomerSubscriptionDTO subscriptionDTO)
        {
            try
            {
                var s = await _repository.AddCustomerSubscriptionAsync(subscriptionDTO);
                if (s == null)
                {
                    return BadRequest("customerSubscription field is null.");
                }
                return Ok(s);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/AddCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateCustomerSubscription")]
        public async Task<IActionResult> UpdateCustomerSubscription(CustomerSubscriptionDTO subscriptionDTO)
        {
            if (subscriptionDTO == null)
            {
                return BadRequest("The content  field is null.");
            }

            try
            {
                await _repository.UpdateCustomerSubscriptionAsync(subscriptionDTO);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/UpdateCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteCustomerSubscription/{id}")]
        public async Task<IActionResult> DeleteCustomerSubscription(int id)
        {
            try
            {
                await _repository.DeleteCustomerSubscriptionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in CustomerSubscriptionsController/DeleteCustomerSubscription");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}