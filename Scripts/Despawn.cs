using Godot;
using System;

public class Despawn : Area2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	
	private void OnArea2DAreaEntered(Area2D area)
	{
		GD.Print(area.Name);
		area.SetProcess(false);
	}
}












