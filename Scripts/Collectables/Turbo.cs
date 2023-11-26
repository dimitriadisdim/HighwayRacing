using Godot;
using System;

public class Turbo : Area2D
{	
	private void OnTurboBodyEntered(object body)
	{
		if (body is Player p)
			p.AddTurbo();
			
		//Dont destroy object but disable it because we use object pool
		SetProcess(false);
		SetPhysicsProcess(false);
		Visible = false;
	}
	
}


