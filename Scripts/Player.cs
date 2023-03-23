using Godot;
using System;
using System.Diagnostics;

public class Player : Area2D
{
    //Movement
    [Export] private int _spd;
    [Export] private int _spdVertical;
    private int[] _lanes;
    private int _currentLane; // 0-4 Lanes
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Initialize variables
        _lanes = new int[]
        {
            200, 300, 415, 520
        };
        _currentLane = 0;
        _spd = 2000;
        _spdVertical = 10;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Move();
    } 
    
    private void Move()
    {
        //Variables
        var pos = new Vector2(_lanes[_currentLane], Position.x);
        var motion = pos - Position;
        //Normalize vector 
        motion = motion.Normalized();
        //Apply speed and delta
        motion *= _spd; // How fast we change lane
        //Apply movement
        if (Math.Abs(Position.x - _lanes[_currentLane]) > 0.1){
            Position += new Vector2(motion.x, -_spdVertical);
        }
        else
            Position = new Vector2(_lanes[_currentLane], Position.y - _spdVertical);
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
        if (motion.x > 0 && _currentLane != 3)
            _currentLane++;
        if (motion.x < 0 && _currentLane != 0)
            _currentLane--;
    }
}
