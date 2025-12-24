using Godot;
using System;

public partial class Botiquin : Area2D
{
    GlobalState _globalState;

    public override void _Ready()
    {
        _globalState = GetNode<GlobalState>("/root/GlobalState");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            // Aumentar la vida del jugador al recoger el botiquín
            float vidaRecuperada = 20f; // Cantidad de vida que recupera el botiquín
            _globalState.vidaActual += vidaRecuperada;

            // Asegurarse de que la vida no exceda la vida máxima
            if (_globalState.vidaActual > _globalState.vidaMaxima)
            {
                _globalState.vidaActual = _globalState.vidaMaxima;
            }

            GD.Print($"Botiquín recogido. Vida actual del jugador: {_globalState.vidaActual}");

            // Eliminar el botiquín del juego
            QueueFree();
        }
    }

}
