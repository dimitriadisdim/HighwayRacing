using System;
using Godot;


public partial class Player : Car
{
	[Signal] public delegate void EndGameEventHandler();
	[Export] private float _boostDuration;
	[Export] private float _spdIncrement; 
	[Export]private float _boostMultiplier;
	private const float TapTimeout = 0.2f;
	private Vector2 _swipeStart;
	private float _minimumDrag;
	private double _lastKeyDelta;
	private float _boostSpd;
	private int _currentBoost;
	private int _currentLane;
	private int _maxBoost;
	private bool _boosting;
	private bool _right;
	private Timer _boostTimer;
	

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
		_maxBoost = 3;
		_boostDuration = 3;
		_currentBoost = 0;
		_lastKeyDelta = 0;
		_boostMultiplier = 200;
		_minimumDrag = 100;
		//Timer
		_boostTimer = GetNode<Timer>("BoostTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		Move();
		//Collision detection
		OnCollisionEnter2D();
		_lastKeyDelta += delta;
	}

	private void Move()
	{
		//Variables
		var pos = new Vector2(lanes[_currentLane], Position.Y);
		var angle = GetAngleTo(pos);
		var motion = Vector2.Zero; 
		//Calculate angle
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			motion.X = Mathf.Cos(angle) * spdX;
		//Change Speed
		if(spd < maxSpeed)
			spd += _spdIncrement;	
		//Apply boost
		motion.Y = _boosting ? -_boostSpd : -spd;
		//Change velocity to apply movement
		Velocity = motion;
		//Apply motion
		MoveAndSlide();
	}

	public void OnCollisionEnter2D(){
		for(int i =0; i<GetSlideCollisionCount(); i++){
			EmitSignal("EndGame"); //Emit to GameManager UiManager
			QueueFree();
		}
	} 
	
	private void CalculateSwipe(Vector2 swipeEnd)
	{	
		var swipe = swipeEnd - _swipeStart;
		GD.Print(swipeEnd.X + "-" + _swipeStart.X);
		//Add a drag so we dont consider the minimal movement
		if(Mathf.Abs(swipe.X) > _minimumDrag)
			// //Change lane
			if (swipe.X > 0 && _currentLane != 3){
				_right = true;
				_currentLane++;
			}
			if (swipe.X < 0 && _currentLane != 0){
				_right = false;
				_currentLane--;
			}
	}

	public override void _Input(InputEvent @event)
	{
		if (!(@event is InputEventScreenTouch touch)) 
			return;
		
		//Check for player swipe
		if (@event.IsPressed()){
			_swipeStart = touch.Position;
			//Check for double tap
			if(_lastKeyDelta <= TapTimeout)
				Boost();
			_lastKeyDelta = 0;
		}
		else 
			CalculateSwipe(touch.Position);
	}

	private async void ResetBoost()
	{
		await ToSignal(_boostTimer, "timeout");
		GD.Print("Timeout");
		_boosting = false;
	}

	private void Boost() 
	{
		if(_currentBoost <= 0 || _boosting)
			return;
		_currentBoost--;
		_boosting = true;
		//Add boost speed to player
		_boostSpd = spd + _boostMultiplier;
		//Start boost timer
		_boostTimer.WaitTime = _boostDuration;
		_boostTimer.Start();
		ResetBoost();
	} 

	public void AddTurbo() => _currentBoost = (_currentBoost < _maxBoost) ? _currentBoost + 1 : _currentBoost;
	
	public int GetTurbo() => _currentBoost;
}