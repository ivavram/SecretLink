using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
    public interface IConnect4Repo : IBaseRepo<Connect4Game>
    {
        Task<int> GetRow(int c4ID);
        Task<int> GetColumn(int c4ID);
        Task<Player> GetWinner(int c4ID);
    }
}