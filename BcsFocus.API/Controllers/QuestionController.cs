using AutoMapper;
using BcsFocus.API.DTO;
using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcsFocus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly ILogger<QuestionController> _logger;
    private readonly IMapper _mapper;
    private readonly IQuestionService _questionService;

    public QuestionController(ILogger<QuestionController> logger, IMapper mapper, IQuestionService questionService)
    {
        _mapper = mapper;
        _logger = logger;
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> Get([FromQuery] int p=1, 
                                                   [FromQuery] int limit=10, 
                                                   [FromQuery] bool f=true, 
                                                   [FromQuery] string? t=null)
    {
        var questions = await _questionService.GetAll(t,p,limit,f);
        return Ok(questions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> Get(string id, [FromQuery] string? f = null)
    {
        Question question;

        if (f != null)
        {
            question = await _questionService.GetQuestionPoint(id, f);
        }
        else
        {
            question = _questionService.Get(id);
        }


        if (question == null)
        {
            return NotFound($"Question with Id = {id} not found");
        }
        return Ok(question);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Question question)
    {
        _questionService.Create(question);

        return CreatedAtAction(nameof(Get), new { id = question.Id }, question);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Question question)
    {
        var existingQuestion = _questionService.Get(id);

        if (existingQuestion == null)
        {
            return NotFound($"Question with Id = {id} not found");
        }

        _questionService.Update(id, question);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Put(string id)
    {
        var question = _questionService.Get(id);

        if (question == null)
        {
            return NotFound($"Question with Id = {id} not found");
        }

        _questionService.Remove(question.Id);

        return NoContent();
    }

    [HttpGet("topics")]
    public async Task<ActionResult<List<Topic>>> GetQuestionsTopics()
    {
        var topics = await _questionService.GetAllTopics();
        return Ok(topics);
    }

    [HttpGet("{id}/topics")]
    public async Task<ActionResult<List<Topic>>> GetQuestionTopics(string id)
    {
        var topics = await _questionService.GetQuestionTopics(id);
        return Ok(topics);
    }
}
