using Godot;
using System;

public class LevelPicker : Node
{
	[Export]private NodePath[] _levels;
	private int _index = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(DataManager.GetLevelState(0));
	}

	private void OnNextPressed()
	{
		if((_index+1) >= _levels.Length)
			return;

		GetNode<Node2D>(_levels[_index]).Visible = false;
		GetNode<Node2D>(_levels[++_index]).Visible = true;
	}
	
	private void OnPreviousPressed()
	{
		if((_index-1) < 0)
			return;

		GetNode<Node2D>(_levels[_index]).Visible = false;
		GetNode<Node2D>(_levels[--_index]).Visible = true;
	}
}




