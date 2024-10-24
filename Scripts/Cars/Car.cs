using Godot;


public abstract partial class Car : CharacterBody2D
{
    [Export]protected float maxSpeed;
    [Export]protected int turboCount; //Maybe will move out of car
    [Export]protected int minSpeed;
    [Export]protected float spdX;
    [Export]protected float spd;
    protected float[] lanes;
    protected DataManager data;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        data = GetNode<DataManager>("/root/DataManager");
		if(data == null)
			GD.Print("DataManager is null");
		else
			lanes = DataManager.Lanes;
    }   

    public float GetSpd() => spd;
}
