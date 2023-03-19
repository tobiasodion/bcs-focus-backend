using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcsFocus.API.Controllers;

[ApiController]
[Route("api/topics")]
public class TopicController : ControllerBase
{
    private readonly ILogger<TopicController> _logger;
    private readonly ITopicService _topicService;

    public TopicController(ILogger<TopicController> logger, ITopicService topicService)
    {
        _logger = logger;
        _topicService = topicService;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<Topic>> Get()
    {
        return Ok(_topicService.Get());
    }

    [HttpGet("{id}")]
    public ActionResult<Topic> Get(string id)
    {
        var topic = _topicService.Get(id);

        if(topic == null){
            return NotFound($"Topic with Id = {id} not found");
        }

        return Ok(topic);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Topic topic)
    {
        _topicService.Create(topic);

        return CreatedAtAction(nameof(Get), new {id = topic.Id}, topic);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Topic topic)
    {
        var existingTopic = _topicService.Get(id);

        if(existingTopic == null){
            return NotFound($"Topic with Id = {id} not found");
        }

        _topicService.Update(id, topic);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Put(string id)
    {
        var topic = _topicService.Get(id);

        if(topic == null){
            return NotFound($"Topic with Id = {id} not found");
        }

        _topicService.Remove(topic.Id);

        return NoContent();
    }
}
