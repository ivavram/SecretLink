namespace Models;


public class GuessTheWordGame
{
    public int ID { get; set; }
    public Word? Word { get; set; }
    public Connect4Game? Connect4Winner { get; set; }
}