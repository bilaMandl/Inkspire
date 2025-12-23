using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Data;
using server_side.Models;
using server_side.Models.DTOs;

namespace server_side.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ShapeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET /api/shape/random
        [HttpGet("random")]
        public async Task<ActionResult<ShapeDto>> GetRandomShape()
        {
            var shape = await _dbContext.Shapes
                .OrderBy(s => Guid.NewGuid())
                .Select(s => new ShapeDto
                {
                    Id = s.Id,
                    Code = s.Code
                })
                .FirstOrDefaultAsync();

            if (shape == null)
                return NotFound();

            return Ok(shape);
        }

        // GET /api/shape/by-code/{code}
        [HttpGet("by-code/{code}")]
        public async Task<ActionResult<int>> GetShapeIdByCode(string code)
        {
            var shapeId = await _dbContext.Shapes
                .Where(s => s.Code == code)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();

            if (shapeId == 0) 
                return NotFound();

            return Ok(shapeId);
        }

        // POST /api/shape
        [HttpPost]
        public async Task<IActionResult> CreateShape([FromBody] ShapeCreateDto shape)
        {
            if (shape == null || string.IsNullOrWhiteSpace(shape.Code))
                return BadRequest("Invalid shape data");
            var newShape = new Shape { Code = shape.Code };
            _dbContext.Shapes.Add(newShape);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRandomShape), new { id = newShape.Id }, shape);
        }
    }
}
