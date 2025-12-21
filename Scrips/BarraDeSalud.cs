using Godot;
using System;

public partial class BarraDeSalud : TextureProgressBar
{
    public override void _Ready()
    {
        var globalState = GetNode<GlobalState>("/root/GlobalState");
       
       MaxValue = globalState.vidaMaxima;
       Value = globalState.vidaActual;

       globalState.OnHealthChange += (current, max) =>
       {
           Value = current;
           GD.Print("Vida actualizada en la barra de salud: " + current + "/" + max);
       };
    }
}
