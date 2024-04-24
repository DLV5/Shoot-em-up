using Godot;

public partial class Bullet : RigidBody2D
{
	private int _damage = 1;

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("enemies"))
		{
			body.GetNode<Health>("Health").Damage(_damage);
		}

		QueueFree();
	}
}
