using Godot;

public class UiManager : Node
{
    private Button _restartButton;
    private Label _bestScoreLabel;
	private Label _scoreLabel;
	private Score _scoreNode;

	public override void _Ready() { 
        _bestScoreLabel = GetNode<Label>("/root/Node/Score/Control/BestScore");
		_scoreLabel = GetNode<Label>("/root/Node/Score/Control/CurrentScore");
        _restartButton = GetNode<Button>("/root/Node/Score/Control/Restart");
		//Disable objects
		_bestScoreLabel.Hide();
		_restartButton.Hide();
		//Retrieve score
		_scoreNode = GetNode<Score>("/root/Node/Score");
	}

    public override void _Process(float delta)
    {
		ShowScore();
    }

    private void ShowScore(){
		_scoreLabel.Text = "Score: " + _scoreNode.GetScore();
	}
    
	private void EndGame()
	{
		//Assign text and show gui items
		_bestScoreLabel.Text = "Best Score: " + _scoreNode.GetBestScore();
		_bestScoreLabel.Show();
		_restartButton.Show();
	}
}


