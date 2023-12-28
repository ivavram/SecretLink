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
        public async Task<int> GetColumn(int c4ID)
        {
            var c4 = await dbSet.FindAsync(c4ID);
            if(c4 != null)
                return c4.Column;
            return -1;
        }

        public async Task<int> GetRow(int c4ID)
        {
            var c4 = await dbSet.FindAsync(c4ID);
            if(c4 != null)
                return c4.Row;
            return -1;
        }

        public async Task<Player> GetWinner(int c4ID)
        {
            var c4 = await dbSet
                .Include(c => c.Winner)
                .Where(c => c.ID == c4ID)
                .FirstOrDefaultAsync();

            return c4!.Winner!;
        }
    }
}