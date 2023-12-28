using Models; 
using Interface; 
using Services; 

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly WordService wordService; 

        public WordController(WordService wordService)
        {
            this.wordService = wordService; 
        }

        [HttpPost("CreateWord")]
        public async Task<ActionResult> CreateWord([FromBody]Word word)
        {
            var p = await wordService.CreateWord(word); 
            return Ok("Rec je uspesno kreirana."); 
        }

        [HttpGet("GetWordByID/{wordID}")]
        public async Task<ActionResult> GetWordByID(int wordID)
        {
            var word = await wordService.GetWordByID(wordID); 
            if(word == null)
                return BadRequest("Ne postoji rec sa zadatim IDjem."); 
            return Ok(word); 
        }

        /*
        [HttpDelete("DeleteWord/{wordID}")]
        public async Task<ActionResult> DeleteWord(int wordID)
        {
            var w = await wordService.DeleteWord(wordID);
            return Ok(w);
        }*/

    }
}