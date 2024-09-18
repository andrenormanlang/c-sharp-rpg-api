using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharpRPG.Models; // Your existing models
using System.Threading.Tasks;
using BCrypt.Net;
using CSharpRPG.Data;
using MongoDB.Bson;

namespace CSharpRPG.Controllers
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

        // GET: api/user/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _users.Find(_ => true).ToListAsync(); // Fetch all users from the collection

            // Check if users are available
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users); // Return the list of users
        }


        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            // Convert string to ObjectId
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid user ID format.");
            }

            // Query the database for the user
            var user = await _users.Find(u => u.Id == objectId.ToString()).FirstOrDefaultAsync();

            // Check if user exists
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user); // Return the user data
        }

        
    }
}

