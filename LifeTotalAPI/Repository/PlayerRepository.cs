using LifeTotalAPI.Data;
using LifeTotalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LifeTotalAPI.Repository;

public class PlayerRepository
{
    private readonly DatabaseContext _context;

    public PlayerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<Player> GetPlayerById(Guid id)
    {
        return await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Player> AddPlayer(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }

    public async Task<Player> UpdatePlayer(Guid id, Player player)
    {
        var existingPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        if (existingPlayer != null)
        {
            existingPlayer.Name = player.Name;
            existingPlayer.Elo = player.Elo;

            await _context.SaveChangesAsync();
        }
        return existingPlayer;
    }

    public async Task<bool> DeletePlayer(Guid id)
    {
        var existingPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        if (existingPlayer != null)
        {
            _context.Players.Remove(existingPlayer);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
