using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class PlayerBase : Area2D
{
    [Export] public virtual int Attack { get; set; }
    [Export] public virtual int AttackDamageIncr { get; set; }
    [Export] public virtual int Critical { get; set; }
    [Export] public virtual int CriticalDamageIncr { get; set; }

    [Export]
    public float AttackRange
    {
        get => attackRangeCircle?.Radius ?? 0f;
        set
        {
            if (attackRangeCircle != null)
            {
                attackRangeCircle.Radius = value;
            }
        }
    }

    public bool AttackRangeVisibility
    {
        get => attackRangeCollision.Visible;
        set => attackRangeCollision.Visible = value;
    }

    private CircleShape2D attackRangeCircle;
    private CollisionShape2D attackRangeCollision;
    private List<EnemyBase> ememies = new();

    public virtual int Damage
    {
        get
        {
            var attack = Attack * (100 + AttackDamageIncr);
            if (Critical > GD.RandRange(0, 100))
            {
                attack *= (150 + CriticalDamageIncr);
            }

            return attack / 100;
        }
    }

    protected virtual void AttackEnemy(Vector2 position)
    {
        throw new NotImplementedException();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var attackTimer = GetNode<Timer>("AttackTimer");
        attackTimer.Timeout += () => { AttackEnemy(GetEnemyInAttackRange()); };
        attackRangeCollision = GetNode<CollisionShape2D>("AttackRange");
        attackRangeCircle = attackRangeCollision.Shape as CircleShape2D;
        this.BodyEntered += (node) =>
        {
            if (node is not EnemyBase enemyBase)
            {
                return;
            }

            enemyBase.TreeExited += () => { this.ememies.Remove(enemyBase); };
            this.ememies.Add(enemyBase);
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    protected Vector2 GetEnemyInAttackRange()
    {
        if (!ememies.Any())
        {
            return Vector2.Zero;
        }
        var enemy = ememies[GD.RandRange(0, ememies.Count)];
        return enemy.Position;
    }

    protected Node GetSense()
    {
        return this.GetParent();
    }
}