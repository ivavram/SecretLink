using Interface.RepositoryInterfaces; 

namespace Interface
{
    public interface IUnitOfWOrk : IDisposable
    {
        IConnect4Repo Connect4Repository {get;}
        IGameRepo GameRepository{get; }
        IGuessTheWordRepo GuessTheWordRepository {get; }
        IPlayerInGameRepo PlayerInGameRepository {get;}
        IPlayerRepo PlayerRepository {get; }
        IWordRepo WordRepository {get;} 

        Task CompleteAsync();
    }
}