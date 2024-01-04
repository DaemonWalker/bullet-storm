using Godot;
using System;

[Tool]
public partial class AttackBase : Area2D
{
    private PlayerBase player;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (this.GetParent() is not PlayerBase playerBase)
        {
            throw new Exception();
        }

        player = playerBase;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    protected virtual void MakeDamage(EnemyBase enemyBase)
    {
        enemyBase.TakeDamage(player.Damage);
    }
}