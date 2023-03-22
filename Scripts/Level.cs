using Godot;
using System;

public class Level : Node
{
    private Camera2D _camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _camera = GetNode<Camera2D>("Player/Camera2D");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }
}
