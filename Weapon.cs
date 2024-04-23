using Godot;

public partial class Weapon : Node2D
{
    [Export]
    public PackedScene BulletScene { get; set; }

    private int _damage = 1;

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("mouse_click"))
        {
            OnMouseClick();
        }
    }

    private void OnMouseClick()
	{
		RigidBody2D bullet = BulletScene.Instantiate<RigidBody2D>();

        bullet.Position = GlobalPosition;

        bullet.Rotation = GlobalRotation;

        bullet.LinearVelocity = bullet.Transform.X * 200;

        GetTree().Root.AddChild(bullet);
    }
}
