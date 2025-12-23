using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Data;
using server_side.Models;
using server_side.Models.DTOs;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TopicController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // GET /api/topic/random
    [HttpGet("random")]
    public async Task<ActionResult<TopicDto>> GetRandomTopic()
    {
        var topic = await _dbContext.Topics
            .OrderBy(t => Guid.NewGuid())
            .Select(t => new TopicDto
            {
                Id = t.Id,
                Name = t.Name
            })
            .FirstOrDefaultAsync();

        if (topic == null)
            return NotFound();

        return Ok(topic);
    }

    // GET /api/topic/by-name/{name}
    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<int>> GetTopicIdByName(string name)
    {
        var topicId = await _dbContext.Topics
            .Where(t => t.Name == name)
            .Select(t => t.Id)
            .FirstOrDefaultAsync();

        if (topicId == 0)
            return NotFound();

        return Ok(topicId);
    }


    // POST /api/topic
    [HttpPost]
    public async Task<IActionResult> CreateTopic([FromBody] TopicCreateDto newTopic)
    {
        if (string.IsNullOrWhiteSpace(newTopic.Name))
            return BadRequest("Topic name is required.");

        var topic = new Topic
        {
            Name = newTopic.Name
        };

        _dbContext.Topics.Add(topic);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRandomTopic), new { id = topic.Id }, topic);
    }

}
