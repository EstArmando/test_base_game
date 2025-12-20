using Godot;
using System;

public partial class PuertaNivel : Area2D
{
    [Export] public string nivelASaltar = "res://nivel_2.tscn";
   
    public override void _Ready()
    {
       BodyEntered  += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {  
            if(body.IsInGroup("player"))
            {
                var globalState = GetTree().Root.GetNode<GlobalState>("GlobalState");
                globalState.GuardarPartida();

                var main = GetTree().Root.GetNode<Main>("MAIN");
               
                main.CallDeferred(nameof(Main.CargarNivel), nivelASaltar);

            }
    }
}
