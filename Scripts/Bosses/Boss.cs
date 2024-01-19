using Godot;
using System;

public abstract class Boss : Node2D
{
    [Signal]public delegate void Defeated();


    public override void _Ready()
    {
        TriggerSinal();
    }

    private void TriggerSinal() => EmitSignal("Defeated");
}
