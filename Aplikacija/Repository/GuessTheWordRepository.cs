using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{


    public class GuessTheWordRepository : BaseRepository<GuessTheWordGame>, IGuessTheWordRepo
    {
        public GuessTheWordRepository(Context context) : base(context){}

         public override void Create(GuessTheWordGame entity)
        {
            base.Create(entity);
        }

        public override void Update(GuessTheWordGame entity)
        {
            base.Update(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<GuessTheWordGame> GetById(int id)
        {
            //return base.GetById(id);
            var game = dbSet.Include(g => g.Word).Where(g => g.ID == id)
                        .FirstOrDefaultAsync();
            return game!; 
        }
        
        public override Task<IEnumerable<GuessTheWordGame>> GetAll()
        {
            return base.GetAll();
        }
        public async Task<Word> GetWordInGuessTheWordGame(int guessGameID)
        {
            var game = await dbSet.Include(g => g.Word).Where(g => g.ID == guessGameID)
                .FirstOrDefaultAsync();
            return game!.Word!;
        }
    }
}

