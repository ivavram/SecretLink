using Common;
using Models; 
using Interface; 

namespace Services
{
    public class PlayerInGameService
    {
        private readonly IUnitOfWOrk unitOfWork; 

        public PlayerInGameService(IUnitOfWOrk unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        public async Task<List<string>> GetPlayersInGame(int gameID)
        {
            var games = await unitOfWork.PlayerInGameRepository.GetPlayersInGame(gameID); 
            if(games == null)
                return null!;
            return games!;
            
        }
        public async Task<int> GetGamesWonByPlayer(int gameID, int playerID)
        {
            var games_won = await unitOfWork.PlayerInGameRepository.GetGamesWonByPlayer(gameID, playerID); 
            return games_won; 
        }
    }
}