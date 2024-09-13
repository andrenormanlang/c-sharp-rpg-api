using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Models;
using System.Threading.Tasks;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;

        // Inject MongoDB context
        public LoginController(MongoDbContext context)
        {
            _users = context.Users;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate a token or session here if needed
            return Ok(new { message = "Login successful" });
        }
    }
}
