using Godot;

public partial class EnemySpawnPoint : Sprite2D
{
    [Export]
    public PackedScene EnemyScene { get; set; }

    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("EnemySpawnTimer");
    }

    public override void _Process(double delta)
    {
        Modulate = new Color(
            Modulate.R,
            Modulate.G,
            Modulate.B,
            (float)(1 - _timer.TimeLeft / _timer.WaitTime));
    }

    public void OnEnemyTimerTimeout()
	{
        Enemy enemy = EnemyScene.Instantiate<Enemy>();

        enemy.Position = GlobalPosition;

        enemy.Rotation = GlobalRotation;

        AddSibling(enemy);

        QueueFree();
    }
}
