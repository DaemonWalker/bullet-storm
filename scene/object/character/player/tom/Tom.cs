using Godot;
using System;

public partial class Tom : Area2D
{
    [Export] public PackedScene Bullet { get; set; }
    [Export] private int Speed { get; set; } = 200;

    [Signal]
    public delegate void AttackEventHandler(TomBullet bullet);

    public Marker2D BulletMarker { get; set; }
    private Vector2 _screenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var timer = GetNode<Timer>("BulletTimer");
        BulletMarker = GetNode<Marker2D>("BulletMarker");
        timer.Timeout += () =>
        {
            var bullet = Bullet.Instantiate<TomBullet>();

            EmitSignal(SignalName.Attack, bullet);
        };
        var animate = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animate.Play();

        _screenSize = GetViewportRect().Size;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var changed = false;
        var velocity = Vector2.Zero; // The player's movement vector.
        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y += -1;
            changed = true;
        }
        else if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
            changed = true;
        }

        if (!changed)
        {
            return;
        }

        velocity = velocity.Normalized() * Speed;
        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, _screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, _screenSize.Y)
        );
    }
}