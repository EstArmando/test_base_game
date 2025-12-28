using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class InventarioInterfaz : Panel
{
	private ItemList _Listavisual;
	private GlobalState gs;


	public override void _Ready()
	{
		_Listavisual = GetNode<ItemList>("ItemList");
		gs = GetNode<GlobalState>("/root/GlobalState");

		Visible = false;
	}


	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("Inventario"))
		{
			Visible = !Visible;

			if(Visible)
			{
			 ActualizarLista();
			 GetTree().Paused = true;   
			}
			else
			{
				GetTree().Paused = false;
			}
		}

	}

	public void ActualizarLista()
	{
		_Listavisual.Clear();

		foreach(var item in gs.Inventario)
		{
			_Listavisual.AddItem(item.nombreItem,item.iconoItem);
		}
	}


}
