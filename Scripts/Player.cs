using Godot;
using System.Diagnostics;

public class Player : KinematicBody2D
{
	[Signal]
	public delegate void EndGame();

	//Movement
	[Export] private float _spdIncrement; 
	[Export] private float _spdVertical;
	[Export] private readonly float _maxSpeed;
	private int _spd;
	private int _currentLane; // 0-4 Lanes
	private int[] _lanes;
	private bool _right;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Initialize variables
		_lanes = new int[]
		{
			180, 300, 415, 535
		};
		_spdVertical = 600;
		_currentLane = 0;
		_spdIncrement = .1f;
		_spd = 700;
		_right = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{	
		Move();
		//Collision detection
		OnCollisionEnter2D();
	}

	private void Move()
	{
		//Variables
		var pos = new Vector2(_lanes[_currentLane], Position.y);
		var angle = GetAngleTo(pos);
		var motion = Vector2.Zero; 
		//Calculate angle
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			motion.x = Mathf.Cos(angle) * _spd;
		//Change Speed
		if(_spdVertical < _maxSpeed)
			_spdVertical += _spdIncrement;
		motion.y = -_spdVertical;
		//Apply motion
		MoveAndSlide(motion);
	}

	public void OnCollisionEnter2D(){
		for(int i =0; i<GetSlideCount(); i++){
			EmitSignal("EndGame"); //Emit to GameManager UiManager
			QueueFree();
		}
	} 
	
	public override void _Input(InputEvent @event)
	{
		if (!(@event is InputEventScreenTouch touch)) 
			return;
	  
		//Check if player can swipe
		if (@event.IsPressed())
			return;
	  
		//Calculate vector
		var motion = touch.Position - Position;
		motion = motion.Normalized();
	  
		//Change lane
		if (motion.x > 0 && _currentLane != 3){
			_right = true;
			_currentLane++;
		}
		if (motion.x < 0 && _currentLane != 0){
			_right = false;
			_currentLane--;
		}
	}
}


