using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{


    public class PlayerRepository : BaseRepository<Player>, IPlayerRepo
    {
        public PlayerRepository(Context context) : base(context){}

        public override void Create(Player entity)
        {
            base.Create(entity);
        }

        public override void Update(Player entity)
        {
            base.Update(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<Player> GetById(int id)
        {
            return base.GetById(id);
        }
        
        public override Task<IEnumerable<Player>> GetAll()
        {
            return base.GetAll();
        }

        public async Task<Player> GetPlyerByUsername(string username)
        {
            var player =  await dbSet.FirstOrDefaultAsync
                             (p =>p.Username == username);
            
            return player!;
            
        }
        public async Task<string> GetPasswordByPlayerUsername(string username)
        {
            var password = await dbSet
                    .Where(p => p.Username == username)
                    .Select(p => p.Password)
                    .FirstOrDefaultAsync();
    
            return password!;
        }
    }
}