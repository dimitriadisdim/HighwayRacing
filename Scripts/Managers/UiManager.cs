using Godot;

public partial class UiManager : Node
{
    private Button _restartButton;
    private Label _bestScoreLabel;
	private Label _scoreLabel;
	private Label _turboLabel;
	private Score _scoreNode;
	private Player _player;

	public override void _Ready() 
	{ 
        _bestScoreLabel = GetNode<Label>("/root/Node/Score/Control/BestScore");
		_scoreLabel = GetNode<Label>("/root/Node/Score/Control/CurrentScore");
        _restartButton = GetNode<Button>("/root/Node/Score/Control/Restart");
		_turboLabel = GetNode<Label>("/root/Node/Score/Control/TurboLabel");
		_player = GetNode<Player>("/root/Node/Player");
		//Disable objects
		_bestScoreLabel.Hide();
		_restartButton.Hide();
		//Retrieve score
		_scoreNode = GetNode<Score>("/root/Node/Score");
		//Connect signal
		_player.Connect("EndGame", new Callable(this, "EndGame"));
	}

    public override void _Process(double delta)
    {
		ShowScore();
		ShowTurbo();
    }

    private void ShowScore(){
		_scoreLabel.Text = "Score: " + _scoreNode.GetScore();
	}

	private void ShowTurbo(){
		_turboLabel.Text = "Turbo: " + _player.GetTurbo();
	}
    
	private void EndGame()
	{
		//Assign text and show gui items
		_bestScoreLabel.Text = "Best Score: " + _scoreNode.GetBestScore();
		_bestScoreLabel.Show();
		_restartButton.Show();
	}
}


