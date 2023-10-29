using Godot;
using System;

public class Destroy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void OnCollisionEntered(object body)
	{
		if(body is IDestructible script)
			script.Destroy();
	}
}


