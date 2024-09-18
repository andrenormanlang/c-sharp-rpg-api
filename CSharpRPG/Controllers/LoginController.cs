using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _configuration; // Inject configuration to access JWT settings

        public LoginController(MongoDbContext context, IConfiguration configuration)
        {
            _users = context.Users;
            _configuration = configuration; // Store configuration for JWT secret
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Find user by email
            var user = await _users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            // Return the JWT token and userId in the response
            return Ok(new
            {
                message = "Login successful",
                token = token,     // Return JWT token
                // TODO discover a way to decode the token to provide the user id
                // Adding for now to allow the front end to use the userId
                userId = user.Id   // Explicitly return the userId
            });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]); // Retrieve JWT secret from configuration

            // Add claims to store user ID and email in the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // Store user ID as a claim
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create and write the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
