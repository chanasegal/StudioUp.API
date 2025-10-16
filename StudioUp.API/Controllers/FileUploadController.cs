using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudioUp.DTO;
using StudioUp.Repo.IRepositories;
using System.Linq;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadRepository _fileUploadRepository;
        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(IFileUploadRepository fileUploadRepository, ILogger<FileUploadController> logger)
        {
            _fileUploadRepository = fileUploadRepository;
            _logger = logger;
        }

        [HttpGet("GetFileById/{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            try
            {
                var fileDownload = await _fileUploadRepository.GetFileAsync(id);
                if (fileDownload == null)
                    return NotFound("File not found ");
                return File(fileDownload.Data, fileDownload.ContentType, fileDownload.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in FileUploadController/GetFileById");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPost("UploadFileAndReturnFile")]
        public async Task<ActionResult> UploadFileAndReturnFile([FromForm] FileUploadDTO fileUploadDTO)
        {
            try
            {
                var validationResult = ValidateFile(fileUploadDTO.File);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.ErrorMessage);

                var fileId = await SaveFileAsync(fileUploadDTO.File);

                var fileResult = await _fileUploadRepository.GetFileAsync(fileId);
                if (fileResult == null)
                    return NotFound("File not found.");

                return File(fileResult.Data, fileResult.ContentType, fileResult.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FileUploadController/UploadFileAndReturnFile");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("UploadFileAndReturnId")]
        public async Task<ActionResult> UploadFileAndReturnId([FromForm] FileUploadDTO fileUploadDTO)
        {
            try
            {
                var validationResult = ValidateFile(fileUploadDTO.File);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.ErrorMessage);

                var fileId = await SaveFileAsync(fileUploadDTO.File);
                return Ok(new { id = fileId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FileUploadController/UploadFileAndReturnId");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private (bool IsValid, string ErrorMessage) ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (false, "File not selected");

            var permittedExtensions = new[] { ".pdf", ".png", ".jpg" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return (false, "Invalid file type");

            const long maxFileSize = 2 * 1024 * 1024;
            if (file.Length > maxFileSize)
                return (false, "File size exceeds the limit of 2 MB");

            return (true, null);
        }

        private async Task<int> SaveFileAsync(IFormFile file)
        {
            return await _fileUploadRepository.AddFileAsync(file);
        }
        
        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            try
            {
                await _fileUploadRepository.DeleteFileAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this error in FileUploadController/DeleteFile");
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }


    }
}
