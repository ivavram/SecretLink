using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
    public interface IPlayerInGameRepo : IBaseRepo<PlayerInGame>
    {
        Task<PlayerInGame> GetByGameIDAndPlayerID(int gameID, int playerID);
        Task<List<string>> GetPlayersInGame(int gameID); 
        Task DeleteComposite(int gameID, int playerID);
    }
}