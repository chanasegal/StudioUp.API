using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCustomerController : ControllerBase
    {
        private readonly ITrainingCustomerRepository _trainingCustomerRepository;
        private readonly ILogger<TrainingCustomerController> _logger;



        public TrainingCustomerController(ITrainingCustomerRepository trainingCustomerRepository, ILogger<TrainingCustomerController> logger)
        {
            _trainingCustomerRepository = trainingCustomerRepository;
            _logger = logger;
        }


        [HttpGet("GetAllTrainingCustomers")]

        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> GetAllTrainingCustomers()
        {
            try
            {
                var trainingsCustomers = await _trainingCustomerRepository.GetAllTrainingCustomers();
                return Ok(trainingsCustomers);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/GetAllTrainingCustomers");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }

        }

        [HttpGet("GetAllRegisteredTrainingsDetails")]
        public async Task<ActionResult<IEnumerable<CalanderAvailableTrainingDTO>>> GetAllRegisteredTrainingsDetailsAsync()
        {
            try
            {
                var trainings = await _trainingCustomerRepository.GetAllRegisteredTrainingsDetailsAsync();
                return Ok(trainings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetTraningCustomerById/{id}")]
        public async Task<ActionResult<TrainingCustomerDTO>> GetTraningCustomerById(int id)
        {
            try
            {
                var trainingCustomer = await _trainingCustomerRepository.GetTrainingCustomerById(id);
                if (trainingCustomer == null)
                {
                    return NotFound($"TraningCustomer with id {id} not found");
                }
                return Ok(trainingCustomer);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/GetTraningCustomerById");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }

        }


        [HttpGet("GetTraningCustomerByTraningId/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> GetTraningCustomerByTrainingId(int id)
        {
            try
            {
                var trainingsCustomer = await _trainingCustomerRepository.GetTrainingCustomerByTrainingId(id);
                if (trainingsCustomer == null || !trainingsCustomer.Any())
                {
                    return NotFound();
                }
                return Ok(trainingsCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/GetTraningCustomerByTraningId");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        [HttpGet("GetTraningCustomerByCustomerId/{id}")]
        public async Task<ActionResult<IEnumerable<TrainingCustomerDTO>>> GetTraningCustomerByCustomerId(int id)
        {
            try
            {
                var trainingsCustomer = await _trainingCustomerRepository.GetTrainingCustomerByCustomerId(id);
                if (trainingsCustomer == null || !trainingsCustomer.Any())
                {
                    return NotFound();
                }
                return Ok(trainingsCustomer);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/GetTraningCustomerByCustomerId");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        [HttpPost("AddTrainingCustomer")]
        public async Task<ActionResult<TrainingCustomer>> AddTrainingCustomer(TrainingCustomerDTO trainingCustomerDto)
        {
            try
            {
                if (trainingCustomerDto == null)
                    return BadRequest("Training Type cannot be null.");
                var trainingCustomer = await _trainingCustomerRepository.AddTrainingCustomer(trainingCustomerDto);
                trainingCustomer.IsActive = true;
                return CreatedAtAction(nameof(GetAllTrainingCustomers), new { id = trainingCustomer.ID }, trainingCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/AddTrainingCustomer");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        [HttpPut("UpdateTrainingCustomer")]
        public async Task<IActionResult> UpdateTrainingCustomer([FromBody] TrainingCustomerDTO trainingCustomerDto)
        {
            try
            {
                if (trainingCustomerDto == null)
                {
                    return BadRequest("The content field is null");
                }
                var trainingCustomer = await _trainingCustomerRepository.GetTrainingCustomerById(trainingCustomerDto.ID);
                if (trainingCustomer == null)
                {
                    return NotFound();
                }

                await _trainingCustomerRepository.UpdateTrainingCustomer(trainingCustomerDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/UpdateTrainingCustomer");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }


        [HttpDelete("DeleteTrainingCustomer/{id}")]
        public async Task<IActionResult> DeleteTrainingCustomer(int id)
        {
            try
            {
                var trainingCustomer = await _trainingCustomerRepository.GetTrainingCustomerById(id);
                if (trainingCustomer == null)
                {
                    return NotFound($"Training Customer with ID {id} not found.");
                }

                await _trainingCustomerRepository.DeleteTrainingCustomer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "this error in TrainingCustomerController/DeleteTrainingCustomer");
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }
        [HttpGet("FilterCustomersTrainingDeatails")]
        public async Task<ActionResult<List<CalanderAvailableTrainingDTO>>> FilterCustomersTrainingDeatails([FromQuery] DTO.CalanderAvailableTrainingFilterDTO filter)
        {
            try
            {
                return Ok(await _trainingCustomerRepository.FilterAsync(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CustomerTrainingsDetails/FilterTrainings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("AddTrainingForCustomer")]
        public async Task<ActionResult> addTrainingForCustomer(int trainingId, int customerId)
        {
            try
            {
                await _trainingCustomerRepository.AddTrainingForCustomer(trainingId, customerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        }
    }
