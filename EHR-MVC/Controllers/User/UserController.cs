using EHR_MVC.Services;
using EHR_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using EHR_MVC.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace EHR_MVC.Controllers.User
{
    public class UserController(UserService userService, IConfiguration configuration) : Controller
    {
        private readonly UserService _userService = userService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null)
                {
                    ModelState.AddModelError("", "Invalid data submitted.");
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data submitted." });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Model validation failed." });
                }

                var userDBModel = _userService.ConvertUserViewModel2DBModel(userViewModel);
                bool dbResult;

                if (userDBModel.UserId == 0)
                {
                    dbResult = await _userService.RegisterUserAsync(userDBModel) > 0;
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, Message = "This email has already been registered." });
                }

                if (dbResult)
                {
                    return Ok(new { StatusCode = 200, Message = "Registration successful!" });
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, Message = "Failed to save patient data." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Status = "Error",
                    ex.Message

                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null || string.IsNullOrWhiteSpace(userViewModel.UserEmail) || string.IsNullOrWhiteSpace(userViewModel.Password))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid input data." });
                }

                var userDBModel = await _userService.GetUserByEmailAsync(userViewModel.UserEmail);
                if (userDBModel == null)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Email not found." });
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(userViewModel.Password, userDBModel.PasswordHashed);

                if (!isPasswordValid)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid password." });
                }

                string token = GenerateJwtToken(userDBModel);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Login successful.",
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Status = "Error",
                    ex.Message
                });
            }
        }

        private string GenerateJwtToken(UserDBModel user)
        {
            // Read JWT configs from appsettings.json
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var expiresMinutes = Convert.ToInt32(_configuration["JwtSettings:ExpiresMinutes"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("UserId", user.UserId.ToString()),
                new("Email", user.UserEmail)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}