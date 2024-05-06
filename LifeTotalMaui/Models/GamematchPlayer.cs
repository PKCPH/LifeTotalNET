using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTotalMaui.Models;

public class GamematchPlayer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid GameMatchId { get; set; }
    public Gamematch Gamematch { get; set; }
    public Player Player { get; set; }

    public Guid PlayerId { get; set; }
    public int? LifeTotal { get; set; }
}
