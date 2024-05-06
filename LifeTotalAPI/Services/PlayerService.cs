namespace LifeTotalAPI.Services;

using LifeTotalAPI.Models;
using LifeTotalAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerService
{
    private readonly PlayerRepository _playerRepository;
    private readonly GamematchRepository _gamematchRepository;

    public PlayerService(PlayerRepository playerRepository, GamematchRepository gamematchRepository)
    {
        _playerRepository = playerRepository;
        _gamematchRepository = gamematchRepository;
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await _playerRepository.GetAllPlayers();
    }

    public async Task<Player> GetPlayerById(Guid id)
    {
        return await _playerRepository.GetPlayerById(id);
    }

    public async Task<Player> AddPlayer(Player player)
    {
        player.Elo = 1200;
        return await _playerRepository.AddPlayer(player);
    }

    public async Task<Player> UpdatePlayer(Guid id, Player player)
    {
        return await _playerRepository.UpdatePlayer(id, player);
    }

    public async Task<bool> DeletePlayer(Guid id)
    {
        var gamematches = _gamematchRepository.GetAllGamematches().Result.ToList();
        var matchesWithPlayer = gamematches
        .Where(match => match.Players.Any(p => p.Player.Id == id))
        .ToList();

        foreach (var match in matchesWithPlayer)
        {
            match.MatchState = MatchState.Cancelled; 
            await _gamematchRepository.UpdateGamematch(match.Id, match);
        }
        return await _playerRepository.DeletePlayer(id);
    }
}
