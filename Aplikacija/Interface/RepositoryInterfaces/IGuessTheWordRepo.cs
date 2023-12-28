using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
    public interface IGuessTheWordRepo : IBaseRepo<GuessTheWordGame>
    {
        Task<Word> GetWordInGuessTheWordGame(int guessGameID);
    }
}