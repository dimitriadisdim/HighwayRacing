using Godot;
using System;

public class Car : KinematicBody2D
{
    [Export]private int _speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Randomize();
        _speed = (int)GD.RandRange(10, 50);   
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var motion = Vector2.Down * _speed;
        MoveAndCollide(motion * delta);
    }
}
