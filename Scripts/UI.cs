using Godot;
using System;

public class UI : CanvasLayer
{
	private bool _isPressed;

	public override void _Ready()
	{
		_isPressed = false;
	}
	
	private void OnRestartPressed()
	{
		GetTree().ReloadCurrentScene();
	}
}










