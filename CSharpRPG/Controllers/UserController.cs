using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactSharpRPG.Models; // Your existing models
using System.Threading.Tasks;
using BCrypt.Net;
using ReactSharpRPG.Data;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;

        public UserController(MongoDbContext context)
        {
            _users = context.Users;
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            // Check if the user already exists
            var existingUser = await _users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Create a new user
            var newUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = hashedPassword
            };

            // Save the user to the database
            await _users.InsertOneAsync(newUser);

            return Ok(new { message = "User registered successfully" });
        }
    }
}

