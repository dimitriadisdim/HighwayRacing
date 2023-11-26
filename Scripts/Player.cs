using Godot;


public class Player : Car
{
	[Signal] public delegate void EndGame();
	[Export] private float _spdIncrement; 
	private int _currentTurbo;
	private int _currentLane;
	private int _maxTurbo;
	private bool _right;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		//Initialize variables
		spdX = 600;
		_currentLane = 0;
		_spdIncrement = .1f;
		spd = 700;
		maxSpeed = 2000;
		_right = true;
		_maxTurbo = 3;
		_currentTurbo = 0;
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
		var pos = new Vector2(lanes[_currentLane], Position.y);
		var angle = GetAngleTo(pos);
		var motion = Vector2.Zero; 
		//Calculate angle
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			motion.x = Mathf.Cos(angle) * spdX;
		//Change Speed
		if(spd < maxSpeed)
			spd += _spdIncrement;
		motion.y = -spd;
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

	public void AddTurbo() => _currentTurbo = (_currentTurbo < _maxTurbo) ? _currentTurbo + 1 : _currentTurbo;
	
	public int GetTurbo() => _currentTurbo;
}