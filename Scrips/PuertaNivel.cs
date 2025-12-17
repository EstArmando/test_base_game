using Godot;
using System;

public partial class PuertaNivel : Area2D
{
    [Export] public String NextLevelPath { get; set; }  = "res://level_2.tscn";
    [Export] public Vector2 SpawnPosition { get; set; } = new Vector2(100, 100);

    private void _on_body_entered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            GlobalState globalState = GetNode<GlobalState>("/root/GlobalState");

            if (globalState != null)
            {
                globalState.LoadScene(NextLevelPath, SpawnPosition);
            } 
        }
    }

    
}
