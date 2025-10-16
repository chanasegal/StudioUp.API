using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCustomersTypesController : Controller
    {
        private readonly ITrainingCustomerTypesRepository _repository;
        private readonly ILogger<TrainingCustomersTypesController> _logger;

        public TrainingCustomersTypesController(ITrainingCustomerTypesRepository repsitory, ILogger<TrainingCustomersTypesController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }

        //גם כאלו שהם לא בפעילות TrainingCusromerType פונקציה שמביאה את כל המערך של
        [HttpGet("GetAllTrainingCustomerTypes")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerType>>> GetAllTrainingCustomerTypes()
        {
            try
            {
                var trainingCustomerType = await _repository.GetAllAsync();
                return Ok(trainingCustomerType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomersTypesController/GetAllTrainingCustomerTypes");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        ////אבל רק את אלו שבפעילות TrainingCusromerType פונקציה שמביאה את המערך של
        //[HttpGet("TCTInActivity")]
        //public async Task<ActionResult<IEnumerable<TrainingCustomerType>>> GetTrainingCustomerTypeInActivity()
        //{
        //    var trainingCustomerType = await _repository.GetActiveTrainingCustomerTypes();
        //    return Ok(trainingCustomerType);
        //}


        [HttpGet("GetTrainingCustomerTypeById/{id}")]
        public async Task<ActionResult<TrainingCustomerTypeDTO>> GetTrainingCustomerTypeById(int id)
        {
            try
            {
                var TrainingCustomerType = await _repository.GetByIdAsync(id);
                if (TrainingCustomerType == null)
                {
                    return NotFound($"Training Customer Type with id {id} not found");
                }
                return Ok(TrainingCustomerType);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomersTypesController/GetTrainingCustomerTypeById");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        //עדכון רגיל של אימון
        [HttpPut("UpdateTrainingCustomerType")]
        public async Task<ActionResult<TrainingCustomerTypePostComand>> UpdateTrainingCustomerType([FromBody] TrainingCustomerTypePostComand trainingCustomerTypedto)
        {
            try
            {
                if (trainingCustomerTypedto == null)
                {
                    return BadRequest("The content field is null");
                }
                var trainingCustomerType = await _repository.GetByIdAsync(trainingCustomerTypedto.ID);
                if (trainingCustomerType == null)
                {
                    return NotFound();
                }
                await _repository.UpdateAsync(trainingCustomerTypedto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomersTypesController/UpdateTrainingCustomerType");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }

        [HttpPost("AddTrainingCustomerType")]
        public async Task<ActionResult<TrainingCustomerTypePostComand>> AddTrainingCustomerType(TrainingCustomerTypePostComand trainingCustomerTypedto)
        {
            try
            {
                if (trainingCustomerTypedto == null)
                {
                    return BadRequest("Training Customer Type cannot be null.");
                }
                if (trainingCustomerTypedto == null)
                    return BadRequest();
                var trainingCustomerType = await _repository.AddAsync(trainingCustomerTypedto);
                trainingCustomerType.IsActive = true;
                return CreatedAtAction(nameof(GetAllTrainingCustomerTypes), new { id = trainingCustomerTypedto.ID }, trainingCustomerTypedto);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomersTypesController/addTrainingCustomerType");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }

        //שימו לב!!! זה בכוונה על עדכון ולא מחיקה!! נא לא לשנות את זה
        //הפונקציה לא מוחקת בפועל את השיעור אלא רק הופכת את ה isActive להיות false
        [HttpDelete("DeleteTrainingCustomerType/{id}")]
        public async Task<IActionResult> DeleteTrainingCustomerType(int id)
        {
            try
            {
                var TrainingCustomerType = await _repository.GetByIdAsync(id);
                if (TrainingCustomerType == null)
                {
                    return NotFound($"Training Customer Type with id {id} not found");
                }
                await _repository.DeleteAsync(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomersTypesController/DeleteTrainingCustomerType");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }

    }
}
