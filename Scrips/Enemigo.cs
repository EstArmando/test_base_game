using Godot;
using System;

public partial class Enemigo : Area2D
{

  [Export] public float FuerzadeEmpuje = 600f;
    private bool _jugadorCerca = false;
    [Export] public float daño = 10f;
    private GlobalState _globalState;
    public override void _Ready()
    {
        _globalState = GetNode<GlobalState>("/root/GlobalState");

        BodyEntered += OnBodyEntered;
    }


    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            _jugadorCerca = true;
            GetNode<Timer>("Timer").Start();
            Atacar(body);
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            _jugadorCerca = false;
            GetNode<Timer>("Timer").Stop();
        }
    }

    private void OnTimerTimeout()
    {
        if (_jugadorCerca)
        {
        _globalState.vidaActual -= daño;
        }
    }


    private void Atacar(Node2D jugador)
    {
        GD.Print("Enemigo ataca al jugador un total de " + daño + " de daño."+"y con vida actual de: "+_globalState.vidaActual);
        _globalState.vidaActual -= daño;

        // Aplicar knockback al jugador
        jugador.Call("AplicarKnockback", GlobalPosition, FuerzadeEmpuje);
    GD.Print("Knockback aplicado al jugador con fuerza: " + FuerzadeEmpuje);
    }

}
