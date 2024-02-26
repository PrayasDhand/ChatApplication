using ChatApplication.Models;
using ChatApplication.Repositories.Interfaces;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChatApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly FirebaseAuthProvider _authProvider;

        public UsersController(IUserRepository userRepository, FirebaseAuthProvider authProvider)
        {
            _userRepository = userRepository;
            _authProvider = authProvider;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users user)
        {
            try
            {
                // Validate user input
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Create user in Firebase Authentication
                var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password);

                // Save user to SQL Server
                var addedUser = await _userRepository.AddUserAsync(user);

                return Ok(new { addedUser.Id, addedUser.Email }); // Return user details
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users user)
        {
            try
            {
                // Validate user input
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Authenticate user with Firebase Authentication
                var auth = await _authProvider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);

                // You can perform additional actions here if needed, such as generating tokens, etc.

                return Ok(new { UserId = auth.User.LocalId, Email = auth.User.Email }); // Return user details
            }
            catch (FirebaseAuthException ex)
            {
                // Handle Firebase Authentication errors
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, ex.Message);
            }
        }
    }
}
