using Godot;
using System;

public partial class AttackBar : ProgressBar
{
    private Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("../AttackTimer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        this.Value = 100 - (timer.TimeLeft / timer.WaitTime) * 100;
    }
}