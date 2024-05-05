using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LifeTotalMaui.Models;

public class Player
{
    [JsonIgnore]
    public string? Id { get; set; }

    public string Name { get; set; }
    [JsonIgnore]
    public int? Elo { get; set; }

    [JsonIgnore]
    public ICollection<GamematchPlayer>? Gamematches { get; set; }
}
