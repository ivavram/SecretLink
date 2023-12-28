using Models; 
using Interface; 
using Services; 

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerInGameController : ControllerBase
    {
        private readonly PlayerInGameService playerInGameService; 

        public PlayerInGameController(PlayerInGameService playerInGameService)
        {
            this.playerInGameService = playerInGameService; 
        }

        [HttpGet("GetPlayersInGame/{gameID}")]
        public async Task<ActionResult> GetPlayersInGame(int gameID)
        {
            var game = await playerInGameService.GetPlayersInGame(gameID); 
            if(game == null)
                return BadRequest("Neuspesno!"); 
            return Ok(game); 
        }

        [HttpGet("GetGamesWonByPlayer/{gameID}/{playerID}")]
        public async Task<ActionResult> GetGamesWonByPlayer(int gameID, int playerID)
        {
            var num = await playerInGameService.GetGamesWonByPlayer(gameID, playerID);
            if(num == -1)
                return BadRequest("Nije pronadjena igra/igrac sa navedenim ID-jem.");
            else
                return Ok("Broj pobeda: " + num + ".");
        }
    }
}