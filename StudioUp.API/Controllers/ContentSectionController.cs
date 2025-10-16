using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentSectionController : ControllerBase
    {
        private readonly IContentSectionRepository _repository;
        private readonly ILogger<ContentSectionController> _logger;


        public ContentSectionController(IContentSectionRepository repository, ILogger<ContentSectionController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetAllContentSections")]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetAllContentSections()
        {
            try
            {
                var contentSections = await _repository.GetAllAsync();
                 return Ok(contentSections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/GetAllContentSections");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetContentSectionById/{id}")]
        public async Task<IActionResult> GetContentSectionById(int id)
        {
            try
            {
                var contentSection = await _repository.GetByIdAsync(id);
                if (contentSection == null)
                {
                    return NotFound("content section not found by ID");
                }
                return Ok(contentSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/GetContentSectionById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetContentSectionsByIdContentType/{contentTypeId}")]
        public async Task<ActionResult<IEnumerable<ContentSectionDTO>>> GetContentSectionsByIdContentType(int contentTypeId)
        {
            try
            {
                var contentSections = await _repository.GetByContentTypeAsync(contentTypeId);
                if (contentSections == null)
                {
                    return NotFound("content section not found by content type");
                }
                return Ok(contentSections);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in ContentSectionController/GetContentSectionsByIdContentType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("CreateContentSection")]
        public async Task<ActionResult<ContentSectionDTO>> CreateContentSection([FromForm] ContentSectionManagementDTO contentSectionDTO)
        {
            if (contentSectionDTO == null)
            {
                return BadRequest("The content section field is null.");
            }
            try
            {
                var contentSection = await _repository.AddAsync(contentSectionDTO);
                return Ok(contentSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/CreateContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateContentSection")]
        public async Task<IActionResult> UpdateContentSection([FromForm] ContentSectionManagementDTO contentSectionDTO)
        {
            if (contentSectionDTO == null)
            {
                return BadRequest("The content section field is null.");
            }
            try
            {
                await _repository.UpdateAsync(contentSectionDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/UpdateContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteContentSection/{id}")]
        public async Task<IActionResult> DeleteContentSection(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentSectionController/DeleteContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
