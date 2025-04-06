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

        [HttpPost("CreateGame/{playerID}/{access_flag}")]
        public async Task<ActionResult> CreateGame(int playerID, bool access_flag)
        {
            var p = await gameService.CreateGame(playerID, access_flag); 
            if(p == null)
                return BadRequest("Nemoguce je kreirati igru!"); 
            return Ok(new 
            { 
                Id = p.ID,
                gameTag = p.GameTag,
                guessTheWordID = p.GuessTheWord!.ID,
                PlayerTurn = p.PlayerTurn!.ID,
                word = p.GuessTheWord.Word!.WordToGuess
            }); 
        }

        [HttpPut("JoinGame/{playerID}/{game_tag}")]
        public async Task<ActionResult> JoinGame(int playerID, string game_tag)
        {
            var p = await gameService.JoinGame(playerID, game_tag); 
            if(p == null)
                return BadRequest("Ne mozete se prikljuciti igri!"); 
            return Ok(new{
                word = p.GuessTheWord!.Word!.WordToGuess
        }); 
        }

        [HttpPut("LeaveGame/{gameID}/{playerID}")]
        public async Task<ActionResult> LeaveGame(int gameID, int playerID)
        {
            var l = await gameService.LeaveGame(gameID, playerID);
            if(l == null)
                return BadRequest("Greska leave game!"); 
            return Ok("Uspesno ste napustili igru."); 
        }

        [HttpGet("GetPublicGame")]
        public async Task<ActionResult> GetPublicGame()
        {
            var game = await gameService.GetPublicGame();
            if(game == null)
                return BadRequest("Ne postoji aktivna javna igra!");
            return Ok(new 
            { 
                Id = game.ID,
                gameTag = game.GameTag
            }); 
        }

        [HttpPut("SetConnect4Winner/{gameID}/{username}")]
        public async Task<ActionResult> SetConnect4Winner(int gameID, string username)
        {
            Console.WriteLine($"Received GameID: {gameID}, Username: {username}");
            var game = await gameService.GetConnect4Games(gameID, username);
            if(game == null)
                return BadRequest("Ne postoji!");
            return Ok(game);
        }

        [HttpPut("AddNewConnect4Game/{gameID}")]
        public async Task<ActionResult> AddNewConnect4Game(int gameID)
        {
            var game = await gameService.AddNewConnect4Game(gameID);
            if(game == null)
                return BadRequest("Neuspesno!");
            return Ok("Uspesno!");
        }

        [HttpGet("CheckConnect4Game/{gameID}")]
        public async Task<ActionResult> CheckConnect4Game(int gameID)
        {
            // vraca igre gde je winner == null
            var game = await gameService.CheckConnect4Game(gameID);
            if(game == null)
                return Ok(new {
                    flag = false 
                });
            return Ok(new {
                flag = true
            });
        }

        [HttpPut("SetGameWinner/{gameID}/{playerUsername}")]
        public async Task<ActionResult> SetGameWinner(int gameID, string playerUsername)
        {
            var game = await gameService.SetGameWinner(gameID, playerUsername);
            if(game == null)
                return BadRequest("Ne postoji!");
            return Ok("Uspesno postavljen pobednik igre!");
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