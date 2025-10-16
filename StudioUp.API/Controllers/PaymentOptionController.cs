using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOptionController : ControllerBase
    {
        private readonly IRepository<PaymentOptionDTO> _repository;
        private readonly ILogger<PaymentOptionController> _logger;


        public PaymentOptionController(IRepository<PaymentOptionDTO> repsitory, ILogger<PaymentOptionController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        [HttpGet("GetPaymentOptions")]
        public async Task<ActionResult<IEnumerable<PaymentOptionDTO>>> GetAPaymentOptions()
        {
            try
            {
                var paymentOptions = await _repository.GetAllAsync();
                return Ok(paymentOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/GetPaymentOptions");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }

        }

        [HttpGet("GetPaymentOptionById/{id}")]
        public async Task<ActionResult<PaymentOptionDTO>> GetPaymentOptionById(int id)
        {
            try
            {
                var paymentOption = await _repository.GetByIdAsync(id);
                if (paymentOption == null)
                {
                    return NotFound("payment option not found by ID");
                }
                return Ok(paymentOption);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/GetPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("UpdatePaymentOption")]
        public async Task<IActionResult> UpdatePaymentOption(PaymentOptionDTO paymentOption)
        {
            if (paymentOption == null)
            {
                return BadRequest("The payment option field is null.");
            }
           
            try
            {
                await _repository.UpdateAsync(paymentOption);
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in PaymentOptionController/PutPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("CreatePaymentOption")]
        public async Task<ActionResult<PaymentOptionDTO>> CreatePaymentOption(PaymentOptionDTO paymentOption)
        {
            if (paymentOption == null)
            {
                return BadRequest("The payment option field is null.");
            }
            try
            {
                var p = await _repository.AddAsync(paymentOption);
                return Ok(p);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/PostPaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeletePaymentOptionById/{id}")]
        
        public async Task<IActionResult> DeletePaymentOption(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in PaymentOptionController/DeletePaymentOption");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
