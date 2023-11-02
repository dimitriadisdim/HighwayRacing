using System;
using System.Linq;
using Godot;


public class Car : KinematicBody2D, ISpanable, IDestructible
{
	[Export]private int _maxSpeed;
	[Export]private int _minSpeed;
	[Export]public bool goingUp;
	[Export]private int _xSpd;
	[Export]private int _spd;
	private Particles2D _explosion;
	private Particles2D _fire;
	private DataManager _data;
	private Vector2 _motion;
	private RayCast2D _ray;
	private int _laneToMove;
	private float[] _lanes;
	private bool _right;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_data = GetNode<DataManager>("/root/Node2D/DataManager");
		if(_data == null)
			GD.Print("DataManager is null");
		else
			_lanes = _data.GetLanes();
		//Randomize values
		GD.Randomize();
		goingUp = true;
		_minSpeed = 200;
		_maxSpeed = 800;

		//Particles
		_fire = GetNode<Particles2D>("Fire");
		_explosion = GetNode<Particles2D>("Explosion");

		//Raycast
		_ray = GetNode<RayCast2D>("RayCast2D");

		//Change lane
		_laneToMove = FindlaneAt();
		_xSpd = 50;

		//We do this becase we are using object pooling
		_fire.Emitting = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Move(delta);
	}

    public override void _PhysicsProcess(float delta)
    {
    	RayCasting();
    }

	private void Move(float delta)
	{
		_motion = goingUp ? Vector2.Up : Vector2.Down;
		_motion *= delta * _spd;
		//Add x speed if need it
		MoveToLane();
		_motion.x *= delta;
		//Apply movement
		var collision = MoveAndCollide(_motion);
		//Collision detection
		if(collision != null)
			if(collision.Collider as Car == null)//That means object is not a car 
				Destroy();
	}

#region ChangeLane
	private void RayCasting()
	{
		if(_ray == null) 
			return;

		//Set ray destination
		var viewportRect = GetViewport().GetVisibleRect();
		_ray.CastTo = new Vector2(0, -viewportRect.Size.y/3);

		//Exclude self 
		_ray.AddExceptionRid(GetRid()); 
		
		//Find collisions
		if(_ray.IsColliding())
			ChangeLane();
	}

	private void ChangeLane()
	{
		//Find current lane
		var _index = FindlaneAt();
		//We haven't found the lane
		if(_index == -1)
			return;
		//We are in the first lane
		if(_index == 0){
			_laneToMove++;
			_right = true;
		}
		//We are in the last lane
		else if(_index == 3){
			_laneToMove--;
			_right = false;
		}
		//We are in one of the middle
		else if(_index > 0 && _index<3){
			var right = GD.RandRange(0,1);
			if(right == 0){
				_laneToMove++;
				_right = true;
			}
			else{
				_laneToMove--;
				_right = false;
			}
		}
		GD.Print("Changing lane to lane:" + _laneToMove);
	}

	private int FindlaneAt() => Array.IndexOf(_lanes, GlobalPosition.x);
	
	private void MoveToLane()
	{
		//In case we have not found the _lane from FinLaneAt
		if(_laneToMove == -1) return;
		
		//Calculate Movement
		var pos = new Vector2(_lanes[_laneToMove], GlobalPosition.y);
		var angle = GetAngleTo(pos);
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			_motion.x = Mathf.Cos(angle) * _xSpd;
	}
#endregion

	private void RandomizeSpeed() => _spd =(int)GD.RandRange(_minSpeed, _maxSpeed);   

#region ObjectPool
	public void Destroy()
	{
		_explosion.Emitting = true;
		_fire.Emitting = true;
		_ray.Enabled = false;
	}
    public void OnSpawn() 
	{
		_laneToMove = FindlaneAt();
		//Because we want diffent speed every time the car respawns
		RandomizeSpeed(); 	
		//Because the car maybe had collide and will play the effect on spawn
		_fire.Emitting = false;
		_explosion.Emitting = false;
		//Enable raycast
		if(_ray != null)
			_ray.Enabled = true;
		else
			GD.Print("Raycast2D is null (" + Name + ")");
	}
#endregion
}








