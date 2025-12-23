using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using server_side.Data;
using server_side.Entities;
using server_side.Models.DTOs;
using server_side.Services;

namespace server_side.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public readonly UserService _userService;
        private readonly Random _random = new();

        public ChallengeController(AppDbContext dbContext, UserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        

        // GET /api/challenge/random
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomChallenge()
        {
            var topicCount = await _dbContext.Topics.CountAsync();
            var shapeCount = await _dbContext.Shapes.CountAsync();

            if (topicCount == 0 || shapeCount == 0)
                return NotFound("אין נושאים או צורות במסד הנתונים");

            var topicIndex = _random.Next(topicCount);
            var shapeIndex = _random.Next(shapeCount);

            var topic = await _dbContext.Topics
                .Skip(topicIndex)
                .Select(t => new { t.Id, t.Name })
                .FirstOrDefaultAsync();

            var shape = await _dbContext.Shapes
                .Skip(shapeIndex)
                .Select(s => new { s.Id, s.Code })
                .FirstOrDefaultAsync();

            var userId = _userService.GetCurrentUserId();
            var user = await _dbContext.Users.FindAsync(userId);

            var challenge = new Challenge { TopicId = topic.Id, ShapeId = shape.Id, ComplexityLevel = 2, Creator = user};

            _dbContext.Challenges.Add(challenge);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                challenge.Id,
                Shape = shape.Code,
                Topic = topic.Name,
                challenge.ComplexityLevel,
                CreatorId = userId
            });
        }

        // GET /api/challenge/random-shape
        [HttpGet("random-shape")]
        public async Task<IActionResult> GetRandomShape()
        {
            var count = await _dbContext.Shapes.CountAsync();
            if (count == 0)
                return NotFound();

            var index = _random.Next(count);
            var shape = await _dbContext.Shapes
                .Skip(index)
                .Select(s => new { s.Id, s.Code })
                .FirstOrDefaultAsync();

            return Ok(shape);
        }

        // GET /api/challenge/random-topic
        [HttpGet("random-topic")]
        public async Task<IActionResult> GetRandomTopic()
        {
            var count = await _dbContext.Topics.CountAsync();
            if (count == 0)
                return NotFound();

            var index = _random.Next(count);
            var topic = await _dbContext.Topics
                .Skip(index)
                .Select(t => new { t.Id, t.Name })
                .FirstOrDefaultAsync();

            return Ok(topic);
        }


        // PUT /api/challenge/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallenge(int id, [FromBody] UpdateChallengeDto dto)
        {
            var challenge = await _dbContext.Challenges.FindAsync(id);
            if (challenge == null)
                return NotFound();

            challenge.TopicId = dto.TopicId;
            challenge.ShapeId = dto.ShapeId;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
