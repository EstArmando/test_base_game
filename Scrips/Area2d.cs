using Godot;
using System;

public partial class Area2d : Area2D
{
    [Export] public DatosItem datosItem;

    public override void _Ready()
    {
        if(datosItem != null)
        {
            GetNode<Sprite2D>("Sprite2D").Texture = datosItem.iconoItem;
            BodyEntered += OnBodyEntered;
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if(body.IsInGroup("player"))
        {
            var gs = GetNode<GlobalState>("/root/GlobalState");
            gs.Inventario.Add(datosItem);
                       
            QueueFree();

            var label = GetTree().Root.GetNode<Main>("MAIN").GetNode<CanvasLayer>("CanvasLayer").GetNode<Label>("Label");
            label.Text = "Has recogido: " + datosItem.nombreItem;
            label.Show();
            label.GetTree().CreateTimer(2.0f).Timeout += () => label.Hide();
        }
    }


}
