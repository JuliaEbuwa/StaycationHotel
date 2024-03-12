using Microsoft.AspNetCore.Mvc;
using StaycationHotel.Models;

namespace StaycationHotel.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            var model = new LoginViewModel
            {
                User = new User()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            var newUser = await _userRepository.AddUserAsync(user);
            var model = new LoginViewModel
            {
                User = new User()
            };
            if (newUser == Models.Response.Success)
            {
                model.Message = "your registration was successful! ";
            }
            else
            {
                
                model.Message = " Account already exist, try again ";
            }

            return View(model);

            // RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            var model = new LoginViewModel
            {
                User = new User()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(User user)
        {
            var loggedinUser = await _userRepository.LoginUser(user.Email, user.Password);
            var model = new LoginViewModel
            {
                User = new User()
            };
            if (loggedinUser != null)
            {
                model.Message = "Welcome " + loggedinUser.FirstName + ", you are now logged in";
            }
            else
            {
                model.Message = "Login Failed";
            }

            return View(model);

        }
    }
}
