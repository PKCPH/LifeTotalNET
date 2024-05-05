using LifeTotalAPI.Data;
using LifeTotalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LifeTotalAPI.Repository;

public class GamematchRepository
{
    private readonly DatabaseContext _context;

    public GamematchRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Gamematch>> GetAllGamematches()
    {
        return await _context.Gamematches.ToListAsync();
    }

    public async Task<Gamematch> GetGamematchById(Guid id)
    {
        return await _context.Gamematches
            .Include(g => g.Players)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Gamematch> AddGamematch(Gamematch gamematch)
    {
        _context.Gamematches.Add(gamematch);
        await _context.SaveChangesAsync();
        return gamematch;
    }

    public async Task<Gamematch> UpdateGamematch(Guid id, Gamematch gamematch)
    {
        var existingGamematch = await _context.Gamematches.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGamematch != null)
        {
            existingGamematch.MatchState = gamematch.MatchState;
            existingGamematch.DateTime = gamematch.DateTime;
            existingGamematch.Winner = gamematch.Winner;

            await _context.SaveChangesAsync();
        }
        return existingGamematch;
    }

    public async Task<bool> DeleteGamematch(Guid id)
    {
        var existingGamematch = await _context.Gamematches.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGamematch != null)
        {
            _context.Gamematches.Remove(existingGamematch);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
