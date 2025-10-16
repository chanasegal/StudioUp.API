using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
using StudioUp.Repo;
using AutoMapper;


namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypeController : ControllerBase
    {
        private readonly IContentTypeRepository _repository;
        private readonly ILogger<ContentTypeController> _logger;

        public ContentTypeController(IContentTypeRepository repository,ILogger<ContentTypeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("GetAllContentType")]
        public async Task<ActionResult<IEnumerable<ContentTypeDTO>>> GetAllContentType()
        {
            try
            {
                var contentTypes = await _repository.GetAll();
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetAllContentType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetContentTypeByIdWithContentSection/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetContentTypeByIdWithContentSection(int id)
        {
            try
            {
                ContentTypeDTO contentTypes = await _repository.GetByIdWithContentSection(id);
                if (contentTypes == null)
                {
                    return NotFound("content types not found by ID");
                }
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetContentTypeByIdWithContentSection");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("GetContentTypeByIdWithContentSectionHPOnly/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetContentTypeByIdWithContentSectionHPOnly(int id)
        {
            try
            {
                ContentTypeDTO contentTypes = await _repository.GetByIdWithContentSectionHPOnly(id);
                if (contentTypes == null)
                {
                    return NotFound("content types not found by ID");

                }
                return Ok(contentTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/GetContentTypeByIdWithContentSectionHPOnly");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("GetByContentTypeId/{id}")]
        public async Task<ActionResult<ContentTypeDTO>> GetByContentTypeId(int id)
        {
            try
            {
                var contentType = await _repository.GetById(id);
                if (contentType == null)
                {
                    return NotFound("content types not found by ID");
                }

                return Ok(contentType);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " this error in ContentTypeController/GetByContentTypeId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost("CreateContentType")]
        public async Task<ActionResult<ContentTypeDTO>> CreateContentType(ContentTypeDTO contentTypeDTO)
        {
            if (contentTypeDTO == null)
            {
                return BadRequest("The content type field is null.");
            }
            try
            {
                var contentType = await _repository.Create(contentTypeDTO);
                return Ok(contentType); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/CreateContentType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("UpdateContentType")]
        public async Task<ActionResult> UpdateContentType(ContentTypeDTO contentTypeDTO)
        {
            if (contentTypeDTO == null)
            {
                return BadRequest("The content type field is null.");
            }
            try
            {
                await _repository.Update(contentTypeDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/Update");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("DeleteContentType/{id}")]
        public async Task<IActionResult> DeleteContentType(int id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in ContentTypeController/DeleteContentType");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
