using Godot;
using System;

public partial class BossSpawner : Node2D
{
	[Export] private PackedScene _bossScene;
	
	private void OnLevelManagerBossFight()
	{
		Node2D boss = _bossScene.Instantiate() as Node2D;
		//Add to scene tree
		AddChild(boss);
	}	
}

