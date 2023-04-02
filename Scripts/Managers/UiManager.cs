using Godot;

public class UiManager : Node
{
    private Button _restartButton;
    private Label _bestScoreLabel;
	private Label _scoreLabel;
	private Timer _couldown;
	private float _increment;
	private int _score;


	public override void _Ready() { 
        _bestScoreLabel = GetNode<Label>("/root/Node/Score/Control/BestScore");
		_scoreLabel = GetNode<Label>("/root/Node/Score/Control/CurrentScore");
        _restartButton = GetNode<Button>("/root/Node/Score/Control/Restart");
		//Disable objects
		_bestScoreLabel.Hide();
		_restartButton.Hide();
		//Create and start timer
		_couldown = new Timer();
		//Add to tree
		AddChild(_couldown);
		_couldown.WaitTime = 1;
		_couldown.Start();
		//Run couldown function
		ScoreTimer();
	}

	private void ScoreIncrement(){
		_scoreLabel.Text = "Score: " + _score++;
	}

	private async void ScoreTimer(){
		await ToSignal(_couldown, "timeout");
		ScoreIncrement();
		//Restart timer
		_couldown.WaitTime = 1;
		_couldown.Start();
		//Run self
		ScoreTimer();
	}
    
	private void EndGame()
	{
		_couldown.Stop();
		//Get high score
		var bestscore = ConfigManager.GetScore();
		//Compare scores
		if(_score > bestscore){
			bestscore = --_score;
			ConfigManager.WriteScore(bestscore);
		}
		//Assign text and show gui items
		_bestScoreLabel.Text = "Best Score: " + bestscore;
		_bestScoreLabel.Show();
		_restartButton.Show();
	}
}


