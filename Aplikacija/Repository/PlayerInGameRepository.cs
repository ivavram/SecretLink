using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{


    public class PlayerInGameRepository : BaseRepository<PlayerInGame>, IPlayerInGameRepo
    {

        public PlayerInGameRepository(Context context) : base(context){}

        public override void Create(PlayerInGame entity)
        {
            base.Create(entity);
        }

        public override void Update(PlayerInGame entity)
        {
            base.Update(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<PlayerInGame> GetById(int id)
        {
            return base.GetById(id);
        }
        
        public override Task<IEnumerable<PlayerInGame>> GetAll()
        {
            return base.GetAll();
        }
        public async Task<PlayerInGame> GetByGameIDAndPlayerID(int gameID, int playerID)
        {
            var pg = await dbSet.FirstOrDefaultAsync
                                (p => p.GameID == gameID && p.PlayerID == playerID);
            return pg!;
        }

        public async Task<int> GetGamesWon(int gameID, int playerID)
        {
            var pg = await dbSet.FirstOrDefaultAsync
                                (p => p.GameID == gameID && p.PlayerID == playerID);
            if(pg != null)
                return pg.GamesWon;
            return -1;
        }

        public async Task<List<string>> GetPlayersInGame(int gameID)
        {
            var game = await dbSet.Where(g => g.GameID == gameID).Include(g => g.Player).ToListAsync();
            var usernames = game.Select(p => p.Player!.Username).ToList();
            return usernames;

        }

        public async Task<int> GetGamesWonByPlayer(int gameID, int playerID)
        {
            var games_won = await dbSet.Where(d => d.GameID==gameID && d.PlayerID == playerID).FirstOrDefaultAsync();
            if(games_won != null)
                return games_won.GamesWon;                
            return -1;
        }
    }
}