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
        public async Task<ActionResult> GetC4Winner(int guessGID)
        {
            var guessW = await guessTheWordService.GetWordInGuessTheWordGame(guessGID); 
            if(guessW == null)
                return  BadRequest("Neuspesno!"); 
            return Ok(guessW);
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