using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;


public partial class GlobalState : Node
{
	 [Signal] public delegate void OnHealthChangeEventHandler(float currentHealth, float maxHealth);

    //datos persistentes
    public float vidaMaxima = 100f;
    private float _vidaActual = 100f;

    private const string SAVE_FILE_PATH = "user://savegame.json";


    public float vidaActual
    {
        get => _vidaActual;
        set
        {
            _vidaActual= Mathf.Clamp(value,0, vidaMaxima);
            EmitSignal(SignalName.OnHealthChange, _vidaActual, vidaMaxima);

            if(_vidaActual <= 0)
            {
                
                // Aquí puedes agregar lógica adicional para manejar la muerte del jugador
            }
        }
    }

    private void morir()
    {
        GD.Print("El jugador ha muerto.");
        var main = GetTree().Root.GetNode<Main>("MAIN");
        main.CallDeferred("resetNivelActual");
    }

    public void GuardarPartida()
    {

        var  inventarioParaJson = new Godot.Collections.Array<string>(Inventario);

        var datos = new Godot.Collections.Dictionary<string, Variant>
        {
            {"vidaActual", vidaActual },
            {"inventario", inventarioParaJson },
            {"nivelActual", GetTree().CurrentScene.SceneFilePath}


        };

        string jsonString = Json.Stringify(datos);

        using var file = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Write);
        file.StoreString(jsonString);   

        GD.Print("Partida guardada.");
    }

        public void CargarPartida()
    {
        if (!FileAccess.FileExists(SAVE_FILE_PATH)) return;

        using var file = FileAccess.Open(SAVE_FILE_PATH, FileAccess.ModeFlags.Read);
        string jsonString = file.GetAsText();

        var json = new Json();
        var error = json.Parse(jsonString);

        if (error == Error.Ok)
        {
           var datos = (Dictionary)json.Data;

           //cargar vida
            if (datos.ContainsKey("vidaActual"))
                vidaActual = (float)datos["vidaActual"];
              //cargar inventario
            if (datos.ContainsKey("inventario"))
            {
                Inventario.Clear();
                var arrayCargado = (Godot.Collections.Array)datos["inventario"];
                foreach (var item in arrayCargado)
                {
                    Inventario.Add((string)item);
                }

            }
            if (datos.ContainsKey("nivelActual"))
            {
                var rutaNivel = (string)datos["nivelActual"];
                var main = GetTree().Root.GetNode<Main>("MAIN");
                main.CallDeferred(nameof(Main.CargarNivel), rutaNivel);
            }
    
            GD.Print("inventario cargado: ");
        }
    }

    public List<string> Inventario = new List<string>();

    public override void _Ready()
    {
        vidaActual = vidaMaxima;
        CargarPartida();
    }
}
