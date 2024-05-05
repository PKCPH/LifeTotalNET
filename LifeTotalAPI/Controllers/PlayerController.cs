using LifeTotalAPI.Models;
using LifeTotalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeTotalAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
    {
        var players = await _playerService.GetAllPlayers();
        return Ok(players);
    }

    [HttpGet("Top20")]
    public async Task<ActionResult<IEnumerable<Player>>> GetTopPlayers()
    {
        var players = await _playerService.GetAllPlayers();
        var topPlayers = players.OrderByDescending(x => x.Elo)
                                .Take(20)                 
                                .ToList();                 

        return Ok(topPlayers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayerById(Guid id)
    {
        var player = await _playerService.GetPlayerById(id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);
    }

    [HttpPost]
    public async Task<ActionResult<Player>> AddPlayer(Player player)
    {
        var newPlayer = await _playerService.AddPlayer(player);
        return CreatedAtAction(nameof(GetPlayerById), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(Guid id, Player player)
    {
        var updatedPlayer = await _playerService.UpdatePlayer(id, player);
        if (updatedPlayer == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(Guid id)
    {
        var result = await _playerService.DeletePlayer(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
