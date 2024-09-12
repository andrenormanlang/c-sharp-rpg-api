using Microsoft.AspNetCore.Mvc;
using ReactSharpRPG.Models;
using ReactSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Controllers
{
    [ApiController]
    [Route("api/class")]  // Set the route to match the singular form
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        // GET: api/class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
        {
            var classes = await _classService.GetClassesAsync();
            return Ok(classes);
        }

        // GET: api/class/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClassById(string id)
        {
            var classEntity = await _classService.GetClassByIdAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }
            return Ok(classEntity);
        }

        // POST: api/class
        [HttpPost]
        public async Task<ActionResult<Class>> CreateClass([FromBody] Class classEntity)
        {
            await _classService.CreateClassAsync(classEntity);
            return CreatedAtAction(nameof(GetClassById), new { id = classEntity.Id }, classEntity);
        }

        // PUT: api/class/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(string id, [FromBody] Class updatedClass)
        {
            var result = await _classService.UpdateClassAsync(id, updatedClass);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/class/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var result = await _classService.DeleteClassAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
