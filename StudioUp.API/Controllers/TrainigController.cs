using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ILogger<TrainingController> _logger;


        public TrainingController(ITrainingRepository trainingRepository, ILogger<TrainingController> logger)
        {
            _trainingRepository = trainingRepository;
            _logger = logger;

        }

        // GET: api/Training
        [HttpGet("GetAllTrainings")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetAllTrainings()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainings();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetAllTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        // GET: api/Training/forCalander

        [HttpGet("GetTrainingsCalender")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetTrainingsCalender()
        {
            try
            {
                var trainings = await _trainingRepository.GetAllTrainingsCalender();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetTrainingsCalender");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Training/5
        [HttpGet("GetTrainingById/{id}")]
        public async Task<ActionResult<TrainingDTO>> GetTrainingById(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingById(id);
                if (training == null)
                {
                    return NotFound($"Training with id {id} not found");
                }
                return Ok(training);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetTrainingById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/Training
        [HttpPost("AddTraining")]
        public async Task<IActionResult> AddTraining([FromBody] TrainingPostDTO trainingDTO)
        {
            try
            {
                if (trainingDTO == null)
                {
                    return BadRequest("The training field is null.");
                }
                var training = await _trainingRepository.AddTraining(trainingDTO);
                training.IsActive = true;
                return CreatedAtAction(nameof(GetAllTrainings), new { id = 0 }, training);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/AddTraining");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // PUT: api/Training/5
        [HttpPut("UpdateTraining")]
        public async Task<IActionResult> UpdateTraining([FromBody] TrainingDTO trainingDto)
        {
            try
            {
                if (trainingDto == null)
                {
                    return BadRequest("The content field is null.");
                }
                var training = await _trainingRepository.GetTrainingById(trainingDto.ID);
                if (training == null)
                {
                    return NotFound();
                }

                await _trainingRepository.UpdateTraining(trainingDto);
                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/UpdateTraining");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Training/5
        [HttpDelete("DeleteTraining/{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            try
            {
                var training = await _trainingRepository.GetTrainingById(id);
                if (training == null)
                    return NotFound($"Training with ID {id} not found.");
                await _trainingRepository.DeleteTraining(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/DeleteTraining");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/Training/forCalander
        [HttpGet("ByCustomerTypeIdForCalander/{CustomerTypeId}")]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> GetByCustomerTypeIdForCalander(int CustomerTypeId)
        {
            try
            {
                var trainings = await _trainingRepository.GetByCustomerTypeForCalander(CustomerTypeId);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainingController/GetTrainingsCalender");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
