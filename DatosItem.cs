using Godot;
using System;

[GlobalClass]
public partial class DatosItem : Resource
{
    [Export]public string nombreItem = "Item";
    [Export]public string descripcionItem = "Descripcion del item";
    [Export]public Texture2D iconoItem;
}
