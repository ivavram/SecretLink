using System.Transactions;
using Interface;
using Interface.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository
{
    public class UnitOfWork : IUnitOfWOrk
    {

        private readonly Context context; 
        private readonly IDbContextTransaction transaction;
        public IConnect4Repo Connect4Repository { get; private set; }

        public IGameRepo GameRepository { get; private set; }

        public IGuessTheWordRepo GuessTheWordRepository { get; private set; }

        public IPlayerInGameRepo PlayerInGameRepository { get; private set; }

        public IPlayerRepo PlayerRepository { get; private set; }

        public IWordRepo WordRepository { get; private set; }

        public UnitOfWork(Context context)
        {
            this.context = context; 
            transaction = context.Database.BeginTransaction();

            Connect4Repository = new Connect4Repository(context); 
            GameRepository = new GameRepository(context); 
            GuessTheWordRepository = new GuessTheWordRepository(context); 
            PlayerInGameRepository = new PlayerInGameRepository(context); 
            PlayerRepository = new PlayerRepository(context); 
            WordRepository = new WordRepository(context); 
        }
        public async Task CompleteAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                transaction.Commit(); 
            }
            catch
            {
                transaction.Rollback(); 
                throw; 
            }
        }

        public void Dispose()
        {
            transaction.Dispose(); 
            context.Dispose();
        }
    }
}