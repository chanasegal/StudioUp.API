using Microsoft.AspNetCore.Mvc;
using StudioUp.Models;
using StudioUp.Repo;
using Microsoft.AspNetCore.Http;
using StudioUp.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol.Core.Types;


namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {

        private readonly IRepository<TrainingTypeDTO> _repository;
        private readonly ILogger<TrainingTypeController> _logger;

        public TrainingTypeController(IRepository<TrainingTypeDTO> repsitory, ILogger<TrainingTypeController> logger)
        {
            _repository = repsitory;
            _logger = logger;
        }


        [HttpGet("GetAllTrainingTypes")]
        public async Task<ActionResult<IEnumerable<TrainingTypeDTO>>> GetAllTrainingTypes()
        {
            try
            {
                var trainingTypes = await _repository.GetAllAsync();
                return Ok(trainingTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingTypeController/GetAllTrainingTypes");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        [HttpGet("GetTrainingTypeById/{id}")]
        public async Task<ActionResult<TrainingTypeDTO>> GetTrainingTypeById(int id)
        {
            try
            {
                var trainingType = await _repository.GetByIdAsync(id);
                if (trainingType == null)
                {
                    return NotFound($"Training Type with id {id} not found");
                }
                return Ok(trainingType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingTypeController/GetTrainingTypeById");
                return StatusCode(500, $"Internal server error:{ex.Message}");

            }
        }


        [HttpPut("UpdateTrainingType")]
        public async Task<IActionResult> UpdateTrainingType([FromBody] TrainingTypeDTO trainingTypeDto)
        {
            try
            {
                if (trainingTypeDto == null)
                {
                    return BadRequest("The content field is null");
                }

                var trainingType = await _repository.GetByIdAsync(trainingTypeDto.ID);
                if (trainingType == null)
                {
                    return NotFound();
                }

                await _repository.UpdateAsync(trainingTypeDto);
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingTypeController/UpdateTrainingType");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("AddTrainingType")]
        public async Task<ActionResult<TrainingType>> AddTrainingType([FromBody] TrainingTypeDTO TrainingTypeDto)
        {
            try
            {
                if (TrainingTypeDto == null)
                    return BadRequest("Training Type cannot be null.");
                var trainingType = await _repository.AddAsync(TrainingTypeDto);
                trainingType.IsActive = true;
                return CreatedAtAction(nameof(GetAllTrainingTypes), new { id = trainingType.ID }, trainingType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingTypeController/AddTrainingType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("DeleteTrainingType/{id}")]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            try
            {
                var trainingType = await _repository.GetByIdAsync(id);
                if (trainingType == null)
                {
                    return NotFound($"Training Type with ID {id} not found.");
                }

                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingTypeController/DeleteTrainingType");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

    }
}
