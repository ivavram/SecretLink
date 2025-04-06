using Models; 
using Interface; 
using Services; 
using Common;

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
            if (player.Avatar == null || player.Avatar.Length == 0)
                return Ok(new
                    {
                        player.Username // Vraćanje smanjene slike kao byte[]
                    }); 

            byte[] avatarBytes = player.Avatar!;
            using (var image = Image.Load(avatarBytes))
            {
                // Smanjite sliku
                image.Mutate(x => x.Resize(150, 150)); // Promena dimenzija, prilagodite prema potrebi
                
                using (var memoryStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(memoryStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 75 }); // Smanjite kvalitet
                    byte[] reducedImageBytes = memoryStream.ToArray();
                    
                    return Ok(new
                    {
                        player.Username,
                        Avatar = reducedImageBytes // Vraćanje smanjene slike kao byte[]
                    });
                }
            }
        }

        
        [HttpGet("GetPlayerByUsername/{username}")]
        public async Task<ActionResult> GetPlayerByUsername(string username)
        {
            var player = await playerService.GetPlayerByUsername(username); 
            if(player == null)
                return BadRequest("Ne postoji player sa zadatim username-om."); 
            return Ok(player); 
        }

        [HttpGet("GetPasswordByPlayerUsername/{username}")]
        public async Task<ActionResult> GetPasswordByPlayerUsername(string username)
        {
            var pass = await playerService.GetPasswordByPlayerUsername(username);
            if(pass == null)
                return BadRequest("Error!"); 
            return Ok(pass); 
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Player loginRequest)
        {
            var player = await playerService.GetPlayerByUsername(loginRequest.Username);
            Console.WriteLine(player.Username);
            if (player != null )
            {

                var player_pass = await playerService.GetPasswordByPlayerUsername(player.Username);

                Console.WriteLine("Player iz baze: " + player_pass);
                Console.WriteLine("Unesena lozinka: " + CommonMethods.EncryptPassword(loginRequest.Password, loginRequest.Username));
                if(player_pass == CommonMethods.EncryptPassword(loginRequest.Password, loginRequest.Username))
                {
                    return Ok(new 
                    { 
                        //message = "Login successful!",
                        Id = player.ID, 
                        Username = player.Username,
                        Avatar = player.Avatar,
                    });
                }
                return Unauthorized("Invalid password!");
            }
            return Unauthorized("Invalid username!");
        }

        
        [HttpPut("UpdatePlayer")]
        public async Task<ActionResult> UpdatePlayer([FromBody] Player updated_player)
        {
            //var player = await playerService.GetPlayerByID(updated_player.ID);
            //Console.WriteLine(player.Username);
            //if (player != null )
            //{
            var response = await playerService.UpdatePlayer(updated_player);
            if(response != null)
                return Ok(response);
            return BadRequest("Neuspesno azuriranje!");
            //}
            //return BadRequest("Ne postoji player sa prosledjenim ID-jem!");
        }

        [HttpPost("CheckPassword/{username}/{password}")]
        public async Task<ActionResult> CheckPassword(string username, string password)
        {
            var player = await playerService.GetPlayerByUsername(username);

            if(player != null)
            {
                var player_password = await playerService.GetPasswordByPlayerUsername(username);

                if(player_password == CommonMethods.EncryptPassword(password, username))
                {
                    return Ok("Lozinka ispravna!");
                }
            }
            return BadRequest("Lozinka neispravna!");
        }
    
    
    }
    // Klasa za zahtev za login
     /*public class PlayerLoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

       [HttpPost("Login/{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            // Provera da li postoji korisnik sa zadatim korisničkim imenom i lozinkom
            var player = await playerService.GetPlayerByUsername(username);
            
            if (player == null)
            {
                return NotFound();
            }

            return Ok(new 
            { 
                message = "Login successful", 
                PlayerId = player.Id, 
                Username = player.Username,
            });
        }*/

}