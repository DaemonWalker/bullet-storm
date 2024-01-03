using Godot;

public static class Share
{
    public static Vector2 GetRandomEnemyPosition(this Node2D node)
    {
        var enemies = node.GetTree().GetNodesInGroup(Constant.GROUP_ENEMY);
        var enemy = enemies[GD.RandRange(0, enemies.Count)];
        return ((Node2D)enemy).Position;
    }
}