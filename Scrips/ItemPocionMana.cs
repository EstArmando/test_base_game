using Godot;
using System;

public partial class ItemPocionMana : Area2D
{
    [Export] public string NombreItem = "Pocion de Mana";

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            var globalState = GetNode<GlobalState>("/root/GlobalState");
            globalState.Inventario.Add(NombreItem);
            GD.Print("Item recogido: " + NombreItem);
            QueueFree();
        }
    }

}
