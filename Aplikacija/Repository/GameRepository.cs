using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{
     public class GameRepository : BaseRepository<Game>, IGameRepo
     {
        public GameRepository(Context context) : base(context) {}

        public override void Create(Game entity)
        {
            base.Create(entity);
        }
        public override void Update(Game entity)
        {
            base.Update(entity);
        }
        public override void Delete(int id)
        {
            base.Delete(id);
        }
        public override Task<IEnumerable<Game>> GetAll()
        {
            return base.GetAll();
        }
        public override Task<Game> GetById(int id)
        {
            return base.GetById(id);
        }
        public async Task<Game> GetGamePlayers(int gameID)
        {
            var pl = await dbSet
                        .Include(p => p.PlayerInGame)
                        .FirstOrDefaultAsync(d => d.ID == gameID);
            return pl!;
        }

        public async Task<string> GetGameTag(int gameID)
        {
            var game = await dbSet.FindAsync(gameID);
            if( game != null)
                return game.GameTag!;
            
            return "Ne postoji igra sa unesenim tagom!";
        }

        public async Task<Game> GetGameByTag(string game_tag)
        {
            var game = await dbSet
                    .Include(g => g.GuessTheWord)  // Učitavanje GuessTheWord entiteta
                        .ThenInclude(gw => gw!.Word)
                    //.Include(p => p.Connect4Games)
                    .FirstOrDefaultAsync(g => g.GameTag == game_tag);
            Console.WriteLine("repo: " + game!.GuessTheWord);
            return game!;  
        }

        public async Task<Player> GetGameWinner(int gameID)
        {
            var game = await dbSet.FindAsync(gameID);
            if(game != null)
                return game!.GameWinner!;
            else return null!;
        }

        public async Task<Game> GetPublicGame()
        {
            var public_game = await dbSet.Where(game => game.NumOfPlayers < 2 &&
                                                !game.PublicPrivate && 
                                                game.GameStatus)
                                        .FirstOrDefaultAsync();
            return public_game!;
        }

        public async Task<Game> GetConnect4Games(int gameID)
        {
            var game = await dbSet.Include(p=> p.Connect4Games!)
                                    .ThenInclude(k => k.Winner)
                                    .FirstOrDefaultAsync(u => u.ID == gameID &&
                                    u.Connect4Games!.Any(c => c.Winner == null));
            return game!;
        }
    }
    
}