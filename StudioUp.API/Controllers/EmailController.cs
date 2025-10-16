using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.ToAddress) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
        {
            return BadRequest("Invalid email request.");
        }

        try
        {
            await _emailService.SendEmailAsync(request.ToAddress, request.Subject, request.Body);
            Console.WriteLine($"Sending email to: {request.ToAddress}");
            Console.WriteLine($"Subject: {request.Subject}");
            Console.WriteLine($"Body: {request.Body}");
            return Ok("Email sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }

    public class EmailRequest
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}


