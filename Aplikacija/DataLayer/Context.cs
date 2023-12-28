namespace Models;

public class Context : DbContext
{
    public required DbSet<Game> Games { get; set; }
    public required DbSet<Connect4Game> Connect4Games { get; set; }
    public required DbSet<GuessTheWordGame> GuessTheWordGames { get; set; }
    public required DbSet<Player> Players { get; set; }
    public required DbSet<Word> Words { get; set; }
    public required DbSet<PlayerInGame> PlayerInGames { get; set; }

    public Context(DbContextOptions options) : base(options) {}

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.Entity<PlayerInGame>()
                    .HasKey(nameof(PlayerInGame.GameID), nameof(PlayerInGame.PlayerID));
    }
}
