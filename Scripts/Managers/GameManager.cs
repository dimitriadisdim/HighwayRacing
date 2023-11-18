using Godot;


public class GameManager : Node
{
	[Export]private NodePath[] _objects;
	private Score _scoreNode;
	private Level _level;


    public override void _Ready()
    {
		_scoreNode = GetNode<Score>("/root/Node/Score");
		//_level = GetNode<Level>("root/Node/Level");
    }

    public override void _Process(float delta)
    {
        //if(_scoreNode.GetScore() >= _level.GetBossTriggerScore());
			//Trigger boss
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


