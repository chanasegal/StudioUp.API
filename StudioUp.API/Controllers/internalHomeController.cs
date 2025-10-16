using Microsoft.AspNetCore.Mvc;
using StudioUp.DTO;
//using StudioUp.Models.Migrations;
using StudioUp.Repo.IRepositories;

namespace StudioUp.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class internalHomeController : ControllerBase
    {
        readonly IInternalHomeLinksRepository HomeService;
       private readonly ILogger<internalHomeController> _logger;

        public internalHomeController(IInternalHomeLinksRepository HomeService, ILogger<internalHomeController> logger)
        {
            this.HomeService = HomeService;
            _logger = logger;
        }
        [HttpGet("GetAllLinks")]
        public async Task<ActionResult<List<InternalHomeLinksDTO>>> GetAllLinks()
        {
            try
            {
                var link = await HomeService.GetAllLinks();
                return Ok(link);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in internalHomeController/GetAll");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addLink")]
        
        public async Task<ActionResult<InternalHomeLinksDTO>> add(InternalHomeLinksDTO link)
        {
            if (link == null)
            {
                return BadRequest("The content field is null.");
            }
            try
            {
                var l = await HomeService.AddAsync(link);
                return Ok(l);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in internalHomeController/add");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetLinkById/{id}")]
        public async Task<ActionResult<InternalHomeLinksDTO>> getById(int id)
        {
            try
            {
                var link = await HomeService.GetLinkById(id);
                if (link == null)
                {
                    return NotFound("content link not found by ID");

                }
                return Ok(link);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in internalHomeController/getById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(int id, InternalHomeLinksDTO link)
        {
            if (link == null)
            {
                return BadRequest("The link field is null.");
            }
            if (id != link.ID)
            {
                _logger.LogError("cant update in InternalHomeLinksController/Update");
                return BadRequest();
            }
            try
            {
                await HomeService.Update(link);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in InternalHomeLinksController/Update");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await HomeService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in InternalHomeLinksController/Delete");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}

