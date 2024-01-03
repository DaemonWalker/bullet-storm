using Godot;
using System;

[Tool]
public partial class EnemyBase : Node2D
{
    public static readonly float ARMOR_REDUCE = 0.06f;
    [Export] public int Armor { get; set; }
    [Export] public int Attack { get; set; }
    [Export] public int Health { get; set; }

    public virtual void TakeDamage(int damage)
    {
        if (Health <= 0)
        {
            return;
        }

        var d = damage * (1 - ArmorDamageReduce());
        Health = (int)(Health - d);
        if (Health <= 0)
        {
            this.QueueFree();
        }
    }

    protected virtual float ArmorDamageReduce() => (Armor * ARMOR_REDUCE) / (1 + Armor * ARMOR_REDUCE);
}