using LifeTotalAPI.Models;
using LifeTotalAPI.Repository;

namespace LifeTotalAPI.Services;

public class GamematchService
{
    private readonly GamematchRepository _gamematchRepository;

    public GamematchService(GamematchRepository gamematchRepository)
    {
        _gamematchRepository = gamematchRepository;
    }

    public async Task<IEnumerable<Gamematch>> GetAllGamematches()
    {
        return await _gamematchRepository.GetAllGamematches();
    }

    public async Task<Gamematch> GetGamematchById(Guid id)
    {
        return await _gamematchRepository.GetGamematchById(id);
    }

    public async Task<Gamematch> AddGamematch(Guid player1, Guid player2)
    {
        Gamematch gamematch = new()
        {
            Id = Guid.NewGuid(),
            DateTime = DateTime.Now,
            MatchState = MatchState.NotStarted,
            Players = new List<GamematchPlayer>()
        };
        GamematchPlayer gamematchPlayer1 = new()
        {
            Id = Guid.NewGuid(),
            GameMatchId = gamematch.Id,
            PlayerId = player1
        };
        GamematchPlayer gamematchPlayer2 = new()
        {
            Id = Guid.NewGuid(),
            GameMatchId = gamematch.Id,
            PlayerId = player2
        };
        gamematch.Players.Add(gamematchPlayer1);
        gamematch.Players.Add(gamematchPlayer2);

        return await _gamematchRepository.AddGamematch(gamematch);
    }

    public async Task<Gamematch> UpdateGamematch(Guid id, Gamematch gamematch)
    {
        return await _gamematchRepository.UpdateGamematch(id, gamematch);
    }

    public async Task<bool> DeleteGamematch(Guid id)
    {
        return await _gamematchRepository.DeleteGamematch(id);
    }
}
