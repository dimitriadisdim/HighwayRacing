using Godot;


public class GameManager : Node
{
	[Signal]public delegate void BossFight();
	[Export]private NodePath[] _objects;
	private bool _bossFightTriggered;
	private Score _scoreNode;
	private Level _level;


    public override void _Ready()
    {
		_scoreNode = GetNode<Score>("/root/Node/Score");
		_level = GetNode<Level>(GetParent().GetPath());
		_bossFightTriggered = false;
    }

    public override void _Process(float delta)
    {
        if(_scoreNode.GetScore() >= _level.GetBossTriggerScore() && !_bossFightTriggered){
			GD.Print("Boss triggered");
			EmitSignal("BossFight");
			_bossFightTriggered = true;
		}
    }

    private void EndGame()
	{
		//Disable all objects
		foreach (var objName in _objects){
			var obj = GetNode<Node2D>(objName);
			obj.QueueFree();
		}
	}
}


