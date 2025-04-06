using Common; 
using Models; 
using Interface; 

namespace Services
{
    public class Connect4Service
    {
         private readonly IUnitOfWOrk unitOfWork; 

        public Connect4Service(IUnitOfWOrk unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        public async Task<Connect4Game> CreateConnect4(Connect4Game connect4)
        {
            using (unitOfWork)
            {
                unitOfWork.Connect4Repository.Create(connect4);
                await unitOfWork.CompleteAsync();

                return connect4;  
            }
        }

         public async Task<Connect4Game> GetC4ByID(int c4ID)
        {
            using (unitOfWork)
            {
                Connect4Game c4 = await unitOfWork.Connect4Repository.GetById(c4ID);  
                if(c4 == null)
                    return null!;
                
                return c4; 
            }
        }

        public async Task<Player> GetWinner(int c4ID)
        {
            
            Player p = await unitOfWork.Connect4Repository.GetWinner(c4ID); 
            if(p == null)
                return null!; 
            else 
                return p;
        }

        public async Task<Connect4Game> SetWinner(int c4ID, string username)
        {
            Connect4Game game = await unitOfWork.Connect4Repository.GetById(c4ID);
            Player player = await unitOfWork.PlayerRepository.GetPlyerByUsername(username);
            if(game != null && player != null)
            {
                game.Winner = player;
                await unitOfWork.CompleteAsync();
                return game;
            }
            return null!;
        }
        
    }
}    