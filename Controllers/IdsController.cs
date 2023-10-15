using Microsoft.AspNetCore.Mvc;
using IdProvider.Models;
using IdProvider.Interfaces;

namespace IdProvider.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class IdsController : Controller
{
    private IIdsService _idsService;

    public IdsController(IIdsService idsService)
    {
        _idsService = idsService;
    }

    [HttpGet("next/{prefix}")]
    public async Task<IActionResult> GetNewIdByPrefix(string prefix)
    {
        NewIdResult idResult = await _idsService.GetNewIdByPrefix(prefix);

        if (idResult == null)
        {
            return NotFound($"Could not find any id for prefix {prefix}");
        }

        return Ok(idResult);
    }

    [HttpGet("current/{prefix}")]
    public async Task<IActionResult> GetCurrentIdByPrefix(string prefix)
    {
        CurrentIdResult idResult = await _idsService.GetCurrentIdByPrefix(prefix);

        if (idResult == null)
        {
            return NotFound($"Could not find any id for prefix {prefix}");
        }

        return Ok(idResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IdCreation newId)
    {
        await _idsService.Create(newId);

        return Ok(new { message = "New id created" });
    }
}