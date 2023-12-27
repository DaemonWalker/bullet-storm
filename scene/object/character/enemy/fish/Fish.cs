using Godot;
using System;

public partial class Fish : Area2D
{
    [Export] public int Health { get; set; } = 100;
    private Vector2 ScreenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
        notifier.ScreenExited += this.QueueFree;
        ScreenSize = GetViewportRect().Size;

        this.AreaEntered += (node) =>
        {
            if (node is TomBullet bullet)
            {
                this.Health -= bullet.Damage;
            }
            GD.Print("body entered", " ", Health);

            if (this.Health <= 0)
            {
                this.QueueFree();
            }
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var velocity = Vector2.Zero; // The player's movement vector.
        velocity.X -= 1;
        velocity = velocity.Normalized() * 100;
        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
}