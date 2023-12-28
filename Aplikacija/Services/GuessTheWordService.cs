namespace Services
{
    public class GuessTheWordService
    {
        private readonly IUnitOfWOrk unitOfWork; 

        public GuessTheWordService(IUnitOfWOrk unitOfWork)
        {
            this.unitOfWork = unitOfWork; 
        }

        /*public async Task<GuessTheWordGame> CreateGuessTheWord(GuessTheWordGame guessTheWord)
        {
            using (unitOfWork)
            {
                unitOfWork.GuessTheWordRepository.Create(guessTheWord);
                await unitOfWork.CompleteAsync();

                return guessTheWord;  
            }
        }*/

        public async Task<Word> GetWordInGuessTheWordGame(int guessGameID)
        {
            using (unitOfWork)
            {   
                Word w = await unitOfWork.GuessTheWordRepository.GetWordInGuessTheWordGame(guessGameID); 
                if(w == null)
                    return null!; 
                else 
                    return w;
            }
        }

        public async Task<GuessTheWordGame> GetGuessTheWordById(int id)
        {
            using (unitOfWork)
            {
                GuessTheWordGame word = await unitOfWork.GuessTheWordRepository.GetById(id); 
                if(word == null)
                    return null!;
                
                return word; 
            }
        }
    }
}