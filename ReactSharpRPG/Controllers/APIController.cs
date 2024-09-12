using Microsoft.AspNetCore.Mvc;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var message = @"
            User has connected to the ReactSharpRPG API 🎮💻. Welcome, adventurer!

            Available Endpoints:

            - /api/character         => Manage your characters 🧙‍♂️🛡️
            - /api/battle            => Engage in epic battles ⚔️
            - /api/enemy             => Encounter enemies 👾
            - /api/item              => Manage items like swords and potions 🗡️
            - /api/inventory         => Manage character inventories 🎒

            Use Swagger at /swagger to explore and interact with the API endpoints 📄. Enjoy your adventure!
            ";

            return Ok(message);
        }
    }
}