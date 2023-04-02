using Godot;
using System;

public class ReloadGame : CanvasLayer
{
	public override void _Ready()
	{
		
	}

	private void OnRestartPressed()
	{
		GetTree().ReloadCurrentScene();
	}
}


