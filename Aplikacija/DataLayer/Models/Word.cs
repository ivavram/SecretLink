namespace Models;

public class Word
{
    public int ID { get; set; }
    public required string WordToGuess { get; set; }
    public int Lenght { get; set; } 
}