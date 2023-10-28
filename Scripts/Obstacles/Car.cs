using Godot;


public class Car : KinematicBody2D, ISpanable
{
	[Export]private int _maxSpeed;
	[Export]private int _minSpeed;
	[Export]public bool goingUp;
	[Export]private int _speed;
	private Particles2D _fire;
	private Particles2D _explosion;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Randomize values
		GD.Randomize();
		goingUp = true;
		_minSpeed = 200;
		_maxSpeed = 700;
		RandomizeSpeed(); 
  
		_fire = GetNode<Particles2D>("Fire");
		_explosion = GetNode<Particles2D>("Explosion");

		//We do this becase we are using object pooling
		_fire.Emitting = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Move(delta);
	}

	private void Move(float delta)
	{
		Vector2 motion = goingUp ? Vector2.Up : Vector2.Down;
		motion *= delta * _speed;
		//Apply movement
		var collision = MoveAndCollide(motion);
		//Collision detection
		if(collision != null)
			Destroy();
	}

	private void Destroy()
	{
		_explosion.Emitting = true;
		_fire.Emitting = true;
	}

	private void RandomizeSpeed() => _speed =(int)GD.RandRange(_minSpeed, _maxSpeed);   
	
	public void OnSpawn() 
	{
		//Because we want diffent speed every time the car respawns
		RandomizeSpeed(); 	
		//Because the car maybe had collide and will play the effect on spawn
		_fire.Emitting = false;
		_explosion.Emitting = false;
	}
}








