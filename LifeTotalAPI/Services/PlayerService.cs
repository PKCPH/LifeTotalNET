namespace LifeTotalAPI.Services;

using LifeTotalAPI.Models;
using LifeTotalAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerService
{
    private readonly PlayerRepository _playerRepository;

    public PlayerService(PlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
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
        return await _playerRepository.AddPlayer(player);
    }

    public async Task<Player> UpdatePlayer(Guid id, Player player)
    {
        return await _playerRepository.UpdatePlayer(id, player);
    }

    public async Task<bool> DeletePlayer(Guid id)
    {
        return await _playerRepository.DeletePlayer(id);
    }
}
