using Godot;
using System;

public partial class GameCamera : Camera2D
{
    	private CharacterBody2d player;

        //velocidad de interpolacion
	[Export] public float LerpSpeed { get; set; } = 5.0f;

	[Export] public float MaplimitLeft { get; set; } = 0f;
	[Export] public float MaplimitRight { get; set; } = 80000f;
	[Export] public float MaplimitTop { get; set; } = 0f;
	[Export] public float MaplimitBottom { get; set; } = 4000f;	

	private Vector2 halfViewportSize;

//metodo ready que busca la instancia del jugador en el nodo padre
	public override void _Ready()
	{
		Node parentNode = GetParent();

//buscar el jugador en los hijos del nodo padre y asignarlo
		if(parentNode != null)
		{
			foreach (var child in parentNode.GetChildren())
			{
				if (child is CharacterBody2d playerBody && playerBody.IsInGroup("player"))
				{
					player = playerBody;
					GD.Print("Player found for CameraFollow");
					break;

				}
			}
		}

		if (player == null)
		{
			GD.PrintErr("NO SE ENCONTRO AL FUCKING JUGADOR PARA LA CAMARA");
		}

	halfViewportSize = GetViewportRect().Size / 2;

	}


//metodo de proceso fisico que sigue al jugador con interpolacion
	public override void _PhysicsProcess(double delta)
	{
		if (player != null)
		{
			Vector2 targetPosition = player.Position;

			float clampedX = Mathf.Clamp(targetPosition.X, MaplimitLeft + halfViewportSize.X, MaplimitRight - halfViewportSize.X);
			float clampedY = Mathf.Clamp(targetPosition.Y, MaplimitTop + halfViewportSize.Y, MaplimitBottom - halfViewportSize.Y);
			Vector2 finalTargetPosition = new Vector2(clampedX, clampedY);

			Position = Position.Lerp(finalTargetPosition, LerpSpeed * (float)delta);
		}
	}

}
