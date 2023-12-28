using Common; 
using Models; 
using Interface; 

namespace Services
{
    public class WordService
    {
        private readonly IUnitOfWOrk unitOfWork; 

        public WordService(IUnitOfWOrk unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        public async Task<Word> CreateWord(Word word)
        {
            using (unitOfWork)
            {
                unitOfWork.WordRepository.Create(word);
                await unitOfWork.CompleteAsync();

                return word;  
            }
        }

        public async Task<Word> GetWordByID(int wordID)
        {
            using (unitOfWork)
            {
                Word word = await unitOfWork.WordRepository.GetById(wordID); 
                if(word == null)
                    return null!;
                
                return word; 
            }
        }
        public async Task<string> DeleteWord(int wordID)
        {
            using (unitOfWork)
            {
                Word w = await unitOfWork.WordRepository.GetById(wordID);
                if(w == null)
                    return "Ne postoji rec sa zadatim ID-jem!";
                unitOfWork.WordRepository.Delete(wordID);
                return "Rec uspesno obrisana.";
            }
        }

        
    }
}