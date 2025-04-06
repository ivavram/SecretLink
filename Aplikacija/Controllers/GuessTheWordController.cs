namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuessTheWordController : ControllerBase
    {
        private readonly GuessTheWordService guessTheWordService; 

        public GuessTheWordController(GuessTheWordService guessTheWordService)
        {
            this.guessTheWordService = guessTheWordService; 
        }

        /*[HttpPost("CreateGuessTheWord")]
        public async Task<ActionResult> CreateGuessTheWord([FromBody] GuessTheWordGame guessTheW)
        {
            var p = await guessTheWordService.CreateGuessTheWord(guessTheW); 
            if(p == null)
                return BadRequest("Nemoguce je kreirati guess the word igru!"); 
            return Ok("Guess the word igra je uspesno kreirana!"); 
        }*/

        [HttpGet("GetWordInGuessTheWordGame/{guessGID}")]
        public async Task<ActionResult> GetWordInGuessTheWordGame(int guessGID)
        {
            var guessW = await guessTheWordService.GetWordInGuessTheWordGame(guessGID); 
            if(guessW == null)
                return  BadRequest("Neuspesno!"); 

            Random rand = new Random();
            int length = guessW.Lenght;
            int index1 = rand.Next(0, length); // Od 0 do length-1
            int index2;

            // Osigurajte da su indeksi različiti
            do
            {
            index2 = rand.Next(0, length); // Od 0 do length-1
            } while (index2 == index1);
            return Ok(new
            {
                wordID = guessW.ID,
                wordToGuess = guessW.WordToGuess,
                wordLength = guessW.Lenght,
                RevealedIndexes = new[] { index1, index2 }
            });
        }

        [HttpGet("GetGuessTheWordByID/{guessWordID}")]
        public async Task<ActionResult> GetWordByID(int guessWordID)
        {
            var word = await guessTheWordService.GetGuessTheWordById(guessWordID); 
            if(word == null)
                return BadRequest("Ne postoji."); 
            return Ok(word); 
        }
    }
}