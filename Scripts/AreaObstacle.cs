using Godot;
using System;

public class AreaObstacle : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	
	private void OnArea2DBodyEntered(object body)
	{
		var body2d = body as Node2D;
        GetTree().ReloadCurrentScene();
	}
}


