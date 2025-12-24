using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PanelInventario : Panel
{
    private ItemList _Listavisual;
    private GlobalState _globalState;

    public override void _Ready()
    {
        _Listavisual = GetNode<ItemList>("ItemList");
        _globalState = GetNode<GlobalState>("/root/GlobalState");
        Hide();
    }

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("abrir_inventario"))
        {
            Visible = !Visible;

            if(Visible)
            {
                ActualizarLista();
            }
        }
    }

    public void ActualizarLista()
    {
        _Listavisual.Clear();

        foreach (string nombre in _globalState.Inventario)
        {
            _Listavisual.AddItem(nombre); 
        }
    }


}
