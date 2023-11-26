using Godot;
using System;

public class Level : Node
{
	[Export] int bossTriggerScore;

	public int GetBossTriggerScore() => bossTriggerScore;
}
