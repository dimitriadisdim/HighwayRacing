using Godot;
using System;
using System.Runtime.Remoting.Messaging;

public class Score : Node
{
	private int _scoreIncreament;
	private Timer _couldownTimer;
	private Player _player;
	private Int64 _score;

	public override void _Ready()
	{
		//Initialize variables
		_score = 0;
		_scoreIncreament = 1;
		_player = GetNode<Player>("/root/Node/Player");
		//Initialize timer
		_couldownTimer = GetNode<Timer>("./Timer");
		_couldownTimer.WaitTime = .1f;
		_couldownTimer.Start();
		//Start timer function
		ScoreTimer();
	}

	private async void ScoreTimer(){
		await ToSignal(_couldownTimer, "timeout");
		_scoreIncreament = (int)_player.GetSpd() / 700;
		_score += _scoreIncreament;
		//Restart timer
		_couldownTimer.WaitTime = .1f;
		_couldownTimer.Start();
		//Run self
		ScoreTimer();
	}

	public Int64 GetBestScore()
	{
		_couldownTimer.Stop();
		//Get high score
		var bestscore = ConfigManager.GetScore();
		//Compare scores
		if(_score > bestscore){
			bestscore = --_score;
			ConfigManager.WriteScore(bestscore);
		}
		return bestscore;
	}

	public Int64 GetScore() => _score;

	private void OnRestartPressed() => GetTree().ReloadCurrentScene();
}



