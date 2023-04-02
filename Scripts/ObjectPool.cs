using Godot;
using System;

public class ObjectPool : Node
{
    [Export] private PackedScene _scene;
    [Export] private int _count;
    private Node2D[] _objects;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instantiate(); //Create objects
    }

    private void Instantiate(){
        _objects = new Node2D[_count];
        for(int i=0; i<_count; i++){
            //Instanciate object
            Node2D node =(Node2D)_scene.Instance();
            //Disable it
            node.SetProcess(false);
            //Add to scene
            AddChild(node);
            //Add to object list
            _objects[i] = node;
        }
    }

    public Node2D GetObject(){
        //Return a diactivated object
        foreach (var obj in _objects){
            if(!obj.IsProcessing())
                return obj;
        }
        return null;
    }

}
