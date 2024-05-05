using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LifeTotalMaui.Models;

public class Player
{
    public string Id { get; set; }

    public string Name { get; set; }
    public int LifeTotal { get; set; }
    public int Elo { get; set; }

    public ICollection<GamematchPlayer>? Gamematches { get; set; }
}
