using System;
using System.Threading.Tasks;
using Godot;


public class Despawn : Area2D
{
    private Vector2 _startingPos;
    private Vector2 _vieportRect;
    private Node2D  _player;

    public override void _Ready()
    {
        _player = GetParent<Node2D>();

        //Get vieport
        var vieport = GetViewport().GetVisibleRect();
        _vieportRect = vieport.Size;
    }

    public override void _Process(float delta)
    {
        float offset = _player.Position.y + _vieportRect.y;
        GlobalPosition = new Vector2(_vieportRect.x/2, offset);
    }

    private void OnArea2DAreaEntered(Area2D area) {
        area.SetProcess(false);
        area.SetPhysicsProcess(false);
        area.Visible = false;
    }
    private void OnDespawnBodyEntered(PhysicsBody2D body) { 
        body.SetProcess(false);
        body.SetPhysicsProcess(false);
        body.Visible = false;
    }
}














