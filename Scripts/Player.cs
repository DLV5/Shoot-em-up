using Godot;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void HitEventHandler();

	[Export]
	public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	public override void _Process(double delta)
	{
		var input = Input.GetVector("move_left", "move_right", "move_up", "move_down");	

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (input.Length() > 0)
		{
            input = input.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Velocity = input;

		MoveAndSlide();

		LookAt(GetGlobalMousePosition());
	}
}


