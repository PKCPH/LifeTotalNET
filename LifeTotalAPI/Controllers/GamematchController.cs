using LifeTotalAPI.Models;
using LifeTotalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeTotalAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class GamematchController : ControllerBase
{
    private readonly GamematchService _gamematchService;

    public GamematchController(GamematchService gamematchService)
    {
        _gamematchService = gamematchService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gamematch>>> GetAllGamematches()
    {
        var gamematches = await _gamematchService.GetAllGamematches();
        return Ok(gamematches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Gamematch>> GetGamematchById(Guid id)
    {
        var gamematch = await _gamematchService.GetGamematchById(id);
        if (gamematch == null)
        {
            return NotFound();
        }
        return Ok(gamematch);
    }

    [HttpPost]
    public async Task<ActionResult<Gamematch>> AddGamematch(Guid playerid1, Guid playerid2)
    {
        //if (playerid1 is null || playerid2 is null) return BadRequest("Match must have 2 players.");
        var newGamematch = await _gamematchService.AddGamematch(playerid1, playerid2);
        return CreatedAtAction(nameof(GetGamematchById), new { id = newGamematch.Id }, newGamematch);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGamematch(Guid id, Gamematch gamematch)
    {
        var updatedGamematch = await _gamematchService.UpdateGamematch(id, gamematch);
        if (updatedGamematch == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGamematch(Guid id)
    {
        var result = await _gamematchService.DeleteGamematch(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
