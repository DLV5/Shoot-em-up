using Godot;

public partial class Weapon : Node2D
{
    [Export] public PackedScene BulletScene { get; set; }

    [Export] private float _bulletSpeed = 400f;

    [Export] private float _bulletsPerSecond;

    private int _damage = 1;

    private float _fireRate = 0f;

    private float _timeUntilFire = 0f;

    private Node2D _shootPoint;

    private AudioStreamPlayer2D _shootAudio;

    public override void _Ready()
    {
        _fireRate = 1 / _bulletsPerSecond;

        _shootAudio = GetTree().Root.GetNode("Main").GetNode<AudioStreamPlayer2D>("ShootSfx");

        _shootPoint = GetNode<Node2D>("ShootPoint");
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());

        if (Input.IsActionPressed("mouse_click") && _timeUntilFire > _fireRate)
        {
            OnMouseClick();

            _timeUntilFire = 0f;
        } else
        {
            _timeUntilFire += (float) delta;
        }
    }

    private void OnMouseClick()
	{
		RigidBody2D bullet = BulletScene.Instantiate<RigidBody2D>();

        bullet.Position = _shootPoint.GlobalPosition;

        bullet.Rotation = GlobalRotation;

        bullet.LinearVelocity = bullet.Transform.X * _bulletSpeed;

        GetTree().Root.AddChild(bullet);

        _shootAudio.Play();
    }
}
