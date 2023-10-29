using Godot;
using System;

public class Despawn : Area2D
{
    private void OnArea2DAreaEntered(Area2D area) => area.SetProcess(false);

    private void OnDespawnBodyEntered(PhysicsBody2D body) => body.SetProcess(false);
}














