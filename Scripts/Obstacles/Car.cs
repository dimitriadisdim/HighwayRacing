using Godot;
using System.Threading.Tasks;


public class Car : KinematicBody2D, ISpanable
{
	[Export]private int _maxSpeed;
	[Export]private int _minSpeed;
	[Export]public bool goingUp;
	[Export]private int _speed;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();
		goingUp = true;
		_minSpeed = 200;
		_maxSpeed = 700;
		RandomizeSpeed();   
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var motion = Vector2.Zero;
		motion = goingUp ? Vector2.Up : Vector2.Down;
		motion *= delta * _speed;
		//Apply movement
		Position += motion;
	}

	private void RandomizeSpeed() => _speed =(int)GD.RandRange(_minSpeed, _maxSpeed);   
	
    public void OnSpawn() {
		RandomizeSpeed(); //Becouse we want diffent speed every time the car respawns
		GD.Print("Random : " + _speed);
	}
}






