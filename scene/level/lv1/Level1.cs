using Godot;
using System;

public partial class Level1 : Node
{
    [Export] public PackedScene[] Enemy { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var tom = GetNode<Tom>("Tom");
        var marker = GetNode<Marker2D>("PlayerMarker");
        tom.Position = marker.Position;

        var timer = GetNode<Timer>("EnemyTimer");
        timer.Timeout += () =>
        {
            //var idx = GD.RandRange(0, Enemy.Length);
            var fish = Enemy[0].Instantiate<Fish>();

            var spawnLocation = GetNode<PathFollow2D>("EnemySpawn/EnemySpawnLocation");
            spawnLocation.ProgressRatio = GD.Randf();

            fish.Position = spawnLocation.Position;
            fish.Rotation = Mathf.Pi;

            AddChild(fish);
        };

        tom.Attack += (bullet) =>
        {
            bullet.GlobalPosition = tom.BulletMarker.GlobalPosition;
            AddChild(bullet);
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}