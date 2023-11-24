using Godot;
using System;

public class Turbo : Node
{
    [Export]private Sprite[] _sprites;
    [Export]private int _currentSprite;
    private int _currentTurbo;
    private int _maxTurbo;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
