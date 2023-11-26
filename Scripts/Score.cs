using Godot;
using System;
using System.Runtime.Remoting.Messaging;

public class Score : Node
{
	private int _score;
	private int _scoreIncreament;
	private Timer _couldownTimer;

	public override void _Ready()
	{
		//Initialize variables
		_score = 0;
		_scoreIncreament = 1;
		//Initialize timer
		_couldownTimer = GetNode<Timer>("./Timer");
		_couldownTimer.WaitTime = 1;
		_couldownTimer.Start();
		//Start timer function
		ScoreTimer();
	}

	private async void ScoreTimer(){
		await ToSignal(_couldownTimer, "timeout");
		_score += _scoreIncreament;
		//Restart timer
		_couldownTimer.WaitTime = 1;
		_couldownTimer.Start();
		//Run self
		ScoreTimer();
	}

	public int GetBestScore()
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

	public int GetScore() => _score;

	private void OnRestartPressed() => GetTree().ReloadCurrentScene();
}



