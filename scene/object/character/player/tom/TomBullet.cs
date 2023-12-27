using Godot;
using System;

public partial class TomBullet : Area2D
{
    public int Attack { get; set; } = 50;
    public int AttackIncr { get; set; } = 0;
    public int Critical { get; set; } = 0;
    public int CriticalDamage { get; set; } = 50;

    public int Damage
    {
        get
        {
            var damage = Attack * (100 + AttackIncr);
            if (Critical >= GD.RandRange(1, 101))
            {
                damage = damage * (100 + CriticalDamage);
            }

            return damage / 100;
        }
    }

    private Vector2 ScreenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        var notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        notifier.ScreenExited += this.QueueFree;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero; // The player's movement vector.
        velocity.X += 1;
        velocity = velocity.Normalized() * 500;
        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
}