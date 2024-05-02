using Godot;

public partial class Health : Node
{
    [Signal]
    public delegate void DiedEventHandler();

    [Export] private int _health = 1;

    private AudioStreamPlayer2D _damagedAudio;

    public override void _Ready()
    {
        _damagedAudio = GetTree().Root.GetNode("Main").GetNode<AudioStreamPlayer2D>("HitSfx");
    }

    public void Damage(int damage)
    {
        _health -= damage;

        _damagedAudio.Play();

        if (_health <= 0)
        {
            EmitSignal(SignalName.Died);
            GetParent().QueueFree();
        }
    }
}
