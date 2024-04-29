using Godot;
using System.Diagnostics;

public partial class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();

    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("MessagePanel/Message");
        message.Text = text;
        GetNode<Panel>("MessagePanel").Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, Timer.SignalName.Timeout);

        var message = GetNode<Label>("MessagePanel/Message");
        message.Text = "Kill the Creeps!";
        GetNode<Panel>("MessagePanel").Show();

        await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
        GetNode<Button>("StartButton").Show();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScorePanel/ScoreLabel").Text = score.ToString();
    }

    private void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal(SignalName.StartGame);
    }

    private void OnMessageTimerTimeout()
    {
        GetNode<Panel>("MessagePanel").Hide();
    }
}
