using Godot;
using System;

public class ObjectPool : Node
{
    [Export] private PackedScene[] _objects;
    private Vector2 _startingPos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instanciate();
    }

    private void Instanciate(){
        foreach (var obj in _objects){
            //Instanciate object
            var node = obj.Instance();
            AddChild(node);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
