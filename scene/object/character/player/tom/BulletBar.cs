using Godot;
using System;

public partial class BulletBar : ProgressBar
{
    private Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("../BulletTimer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        this.Value = 100 - (timer.TimeLeft / timer.WaitTime) * 100;
    }
}