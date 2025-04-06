using Common; 
using Models; 
using Interface; 

namespace Services
{
    public class PlayerService
    {
         private readonly IUnitOfWOrk unitOfWork; 

        public PlayerService(IUnitOfWOrk unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        public async Task<Player?> CreatePlayer(Player player)
        {
            using (unitOfWork)
            {
                
                if(player.Password.Length < 8 || player.Password.Length > 25)
                    return null; 

                Player p = await unitOfWork.PlayerRepository.GetPlyerByUsername(player.Username); 

                if(p != null)
                    return null; 
                
                player.Password = CommonMethods.EncryptPassword(player.Password, player.Username); 

                unitOfWork.PlayerRepository.Create(player); 
                await unitOfWork.CompleteAsync(); 

                return player; 
            }
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            using(unitOfWork)
            {
                IEnumerable<Player> players = await unitOfWork.PlayerRepository.GetAll(); 
                foreach(var p in players)
                {
                    p.Password = null!; 
                }
                return players; 
            }
        }

        public async Task<Player> GetPlayerByID(int userID)
        {
            using (unitOfWork)
            {
                Player player = await unitOfWork.PlayerRepository.GetById(userID); 
                if(player != null)
                    player.Password = null!;
                
                return player!; 
            }
        }

        public async Task<Player> GetPlayerByUsername(string username)
        {
            Player player = await unitOfWork.PlayerRepository.GetPlyerByUsername(username); 
            if (player != null)
                player.Password = null!;
        
            return player!; 
            /*using (unitOfWork)
            {
                Player player = await unitOfWork.PlayerRepository.GetPlyerByUsername(username); 
                if(player != null)
                    player.Password = null!;
                
                return player!; 
            }*/
        }

        public async Task<string> GetPasswordByPlayerUsername(string username)
        {
           return await unitOfWork.PlayerRepository.GetPasswordByPlayerUsername(username);
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            Player p = await unitOfWork.PlayerRepository.GetById(player.ID);
            if (p == null)
                return null!;

            p.Username = player.Username;
            if(player.Avatar != null)
                p.Avatar = player.Avatar;
            p.Password = CommonMethods.EncryptPassword(player.Password, player.Username);
    
            unitOfWork.PlayerRepository.Update(p); 
    
            await unitOfWork.CompleteAsync();

            return p;
        }

    }
}