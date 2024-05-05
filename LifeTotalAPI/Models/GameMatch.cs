using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTotalAPI.Models;

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
