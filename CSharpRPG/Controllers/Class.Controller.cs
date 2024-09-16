using Microsoft.AspNetCore.Mvc;
using CSharpRPG.Models;
using CSharpRPG.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Controllers
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

        // POST: api/class/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateClassesBulk([FromBody] IEnumerable<Class> classes)
        {
            foreach (var classEntity in classes)
            {
                await _classService.CreateClassAsync(classEntity);
            }
            return Ok("Bulk classes created successfully.");
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

        [HttpPut("bulk")]
        public async Task<ActionResult> UpdateClassesBulk([FromBody] IEnumerable<Class> updatedClasses)
        {
            foreach (var classEntity in updatedClasses)
            {
                var result = await _classService.UpdateClassAsync(classEntity.Id, classEntity);
                if (!result)
                {
                    // Return NotFound if any class is not found, you can also choose to continue updating the rest
                    return NotFound($"Class with ID {classEntity.Id} not found.");
                }
            }
            return Ok("Bulk classes updated successfully.");
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
