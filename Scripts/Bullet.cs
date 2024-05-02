using Godot;

public partial class Bullet : RigidBody2D
{
    [Export] public PackedScene SmallExplosionScene { get; set; }

    private int _damage = 1;

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("enemies"))
		{
			body.GetNode<Health>("Health").Damage(_damage);
		}

        SpawnSmallExplosion();

        QueueFree();
	}

	private void SpawnSmallExplosion()
	{
        Node2D explosion = SmallExplosionScene.Instantiate<Node2D>();

        explosion.Position = GlobalPosition;

        explosion.Rotation = GlobalRotation;

        GetTree().Root.AddChild(explosion);

        explosion.GetNode<CpuParticles2D>("Particles").Emitting = true;
    }
}
