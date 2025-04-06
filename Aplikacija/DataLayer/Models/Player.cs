namespace Models;

public class Player
{
    public int ID { get; set; }
    public required string Username { get; set; } 
    public required string Password { get; set; }
    public byte[]? Avatar { get; set; }

}
