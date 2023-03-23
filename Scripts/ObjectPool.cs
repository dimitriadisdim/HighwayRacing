using Godot;
using System;

public class ObjectPool : Node
{
    [Export] private PackedScene _rockScene;
    [Export] private int _rockCount;
    private Node2D[] _rocks;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Instantiate(); //Create objects
    }

    private void Instantiate(){
        _rocks = new Node2D[_rockCount];
        for(int i=0; i<_rockCount; i++){
            //Instanciate object
            Node2D node =(Node2D)_rockScene.Instance();
            //Disable it
            node.SetProcess(false);
            //Add to scene
            AddChild(node);
            //Add to object list
            _rocks[i] = node;
        }
    }

    public Node2D GetObject(){
        //Return a diactivated object
        foreach (var obj in _rocks){
            if(!obj.IsProcessing())
                return obj;
        }
        return null;
    }

}
