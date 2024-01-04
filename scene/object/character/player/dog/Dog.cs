using Godot;
using System;

public partial class Dog : PlayerBase
{
    [Export] public PackedScene AttackType { get; set; }

    protected override void AttackEnemy(Vector2 position)
    {
        base.AttackEnemy(position);
        var bite = AttackType.Instantiate<DogBite>();
        bite.Position = position;
        AddChild(bite);
    }
}