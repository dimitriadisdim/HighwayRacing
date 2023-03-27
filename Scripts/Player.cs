using Godot;
using System;
using System.Diagnostics;

public class Player : KinematicBody2D
{
	//Movement
	[Export] private int _spdVertical;
	[Export] private int _spd;
	private int _currentLane; // 0-4 Lanes
	private int[] _lanes;
	private bool _right;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Initialize variables
		_lanes = new int[]
		{
			200, 300, 415, 520
		};
		_spdVertical = 1500;
		_currentLane = 0;
		_spd = 500;
		_right = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
			
		//Variables
		var pos = new Vector2(_lanes[_currentLane], Position.y);
		var angle = GetAngleTo(pos);
		var motion = Vector2.Zero; 
		//Calculate angle
		if((_right && angle == 0) || (!_right && angle == Mathf.Pi))
			motion.x = Mathf.Cos(angle) * _spd;
		//Add y axis 
		motion.y = -_spdVertical;
		//Apply motion
		MoveAndCollide(motion * GetProcessDeltaTime());
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
