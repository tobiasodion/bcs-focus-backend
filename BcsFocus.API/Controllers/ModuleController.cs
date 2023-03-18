using AutoMapper;
using BcsFocus.API.DTO;
using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcsFocus.API.Controllers;

[ApiController]
[Route("api/modules")]
public class ModuleController : ControllerBase
{
    private readonly ILogger<ModuleController> _logger;
    private readonly IMapper _mapper;
    private readonly IModuleService _moduleService;

    public ModuleController(ILogger<ModuleController> logger, IMapper mapper, IModuleService moduleService)
    {
        _mapper = mapper;
        _logger = logger;
        _moduleService = moduleService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Module>> GetModules([FromQuery] string? t = null)
    {
         List<Module> modules;

        if (t != null)
        {
            modules = _moduleService.GetByTopic(t);
        }
        else
        {
            modules = _moduleService.Get();
        }
        
        var response = _mapper.Map<List<GetModulesResponse>>(modules);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<Module> GetModuleById(string id)
    {
        var module = _moduleService.Get(id);

        if (module == null)
        {
            return NotFound($"Module with Id = {id} not found");
        }

        var response = _mapper.Map<GetModulesResponse>(module);
        return Ok(response);
    }

    [HttpPost]
    public ActionResult PostModule([FromBody] Module module)
    {
        _moduleService.Create(module);

        return CreatedAtAction(nameof(GetModuleById), new { id = module.Id }, module);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateModule(string id, [FromBody] Module module)
    {
        var existingModule = _moduleService.Get(id);

        if (existingModule == null)
        {
            return NotFound($"Module with Id = {id} not found");
        }

        _moduleService.Update(id, module);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteModule(string id)
    {
        var module = _moduleService.Get(id);

        if (module == null)
        {
            return NotFound($"Module with Id = {id} not found");
        }

        _moduleService.Remove(module.Id);

        return NoContent();
    }

    [HttpGet("topics")]
    public async Task<ActionResult<List<Topic>>> GetModulesTopics()
    {
        var topics = await _moduleService.GetAllTopics();
        return Ok(topics);
    }

    [HttpGet("{id}/topics")]
    public async Task<ActionResult<List<Topic>>> GetModuleTopics(string id)
    {
        var topics = await _moduleService.GetModuleTopics(id);
        return Ok(topics);
    }

    [HttpGet("{id}/questions")]
    public async Task<ActionResult<List<Question>>> GetModuleQuestions(string id, [FromQuery] int p=1, 
                                                                                  [FromQuery] int limit=10, 
                                                                                  [FromQuery] bool f=true, 
                                                                                  [FromQuery] string? t=null)
    {
        var questions = await _moduleService.GetModuleQuestions(id, t, p, limit, f);
        return Ok(questions);
    }
}
