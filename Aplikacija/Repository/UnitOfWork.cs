using Interface;
using Interface.RepositoryInterfaces; 

namespace Repository
{
    public class UnitOfWork : IUnitOfWOrk
    {

        private readonly Context context; 
        public IConnect4Repo Connect4Repository { get; private set; }

        public IGameRepo GameRepository { get; private set; }

        public IGuessTheWordRepo GuessTheWordRepository { get; private set; }

        public IPlayerInGameRepo PlayerInGameRepository { get; private set; }

        public IPlayerRepo PlayerRepository { get; private set; }

        public IWordRepo WordRepository { get; private set; }

        public UnitOfWork(Context context)
        {
            this.context = context; 

            Connect4Repository = new Connect4Repository(context); 
            GameRepository = new GameRepository(context); 
            GuessTheWordRepository = new GuessTheWordRepository(context); 
            PlayerInGameRepository = new PlayerInGameRepository(context); 
            PlayerRepository = new PlayerRepository(context); 
            WordRepository = new WordRepository(context); 
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            context.Dispose(); 
        }
    }
}