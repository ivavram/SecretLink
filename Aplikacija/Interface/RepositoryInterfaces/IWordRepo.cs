using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interface.RepositoryInterfaces
{
    public interface IWordRepo : IBaseRepo<Word>
    {
        Task<Word> GetWord(int wordID);
    }
}