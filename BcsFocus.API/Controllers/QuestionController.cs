using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcsFocus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly ILogger<QuestionController> _logger;
    private readonly IQuestionService _questionService;

    public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService)
    {
        _logger = logger;
        _questionService = questionService;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<Question>> Get()
    {
        return Ok(_questionService.Get());
    }

    [HttpGet("{id}")]
    public ActionResult<Question> Get(string id)
    {
        var question = _questionService.Get(id);

        if(question == null){
            return NotFound($"Question with Id = {id} not found");
        }

        return Ok(question);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Question question)
    {
        _questionService.Create(question);

        return CreatedAtAction(nameof(Get), new {id = question.Id}, question);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Question question)
    {
        var existingQuestion = _questionService.Get(id);

        if(existingQuestion == null){
            return NotFound($"Question with Id = {id} not found");
        }

        _questionService.Update(id, question);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Put(string id)
    {
        var question = _questionService.Get(id);

        if(question == null){
            return NotFound($"Question with Id = {id} not found");
        }

        _questionService.Remove(question.Id);

        return NoContent();
    }
}
