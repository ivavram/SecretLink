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
    }
}