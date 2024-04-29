using Godot;
using System.Diagnostics;
using System.Runtime.Intrinsics;

public partial class Enemy : CharacterBody2D
{
    private Player _player;

    [Export] private float _speed = 200f;
    [Export] private int _damage = 1;
    [Export] private float _attackPerSecond = 2f;

    private float _attackSpeed;
    private float _timeUntilAttack;

    private bool _withinAttackRange = false;

    public override void _Ready()
    {
        _player = GetTree().Root.GetNode("Main").GetNode<Player>("Player");

        _attackSpeed = 1 / _attackPerSecond;
        _timeUntilAttack = _attackSpeed;
    }

    public override void _Process(double delta)
    {
        if (_withinAttackRange && _timeUntilAttack <= 0)
        {
            Attack();
            _timeUntilAttack = _attackSpeed;
        }
        else
            _timeUntilAttack -= (float) delta;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_player != null)
        {
            LookAt(_player.GlobalPosition);
            Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * _speed;
        } else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    public void OnAttackRangeBodyEnter(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            _withinAttackRange = true;
        }
    }
    
    public void OnAttackRangeBodyExit(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            _withinAttackRange = false;
            _timeUntilAttack = _attackSpeed;
        }
    }

    private void Attack()
    {
       _player.GetNode<Health>("Health").Damage(_damage);
    }
}
