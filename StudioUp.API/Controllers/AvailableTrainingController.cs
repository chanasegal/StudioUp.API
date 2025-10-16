using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableTrainingController : ControllerBase
    {
        private readonly IAvailableTrainingRepository _availableTrainingRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ILogger<AvailableTrainingController> _logger;

        public AvailableTrainingController(IAvailableTrainingRepository availableTrainingRepository,ITrainingRepository trainingRepository, ILogger<AvailableTrainingController> logger)
        {
            _availableTrainingRepository = availableTrainingRepository;
            _trainingRepository= trainingRepository;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAllAvailableTrainings()
        {
            try
            {
                var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsync();
                return Ok(availableTrainingsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAllAvailableTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/AvailableTraining/forCalander

        [HttpGet("GetAvailableTrainingsCalender")]
        public async Task<ActionResult<IEnumerable<AvailableTrainingDTO>>> GetAvailableTrainingsCalender()
        {
            var availableTrainingsDTO = await _availableTrainingRepository.GetAllAvailableTrainingsAsyncForCalander();
            return Ok(availableTrainingsDTO);
        }

        //[HttpGet("{id}")]
        [HttpGet("GetByIdAvailableTraining/{id}")]
        public async Task<ActionResult<AvailableTrainingDTO>> GetByIdAvailableTraining(int id)
        {
            try
            {
                var availableTrainingDTO = await _availableTrainingRepository.GetAvailableTrainingByIdAsync(id);
                if (availableTrainingDTO == null)
                {
                    return NotFound("available training not found by ID");
                }
                return Ok(availableTrainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetByIdAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }

        [HttpGet("GetByTrainingIdForCalander/{id}")]
        public async Task<ActionResult<CalanderAvailableTrainingDTO>> GetByTrainingIdAvailableTrainingForCalander(int id)
        {
            try
            {
                var availableTrainingDTO = await _availableTrainingRepository.GetAvailableByTrainingIdForCalander(id);

                if (availableTrainingDTO == null)
                {
                    return NotFound($"Training with ID {id} not found.");
                }

                return Ok(availableTrainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAvailableTrainingsForCustomerAsync");
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }

        [HttpGet("GetAllTrainingsDetailsForCustomerAsync/{id}")]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetAllTrainingsDetailsForCustomerAsync(int id)
        {
            try
            {
                var trainings = await _availableTrainingRepository.GetAllTrainingsDetailsForCustomerAsync(id);
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GetAvailableTrainingsForCustomer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

       
        [HttpPost("CreateAvailableTraining")]
        public async Task<ActionResult<AvailableTrainingDTO>> CreateAvailableTraining([FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (availableTrainingDTO == null)
            {
                return BadRequest("The availableTrainingDTO field is null.");
            }
            try
            {
                await _availableTrainingRepository.AddAvailableTrainingAsync(availableTrainingDTO);
                return CreatedAtAction(nameof(GetByIdAvailableTraining), new { id = availableTrainingDTO.Id }, availableTrainingDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/CreateAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPut("UpdateAvailableTraining")]
        public async Task<IActionResult> UpdateAvailableTraining([FromBody] AvailableTrainingDTO availableTrainingDTO)
        {
            if (availableTrainingDTO == null)
            {
                return BadRequest("AvailableTrainingDTO object is null");
            }

            try
            {
                await _availableTrainingRepository.UpdateAvailableTrainingAsync(availableTrainingDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/UpdateAvailableTrainingAsync");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAvailableTraining/{id}")]
        public async Task<IActionResult> DeleteAvailableTraining(int id)
        {
            try
            {
                await _availableTrainingRepository.DeleteAvailableTrainingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/DeleteAvailableTraining");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpGet("GenerateAvailableTrainings")]
        public async Task<bool> GenerateAvailableTrainings( DateOnly startDate, DateOnly? endDate, bool isWeekEnd)
        {
            try
            {
             return await _availableTrainingRepository.GenerateAvailableTrainings(startDate,endDate,isWeekEnd);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in AvailableTrainingController/GenerateAvailableTrainings");
                return false;
            }
        }




    }
}
