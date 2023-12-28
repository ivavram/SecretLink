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
            var game = await dbSet.Where(g => g.GameTag == game_tag).FirstOrDefaultAsync();
            return game!;  
        }

        public async Task<Player> GetGameWinner(int gameID)
        {
            var game = await dbSet.FindAsync(gameID);
            if(game != null)
                return game!.GameWinner!;
            else return null!;
        }

     
    }
    
}