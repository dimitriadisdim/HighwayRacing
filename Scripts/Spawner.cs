using Godot;
using System;

public class Spawner : Node2D
{
	[Export]private NodePath _objectPoolPath;
    private ObjectPool _objectPool;
    private KinematicBody2D _player;
    private Vector2 _screenSize;
    private Position2D _posY;
	private float _couldown;
	private Timer _timer;
    private int[] _pos;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _objectPool = GetNode<ObjectPool>(_objectPoolPath);
        _posY = GetNode<Position2D>("/root/Node/Player/Position2D");
		_timer = GetNode<Timer>("Timer");
        _player = GetNode<KinematicBody2D>("/root/Node/Player");
        _screenSize = GetViewportRect().Size;
        _pos = new int[] {
            200, 300, 415, 520
        };
        //Randomize values
        _couldown = (float)GD.RandRange(1,5);
		//Start timer
		_timer.Start(_couldown);
	}
    
    private void Spawn()
    {
        //Get object and check for errors
        var obj = _objectPool.GetObject();
        if(obj == null)
            return;
        //Change location
        GD.Randomize(); //Randomize seed
        var index = (int)GD.RandRange(0, 3);
        obj.GlobalPosition = new Vector2(_pos[index], _posY.GlobalPosition.y);
        //Run object script
        var script = obj as ISpanable;
        if(script != null)
            script.OnSpawn();
        //Enable object
        obj.SetProcess(true);
    }
	
    private void OnTimerTimeout()
	{
        Spawn();
        _timer.Start(_couldown);
	}

}


