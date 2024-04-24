using Godot;

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

        // Note that for calling Godot-provided methods with strings,
        // we have to use the original Godot snake_case name.
        GetTree().CallGroup("enemies", Node.MethodName.QueueFree);
    }

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();

        GetNode<HUD>("HUD").ShowGameOver();
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
        // Note: Normally it is best to use explicit types rather than the `var`
        // keyword. However, var is acceptable to use here because the types are
        // obviously Mob and PathFollow2D, since they appear later on the line.

        // Create a new instance of the Mob scene.
        EnemySpawnPoint enemy = EnemySpawnPointScene.Instantiate<EnemySpawnPoint>();

        // Choose a random location on Path2D.
        var enemySpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        enemySpawnLocation.ProgressRatio = GD.Randf();

        // Set the mob's direction perpendicular to the path direction.
        float direction = enemySpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location.
        enemy.Position = enemySpawnLocation.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        enemy.Rotation = direction;

        // Spawn the mob by adding it to the Main scene.
        AddChild(enemy);
    }
}
