using Godot;
using System;

public class Spawner : Node2D
{
	private float _couldown;
	private ObjectPool _objectPool;
    private Area2D _player;
	private Timer _timer;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_objectPool = GetNode<ObjectPool>("/root/Node/ObjectPool");
		_timer = GetNode<Timer>("Timer");
        _player = GetNode<Area2D>("/root/Node/Player");
		_couldown = 2;
		//Start timer
		_timer.Start(_couldown);
	}
    
    private void Spawn()
    {
        //Get object and check for errors
        var obj = _objectPool.GetObject();
        if(obj == null)
            return;
        //Change location and enable object 
        GD.Randomize(); //Randomize seed
        var posX = (float)GD.RandRange(180, 545);
        var posY = _player.Position.y - OS.WindowSize.y;
        obj.GlobalPosition = new Vector2(posX, posY);
        obj.SetProcess(true);
    }
	
    private void OnTimerTimeout()
	{
        Spawn();
        _timer.Start(_couldown);
	}

}


