using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudioUp.Models;
using StudioUp.Repo;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudioUp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IConfiguration configuration, ICustomerRepository iCustomerRepository, ILogger<AuthController> logger)
        {
            _iCustomerRepository = iCustomerRepository;
            _configuration = configuration;
            _logger = logger;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var cust = await _iCustomerRepository.GetCustomerByEmailAndPassword(login.Email, login.Password);
                if (cust is not null)
                {
                    var claims = new List<Claim>()
                {
                new Claim("Id", cust.Id.ToString()),
                new Claim("Name", cust.FirstName+" "+cust.LastName),
                new Claim("CustomerType", cust.CustomerTypeId.ToString()),
                new Claim("HMOId", cust.HMOId.ToString()),
                new Claim("PaymentOptionId", cust.PaymentOptionId.ToString()),
                new Claim("SubscriptionTypeId", cust.SubscriptionTypeId.ToString()),
                new Claim("IsActive", cust.IsActive.ToString()),
                new Claim("phone", cust.Tel),
                new Claim("Addree", cust.Address),
                new Claim("Email", cust.Email)
                 };

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: _configuration.GetValue<string>("JWT:Issuer"),
                        audience: _configuration.GetValue<string>("JWT:Audience"),
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred" });
            }
        }

        [HttpPost("loginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] loginWithGoogleModel email)
        {
            try
            {
                if (string.IsNullOrEmpty(email.Email))
                {
                    return BadRequest(new { message = "Email cannot be empty" });
                }

                var cust = await _iCustomerRepository.GetCustomerByEmail(email.Email);
                if (cust is not null)
                {
                    var claims = new List<Claim>()
            {
                new Claim("Id", cust.Id.ToString()),
                new Claim("Name", cust.FirstName + " " + cust.LastName),
                new Claim("CustomerType", cust.CustomerTypeId.ToString()),
                new Claim("HMOId", cust.HMOId.ToString()),
                new Claim("PaymentOptionId", cust.PaymentOptionId.ToString()),
                new Claim("SubscriptionTypeId", cust.SubscriptionTypeId.ToString()),
                new Claim("IsActive", cust.IsActive.ToString()),
                new Claim("phone", cust.Tel),
                new Claim("Addree", cust.Address),
                new Claim("Email", cust.Email)
            };

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: _configuration.GetValue<string>("JWT:Issuer"),
                        audience: _configuration.GetValue<string>("JWT:Audience"),
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return Unauthorized(new { message = "Customer not found" });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred" });
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] loginWithGoogleModel email)
        {
            try
            {
                var password = await _iCustomerRepository.GetPasswordrByEmail(email.Email);
                if (password == null)
                    return NotFound();
                return Ok(password);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception occurred: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred" });
            }
        }


        [Authorize]
        [HttpPost("check-token")]
        public IActionResult CheckToken()
        {
            return Ok(new { valid = true });
        }
    }

}