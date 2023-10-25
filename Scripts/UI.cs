using Godot;
using System;

public class UI : CanvasLayer
{
	[Signal]
	public delegate void Gas();
	private Button _gasButton;
	private bool _isPressed;

	public override void _Ready()
	{
		_isPressed = false;
		_gasButton = GetNode<Button>("Control/Gas");
	}

	public override void _Process(float delta)
	{
		if(_isPressed)
			OnGasPressed();
	}

	private void OnRestartPressed()
	{
		GetTree().ReloadCurrentScene();
	}
	
#region Gas
	private void OnGasPressed() => EmitSignal("Gas");
	
	private void OnGasButtonUp() => _isPressed = false;

	private void OnGasButtonDown() => _isPressed = true;
#endregion
}










