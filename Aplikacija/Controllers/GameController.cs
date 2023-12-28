namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService gameService; 

        public GameController(GameService gameService)
        {
            this.gameService = gameService; 
        }

        [HttpPost("CreateGame/{playerID}")]
        public async Task<ActionResult> CreateGame(int playerID)
        {
            var p = await gameService.CreateGame(playerID); 
            if(p == null)
                return BadRequest("Nemoguce je kreirati igru!"); 
            return Ok("Igra je uspesno kreirana!"); 
        }

        [HttpPut("JoinGame/{playerID}/{game_tag}")]
        public async Task<ActionResult> JoinGame(int playerID, string game_tag)
        {
            var p = await gameService.JoinGame(playerID, game_tag); 
            if(p == null)
                return BadRequest("Ne mozete se prikljuciti igri!"); 
            return Ok("Uspesno ste se prikljucili igri."); 
        }

        /*[HttpGet("GetGameWinner/{gameID}")]
        public async Task<ActionResult> GetGameWinner(int gameID)
        {
            var player = await gameService.GetGameWinner(gameID);
            if(player == null)
                return BadRequest("Jos uvek ne postoji pobednik igre!");
            else
                return Ok(player);
        }*/
        
    }
}