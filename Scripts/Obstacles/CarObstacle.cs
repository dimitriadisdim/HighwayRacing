using System;
using Godot;


public partial class CarObstacle : Car, ISpanable, IDestructible
{
	[Export]public bool goingUp;
	private GpuParticles2D _explosion;
	private GpuParticles2D _fire;
	private DataManager _data;
	private Vector2 _motion;
	private RayCast2D _ray;
	private bool _changingLane;
	private int _laneToMove;
	private bool _right;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		//Randomize values
		GD.Randomize();
		goingUp = true;
		minSpeed = 200;
		maxSpeed = 800;

		//Particles
		_fire = GetNode<GpuParticles2D>("Fire");
		_explosion = GetNode<GpuParticles2D>("Explosion");

		//Raycast
		_changingLane = false;
		_ray = GetNode<RayCast2D>("RayCast2D");

		//Change lane
		_laneToMove = FindlaneAt();
		spdX = 100;

		//We do this becase we are using object pooling
		_fire.Emitting = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Move(delta);
	}

    public override void _PhysicsProcess(double delta)
    {
		if(!_changingLane)
    		RayCasting();
    }

	private void Move(double delta)
	{
		_motion = goingUp ? Vector2.Up : Vector2.Down;
		_motion *= (float)(delta * spd);
		//Add x speed if need it
		MoveToLane();
		_motion.X *= (float)delta;
		//Apply movement
		var collision = MoveAndCollide(_motion);
		//Collision detection
		if(collision != null)
			if(collision.GetCollider() is not CarObstacle) //That means object is not a car 
				Destroy();
	}

#region ChangeLane
	private void RayCasting()
	{
		if(_ray == null) 
			return;

		//Set ray destination
		var viewportRect = GetViewport().GetVisibleRect();
		_ray.TargetPosition = new Vector2(0, -viewportRect.Size.Y/3);

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
		//Disable raycast
		_changingLane = true;
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

	private int FindlaneAt() => Array.IndexOf(lanes, GlobalPosition.X);
	
	private void MoveToLane()
	{
		//In case we have not found the _lane from FinLaneAt
		if(_laneToMove == -1) return;
		
		//Calculate Movement
		var pos = new Vector2(lanes[_laneToMove], GlobalPosition.Y);
		var angle = GetAngleTo(pos);
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			_motion.X = Mathf.Cos(angle) * spdX;
		else{
			GlobalPosition = new Vector2(lanes[_laneToMove], GlobalPosition.Y);
			_changingLane = false;
		}
	}
#endregion

	private void RandomizeSpeed() => spd =(int)GD.RandRange(minSpeed, maxSpeed);   

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








