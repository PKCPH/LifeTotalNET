using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTotalMaui.Models;

public class Gamematch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<GamematchPlayer> Players { get; set; }

    public MatchState MatchState { get; set; }
    
    public DateTime DateTime { get; set; }
    public Player? Winner { get; set; }
}

public enum MatchState
{
    NotStarted, InProgress, Finished, Cancelled
}


public class GetAllGamematch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<GamematchPlayer> Players { get; set; }

    public MatchState MatchState { get; set; }

    public DateTime DateTime { get; set; }
    public Player? Winner { get; set; }

    public bool IsExpanded { get; set; }
    public string PlayerNames
    {
        get
        {
            return Players != null ? string.Join(" vs ", Players.Select(p => p.Player.Name)) : "Players not available";
        }
    }

    // Method to toggle expansion
    public void ToggleExpansion()
    {
        IsExpanded = !IsExpanded;
    }
}