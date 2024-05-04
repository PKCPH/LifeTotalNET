using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTotalAPI.Models;

public class GameMatch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<Player> Players { get; set; }

    public MatchState MatchState { get; set; }
    
    public DateTime DateTime { get; set; }
    public Player? Winner { get; set; }
}

public enum MatchState
{
    NotStarted, InProgress, Finished, Cancelled
}
