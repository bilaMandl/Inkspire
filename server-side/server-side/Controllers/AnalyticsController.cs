using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Data;

namespace server_side.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AnalyticsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET /api/analytics/top-topics
        [HttpGet("top-topics")]
        public async Task<IActionResult> GetTopTopics()
        {
            var count = await _dbContext.Drawings.CountAsync();
            if (count == 0)
                return NotFound("No drawings found.");

            var topTopics = await _dbContext.Drawings
                .Include(d => d.Challenge)
                    .ThenInclude(c => c.Topic)
                .Where(d => d.Challenge != null && d.Challenge.Topic != null)
                .GroupBy(d => d.Challenge.Topic.Name)
                .Select(g => new
                {
                    Topic = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return Ok(topTopics);
        }

        // GET /api/analytics/popular-shapes
        [HttpGet("popular-shapes")]
        public async Task<IActionResult> GetPopularShapes()
        {
            var count = await _dbContext.Drawings.CountAsync();
            if (count == 0)
                return NotFound("No drawings found.");

            var popularShapes = await _dbContext.Drawings
                .Include(d => d.Challenge)
                    .ThenInclude(c => c.Shape)
                .Where(d => d.Challenge != null && d.Challenge.Shape != null)
                .GroupBy(d => d.Challenge.Shape.Code)
                .Select(g => new
                {
                    Shape = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return Ok(popularShapes);
        }

        // GET /api/analytics/user-activity
        [HttpGet("user-activity")]
        public async Task<IActionResult> GetUserActivity()
        {
            var count = await _dbContext.Drawings.CountAsync();
            if (count == 0)
                return NotFound("No drawings found.");

            var activity = await _dbContext.Drawings
                .GroupBy(d => d.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    DrawingsCount = g.Count()
                })
                .OrderByDescending(x => x.DrawingsCount)
                .ToListAsync();

            return Ok(activity);
        }
    }
}