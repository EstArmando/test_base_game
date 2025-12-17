using Godot;
using System;
using System.ComponentModel.Design.Serialization;
using System.Xml.Serialization;

public partial class GlobalState : Node
{
	// dirrecciones de las escenas de jugador y camara
	private const string PlaerScenePath = "res://NODOS/Personaje.tscn";
	 private const string CameraScenePath = "res://NODOS/game_camera.tscn";
private const string InitialLevelPath = "res://nive_1_prueba.tscn";
	private readonly Vector2 initialPlayerPosition = new Vector2(100, 400);



	 //codido que carga las escenas
	private PackedScene playerScene = ResourceLoader.Load<PackedScene>(PlaerScenePath);
	private PackedScene cameraScene = ResourceLoader.Load<PackedScene>(CameraScenePath);

//instancias de jugador y camara
	private CharacterBody2d playerInstance;
	private Camera2D cameraInstance;


//metodo ready que instancia y agrega las escenas como hijos
	public override void _Ready()
	{
		//verificar si las escenas se cargaron correctamente
		if (playerScene != null)
		{
			playerInstance = playerScene.Instantiate<CharacterBody2d>();
			AddChild(playerInstance);
		}

		// load_scene("res://levels/Level_1.tscn");

		if (cameraScene != null)
        {
            cameraInstance = cameraScene.Instantiate<Camera2D>();
            AddChild(cameraInstance);
			
			cameraInstance.MakeCurrent();
        }
		//establecer la posicion inicial del jugador
		if (playerInstance != null)
		{
			LoadScene(InitialLevelPath, initialPlayerPosition);
		}


	}


	//metodo para cargar escenas con posicion de spawn
	public void LoadScene(string path,Vector2 spawnPosition)
	{
		GetTree().ChangeSceneToFile(path);

		if(playerInstance != null)
		{
			playerInstance.Position = spawnPosition;
		}
	}




}
