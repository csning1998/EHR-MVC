using EHR_MVC.Services;
using EHR_MVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using EHR_MVC.Models.Users;
using System.Reflection.Metadata.Ecma335;
using EHR_MVC.Models.Patient;

namespace EHR_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;

        public UserController(UserService userService, UserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

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

                return Ok(new { 
                    StatusCode = 200, 
                    Message = "Login successful.",
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
    }
}