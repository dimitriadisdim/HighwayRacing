using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export]private NodePath _objectPoolPath;
	[Export]private float min;
	[Export]private float max;
	private ObjectPool _objectPool;
	private CharacterBody2D _player;
	private Vector2 _screenSize;
	private bool _shouldSpawn;
	private Marker2D _posY;
	private float _couldown;
	private Timer _timer;
	private int[] _pos;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_objectPool = GetNode<ObjectPool>(_objectPoolPath);
		_posY = GetNode<Marker2D>("/root/Node/Player/Marker2D");
		_timer = GetNode<Timer>("Timer");
		_player = GetNode<CharacterBody2D>("/root/Node/Player");
		_screenSize = GetViewportRect().Size;
		_pos = new int[] {
			180, 300, 415, 535
		};
		_shouldSpawn = true;
		//Randomize values
		_couldown = (float)GD.RandRange(min, max);
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
		var index = (int)GD.RandRange(0, 4);
		obj.GlobalPosition = new Vector2(_pos[index], _posY.GlobalPosition.Y);
		//Run object script
		if(obj is ISpanable script)
			script.OnSpawn();
		//Enable object
		obj.SetProcess(true);
		obj.SetPhysicsProcess(true);
		obj.Visible = true;
	}
	
	private void OnTimerTimeout()
	{
		if(_shouldSpawn)
			Spawn();
		_timer.Start(_couldown);
	}

	private void OnGameManagerBossFight() => _shouldSpawn = false;

	private void OnEndGame() => _shouldSpawn = false;
}








