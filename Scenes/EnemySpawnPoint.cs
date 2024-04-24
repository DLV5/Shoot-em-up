using Godot;

public partial class EnemySpawnPoint : Sprite2D
{
    [Export]
    public PackedScene EnemyScene { get; set; }

    public void OnEnemyTimerTimeout()
	{
        Enemy enemy = EnemyScene.Instantiate<Enemy>();

        enemy.Position = GlobalPosition;

        enemy.Rotation = GlobalRotation;

        AddSibling(enemy);

        QueueFree();
    }
}
