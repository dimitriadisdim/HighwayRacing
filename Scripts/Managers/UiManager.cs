using Godot;

public class UiManager : Node
{
    private Label _scoreLabel;
    private Timer _couldown;
    private float _increment;
    private float _score;


    public override void _Ready() { 
        _scoreLabel = GetNode<Label>("/root/Node/Score/Control/Label");
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
}
