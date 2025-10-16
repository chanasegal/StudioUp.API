using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerControllers : ControllerBase
    {
        private readonly ITrainerRepository trainerRepository;
        private readonly ILogger<TrainerControllers> _logger;


        public TrainerControllers(ITrainerRepository trainerRepository, ILogger<TrainerControllers> logger)
        {
            this.trainerRepository = trainerRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllTrainers")]
        public async Task<ActionResult<List<TrainerDTO>>> GetAllTrainers()
        {
            try
            {
                var trainer = await trainerRepository.GetAllTrainersAsync();
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/getAllTrainers");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetTrainerById/{id}")]
        public async Task<ActionResult<TrainerDTO>> GetTrainerById(int id)
        {
            try
            {
                var trainer = await trainerRepository.GetTrainerByIdAsync(id);
                if (trainer == null)
                {
                    return NotFound("customer not found by ID");
                }
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/getTrainerById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AddTrainer")]
        public async Task<ActionResult<TrainerDTO>> AddTrainer(TrainerDTO t)
        {
            if (t == null)
            {
                return BadRequest("The trainer field is null.");
            }
            try
            {
                var trainer = await trainerRepository.AddTrainerAsync(t);
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/addTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPut]
        [Route("UpdateTrainer")]
        public async Task<IActionResult> UpdateTrainer(DTO.TrainerDTO t)

        {
            if (t == null)
            {
                return BadRequest("The trainer field is null.");
            }
            try
            {
                 await trainerRepository.UpdateTrainerAsync(t);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/updateTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("DeleteTrainer/{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            try
            {
                await trainerRepository.DeleteTrainerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/deleteTrainer");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<List<TrainerDTO>>> FilterTrainer([FromQuery] DTO.TrainerFilterDto filter)
        {
            try
            {

                filter.FirstName = filter.FirstName?.Trim();
                filter.LastName = filter.LastName?.Trim();
                filter.mail = filter.mail?.Trim();
                return Ok(await trainerRepository.FilterTrainerAsync(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in TrainerControllers/Filter");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


