using Godot;
using System;

public partial class Scroll : ParallaxBackground
{
    [Export]private int _scrollSpd; 

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _scrollSpd = 100;
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var direction = new Vector2(0, 1);
        ScrollOffset += direction * (float)(_scrollSpd * delta);
    }
}
