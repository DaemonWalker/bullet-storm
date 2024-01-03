#if TOOLS
using Godot;
using System;

[Tool]
public partial class PlugPlayerBase : EditorPlugin
{
    private static readonly string PLUGIN_NAME = "PlayerBase";
    private static readonly string PLUGIN_AUTOLOAD_NAME = "PlayBase_AutoLoad";

    public override void _EnterTree()
    {
        // Initialization of the plugin goes here.
        var script = GD.Load<Script>("res://scene/object/character/player/PlayerBase.cs");
        var texture = GD.Load<Texture2D>("res://art/icon.svg");
        AddCustomType(PLUGIN_NAME, "Node", script, texture);

        AddAutoloadSingleton(PLUGIN_AUTOLOAD_NAME, "res://scene/object/character/player/player_base.tscn");
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        RemoveCustomType(PLUGIN_NAME);

        RemoveAutoloadSingleton(PLUGIN_AUTOLOAD_NAME);
    }
}
#endif