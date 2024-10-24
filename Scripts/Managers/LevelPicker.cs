using Godot;


public partial class LevelPicker : Node
{
	public static int CurrentLevel;
	[Export]private NodePath[] _levels;
	private int _index = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(ConfigManager.GetLevelState(0));
	}

	private void OnNextPressed()
	{
		if((_index+1) >= _levels.Length)
			return;

		GetNode<Node2D>(_levels[_index]).Visible = false;
		GetNode<Node2D>(_levels[++_index]).Visible = true;

		CurrentLevel = _index;
	}
	
	private void OnPreviousPressed()
	{
		if((_index-1) < 0)
			return;

		GetNode<Node2D>(_levels[_index]).Visible = false;
		GetNode<Node2D>(_levels[--_index]).Visible = true;
	}
}




