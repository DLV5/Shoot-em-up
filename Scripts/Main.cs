using Godot;
using System.Diagnostics;

public partial class Main : Node
{
    [Export]
    public PackedScene EnemySpawnPointScene { get; set; }
    
    [Export]
    public PackedScene PlayerScene { get; set; }

    private int _score;

    public void NewGame()
    {
        _score = 0;

        Player player = PlayerScene.Instantiate<Player>();
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        player.GetNode<Health>("Health").Died += GameOver;

        AddChild(player);

        GetNode<Timer>("StartTimer").Start();

        var hud = GetNode<HUD>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

        GetTree().CallGroup("enemies", Node.MethodName.QueueFree);
    }

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();

        GetNode<HUD>("HUD").ShowGameOver();

        GetTree().CallGroup("enemies", Node.MethodName.QueueFree);
    }


    private void OnScoreTimerTimeout()
    {
        _score++;
        GetNode<HUD>("HUD").UpdateScore(_score);
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    private void OnMobTimerTimeout()
    {
        EnemySpawnPoint enemy = EnemySpawnPointScene.Instantiate<EnemySpawnPoint>();

        var enemySpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        enemySpawnLocation.ProgressRatio = GD.Randf();

        float direction = enemySpawnLocation.Rotation + Mathf.Pi / 2;

        enemy.Position = enemySpawnLocation.Position;

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        enemy.Rotation = direction;

        AddChild(enemy);
    }
}
