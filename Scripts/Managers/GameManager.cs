using Godot;


public class GameManager : Node
{
	[Export]private NodePath[] _objects;

	private void EndGame()
	{
		//Disable all objects
		foreach (var objName in _objects){
			var obj = GetNode<Node2D>(objName);
			obj.QueueFree();
		}
	}
}


