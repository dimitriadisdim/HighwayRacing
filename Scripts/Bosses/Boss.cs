using Godot;
using System;

public abstract partial class Boss : Node2D
{
    [Signal]public delegate void DefeatedEventHandler();


    public override void _Ready()
    {
        TriggerSinal();
    }

    private void TriggerSinal() => EmitSignal("Defeated");
}
