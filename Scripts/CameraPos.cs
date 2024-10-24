using Godot;


public partial class CameraPos : Camera2D
{
    private float _posX;
    private CharacterBody2D _player;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _player = GetNode<CharacterBody2D>("/root/Node/Player");
        _posX = _player.Position.X + 155;   
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var pos = new Vector2
        {
            X = _posX,
            Y = _player.Position.Y + Position.Y
        };
        GlobalPosition = pos;
    }
}
