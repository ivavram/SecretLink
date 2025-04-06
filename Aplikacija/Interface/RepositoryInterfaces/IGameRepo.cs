using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
    public interface IGameRepo : IBaseRepo<Game>
    {
        Task<string> GetGameTag(int gameID);
        Task<Game> GetGamePlayers(int gameID);
        Task<Game> GetGameByTag(string game_tag); 
        Task<Player> GetGameWinner(int gameID);
        Task<Game> GetPublicGame();
        Task<Game> GetConnect4Games(int gameID);
    }
}