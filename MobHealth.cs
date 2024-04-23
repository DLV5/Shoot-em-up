using Godot;

public partial class MobHealth : Node
{
    private int _health = 1;

    public void Damage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            GetParent().QueueFree();
        }
    }
}
