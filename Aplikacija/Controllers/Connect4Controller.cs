namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Connect4Controller : ControllerBase
    {
        private readonly Connect4Service connect4Service; 

        public Connect4Controller(Connect4Service connect4Service)
        {
            this.connect4Service = connect4Service; 
        }

        [HttpPost("CreateConnect4")]
        public async Task<ActionResult> CreateConnect4([FromBody] Connect4Game connect4)
        {
            var p = await connect4Service.CreateConnect4(connect4); 
            if(p == null)
                return BadRequest("Nemoguce je kreirati connect4 igru!"); 
            return Ok("Connect4 igra je uspesno kreirana!"); 
        }

        [HttpGet("GetC4ByID/{c4ID}")]
        public async Task<ActionResult> GetC4ByID(int c4ID)
        {
            var c4 = await connect4Service.GetC4ByID(c4ID); 
            if(c4 == null)
                return BadRequest("Ne postoji c4 sa zadatim IDjem."); 
            return Ok(c4); 
        }

        [HttpGet("GetC4Winner/{c4ID}")]
        public async Task<ActionResult> GetC4Winner(int c4ID)
        {
            var player = await connect4Service.GetWinner(c4ID); 
            if(player == null)
                return  BadRequest("Ne postoji pobednik ove C4 igre!"); 
            return Ok(player);
        }
    }
}