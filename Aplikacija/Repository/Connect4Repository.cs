using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{


    public class Connect4Repository : BaseRepository<Connect4Game>, IConnect4Repo
    {
        public Connect4Repository(Context context) : base(context){}

        public override void Create(Connect4Game entity)
        {
            base.Create(entity);
        }

        public override void Update(Connect4Game entity)
        {
            base.Update(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<Connect4Game> GetById(int id)
        {
            return base.GetById(id);
        }
        
        public override Task<IEnumerable<Connect4Game>> GetAll()
        {
            return base.GetAll();
        }

        public async Task<Player> GetWinner(int c4ID)
        {
            var c4 = await dbSet
                .Include(c => c.Winner)
                .Where(c => c.ID == c4ID)
                .FirstOrDefaultAsync();

            return c4!.Winner!;
        }
        //public async Task<Connect4Game> GetConnect4Game(int c4ID)
        //{            var game = await dbSet.Where(p => p. == c4ID && p.Winner == null);

        //}
    }
}