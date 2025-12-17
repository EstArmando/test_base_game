using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{

//Referencia al nodo GlobalState para guardar la posicion del jugador al cambiar de nivel
    private GlobalState globalState;

    ///Declaro variables exportadas para poder modificarlas desde el editor
    [Export]
    public float speed = 300.0f;
    [Export]
    public float jumpVelocity = -400.0f;

    [Export]
    public float gravity = 1200.0f;



//Referencia al sprite para poder cambiar la animacion segun el movimiento
    private Sprite2D sprite;

//Inicializo el nodo sprite
    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("ProtaRediseño");

    }

    public override void _PhysicsProcess(double delta)
    {
        
        Vector2 velocidadNueva = Velocity;
        //Indico la gravedad
        if(!IsOnFloor())
        {
            velocidadNueva.Y += gravity * (float)delta; 
        }
        //indicando el salto 
        if(Input.IsActionJustPressed("saltar") && IsOnFloor())
        {
            velocidadNueva.Y = jumpVelocity;
        }
        //Movimiento lateral
        float direccionX = Input.GetAxis("izquierda", "derecha");

        if(direccionX != 0)
        {
            //Si se mueve en X, la velocidad en X es la direccion por la velocidad(resumen es la velocidad de movimiento)
            velocidadNueva.X = direccionX * speed;

            //Cambio la animacion del sprite dependiendo de la direccion
            if(direccionX < 0)
            {
                sprite.FlipH = true;
            }
            else
            {
                sprite.FlipH = false;
            }
        }
        else
        {
            //Si no se mueve en X, la velocidad en X es 0
            velocidadNueva.X = Mathf.MoveToward(velocidadNueva.X, 0, speed);
        }
        Velocity = velocidadNueva;
        MoveAndSlide(); 

    }

}
    