using Godot;
using System;

public partial class DogBite : AttackBase
{
    public override void _Ready()
    {
        base._Ready();
        this.BodyEntered += (node) =>
        {
            if (node is EnemyBase enemyBase)
            {
                this.MakeDamage(enemyBase);
            }
        };
        var timer = GetNode<Timer>("AffectTimer");
        timer.Timeout += this.QueueFree;
    }
}