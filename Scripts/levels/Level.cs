using Godot;
using System;

public class Level : Node
{
	[Export] private int _bossTriggerScore;

	public int GetBossTriggerScore() => _bossTriggerScore;
}
