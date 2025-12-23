using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Data;
using server_side.Models;
using server_side.Models.DTOs;

namespace server_side.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DrawingController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DrawingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /api/drawing
        [HttpGet]
        public async Task<IActionResult> GetAllDrawings()
        {
            var drawings = await _dbContext.Drawings.ToListAsync();

            var drawingDtos = drawings.Select(d => new DrawingDto
            {
                Id = d.Id,
                DrawingName = d.DrawingName,
                CreatedAt = d.CreatedAt,
                Rating = d.Rating,
                ImageUrl = d.ImageUrl,
            }).ToList();

            return Ok(drawingDtos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] DrawingCreateDto dto)
        {
            var drawing = new Drawing
            {
                UserId = dto.UserId,
                ChallengeId = dto.ChallengeId,
                DrawingName = dto.DrawingName,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = "" 
            };

            _dbContext.Drawings.Add(drawing);
            await _dbContext.SaveChangesAsync(); 

            var fileName = $"drawing_{drawing.Id}_{DateTime.UtcNow.Ticks}.png";
            var filePath = Path.Combine("wwwroot", "drawings", fileName);

            var base64 = dto.ImageDataUrl.Split(',')[1];
            var bytes = Convert.FromBase64String(base64);
            await System.IO.File.WriteAllBytesAsync(filePath, bytes);

            var imageUrl = $"{Request.Scheme}://{Request.Host}/drawings/{fileName}";

            drawing.ImageUrl = imageUrl;
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                drawing.Id,
                drawing.DrawingName,
                drawing.ImageUrl
            });
        }
        [Authorize]

        // GET /api/drawing/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDrawingsByUser(int userId)
        {
            var drawings = await _dbContext.Drawings
                .Where(d => d.UserId == userId)
                .ToListAsync();

            var drawingDtos = drawings.Select(d => new DrawingDto
            {
                Id = d.Id,
                DrawingName = d.DrawingName,
                CreatedAt = d.CreatedAt,
                Rating = d.Rating,
                ImageUrl = d.ImageUrl,
            }).ToList();

            return Ok(drawingDtos);
        }

        [Authorize]
        // GET /api/drawing/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrawingById(int id)
        {
            var drawing = await _dbContext.Drawings.FindAsync(id);
            if (drawing == null)
                return NotFound();

            return Ok(new
            {
                drawing.Id,
                drawing.DrawingName,
                drawing.ImageUrl,
                drawing.UserId,
                drawing.ChallengeId,
                drawing.CreatedAt
            });
        }
        [Authorize]
        // DELETE /api/drawing/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrawing(int id)
        {
            var drawing = await _dbContext.Drawings.FindAsync(id);
            if (drawing == null)
                return NotFound();

            // מחיקת קובץ התמונה מהשרת
            if (!string.IsNullOrEmpty(drawing.ImageUrl))
            {
                var fileName = Path.GetFileName(new Uri(drawing.ImageUrl).LocalPath);
                var filePath = Path.Combine("wwwroot", "drawings", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _dbContext.Drawings.Remove(drawing);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
