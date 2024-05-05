using LifeTotalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeTotalAPI.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Gamematch> Gamematches { get; set; }
    public DbSet<User> Users{ get; set; }
    public DbSet<GamematchPlayer> GamematchPlayers { get; set; }
}
