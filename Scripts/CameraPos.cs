using Godot;
using System;

public class CameraPos : Camera2D
{
    private float _posX;
    private KinematicBody2D _player;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _player = GetNode<KinematicBody2D>("/root/Node/Player");
        _posX = _player.Position.x + 155;   
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var pos = new Vector2();
        pos.x = _posX;
        pos.y = _player.Position.y + Position.y;
        GlobalPosition = pos;
    }
}
