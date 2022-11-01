using Automation.Core.Entities;
using Automation.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutomationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private readonly IService<Money> _service;
    
    public MainController(IService<Money> service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}