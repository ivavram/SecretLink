using Models; 
using Interface; 
using Services; 

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService playerService; 

        public PlayerController(PlayerService playerService)
        {
            this.playerService = playerService; 
        }

        [HttpPost("CreatePlayer")]
        public async Task<ActionResult> CreatePlayer([FromBody] Player player)
        {
            var p = await playerService.CreatePlayer(player); 
            if(p == null)
                return BadRequest("Nemoguce je kreirati playera"); 
            return Ok("Player je uspesno kreiran " + p); 
        }

        [HttpGet("GetAllPlayers")]
        public async Task<ActionResult> GetAllPLayers()
        {
            var players = await playerService.GetAllPlayers(); 
            return Ok(players); 
        }

        [HttpGet("GetPlayerByID/{playerID}")]
        public async Task<ActionResult> GetPlayerByID(int playerID)
        {
            var player = await playerService.GetPlayerByID(playerID); 
            if(player == null)
                return BadRequest("Ne postoji player sa zadatim IDjem."); 
            return Ok(player); 
        }

        
        [HttpGet("GetPlayerByUsername/{username}")]
        public async Task<ActionResult> GetPlayerByUsername(string username)
        {
            var player = await playerService.GetPlayerByUsername(username); 
            if(player == null)
                return BadRequest("Ne postoji player sa zadatim username-om."); 
            return Ok(player); 
        }

    }
}