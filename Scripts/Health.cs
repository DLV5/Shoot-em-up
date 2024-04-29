using Godot;
using System.Diagnostics;

public partial class Health : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    private int _health = 1;

    public void Damage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            EmitSignal(SignalName.Died);
            GetParent().QueueFree();
        }
    }
}
