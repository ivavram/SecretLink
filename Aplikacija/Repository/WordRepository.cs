using Interface.RepositoryInterfaces;
using Models;

namespace Repository
{


    public class WordRepository : BaseRepository<Word>, IWordRepo
    {
        public WordRepository(Context context) : base(context){}

        public override void Create(Word entity)
        {
            base.Create(entity);
        }

        public override void Update(Word entity)
        {
            base.Update(entity);
        }

        public override void Delete(int id)
        {
            base.Delete(id);
        }

        public override Task<Word> GetById(int id)
        {
            return base.GetById(id);
        }
        
        public override Task<IEnumerable<Word>> GetAll()
        {
            return base.GetAll();
        }
        public async Task<Word> GetWord(int wordID)
        {
            var word = await dbSet.FindAsync(wordID);
            
            return word!;
        }
    }
}