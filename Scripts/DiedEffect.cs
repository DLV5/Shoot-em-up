using Godot;

public partial class DiedEffect : Node2D
{
    [Export] public PackedScene ExplosionScene { get; set; }

    public void OnDied()
    {
        Node2D explosion = ExplosionScene.Instantiate<Node2D>();

        explosion.Position = GlobalPosition;

        explosion.Rotation = GlobalRotation;

        GetTree().Root.AddChild(explosion);

        explosion.GetNode<CpuParticles2D>("Particles").Emitting = true;
    }
}
