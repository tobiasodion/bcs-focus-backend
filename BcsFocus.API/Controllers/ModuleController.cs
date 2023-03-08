using BcsFocus.API.Models;
using BcsFocus.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BcsFocus.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly ILogger<ModuleController> _logger;
    private readonly IModuleService _moduleService;

    public ModuleController(ILogger<ModuleController> logger, IModuleService moduleService)
    {
        _logger = logger;
        _moduleService = moduleService;
    }

    [HttpGet]
    public  ActionResult<IEnumerable<Module>> Get()
    {
        return Ok(_moduleService.Get());
    }

    [HttpGet("{id}")]
    public ActionResult<Module> Get(string id)
    {
        var module = _moduleService.Get(id);

        if(module == null){
            return NotFound($"Module with Id = {id} not found");
        }

        return Ok(module);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Module module)
    {
        _moduleService.Create(module);

        return CreatedAtAction(nameof(Get), new {id = module.Id}, module);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Module module)
    {
        var existingModule = _moduleService.Get(id);

        if(existingModule == null){
            return NotFound($"Module with Id = {id} not found");
        }

        _moduleService.Update(id, module);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Put(string id)
    {
        var module = _moduleService.Get(id);

        if(module == null){
            return NotFound($"Module with Id = {id} not found");
        }

        _moduleService.Remove(module.Id);

        return NoContent();
    }
}
