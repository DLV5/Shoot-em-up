using Godot;

public partial class Bullet : RigidBody2D
{
	private int _damage = 1;

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("mobs"))
		{
			body.GetNode<MobHealth>("MobHealth").Damage(_damage);
		}

		QueueFree();
	}
}
