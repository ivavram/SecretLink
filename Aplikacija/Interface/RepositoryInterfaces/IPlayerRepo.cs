using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
   public interface IPlayerRepo :IBaseRepo<Player>
   {
        Task<Player> GetPlyerByUsername(string username);
   }
}