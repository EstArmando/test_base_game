using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
   private Node _NivelActual;
   private Node2D _ContenedorNiveles;

   public override void _Ready()
   {
       _ContenedorNiveles = GetNode<Node2D>("ContenedorNiveles");
         CargarNivel("res://nivel_1.tscn");
   }

   public void CargarNivel(string ruta)
    {
        if (_NivelActual != null)
        {
            _NivelActual.QueueFree();
        }
        var escenaNueva = GD.Load<PackedScene>(ruta);
        _NivelActual = escenaNueva.Instantiate();
        _ContenedorNiveles.AddChild(_NivelActual);

        var spawn = _NivelActual.FindChild("SpawnPoint", recursive: true) as Node2D;
        var jugador = _NivelActual.FindChild("Jugador", recursive: true) as Node2D;

        if (spawn != null && jugador != null)
        {
            jugador.GlobalPosition = spawn.GlobalPosition;
        }
    }

}
