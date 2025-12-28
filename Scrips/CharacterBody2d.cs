using Godot;
using System;
using System.Threading.Tasks;

public partial class CharacterBody2d : CharacterBody2D
{

//Referencia al nodo GlobalState para guardar la posicion del jugador al cambiar de nivel
    private GlobalState _globalState;
    //Variables para el knockback
      private Vector2 _knockbackVelocity = Vector2.Zero;
    private bool _isBeingKnockedBack = false;

    private bool _IsInvulnerable = false;


    ///Declaro variables exportadas para poder modificarlas desde el editor
    [Export]
    public float speed = 300.0f;
    [Export]
    public float jumpVelocity = -400.0f;

    [Export]
    public float gravity = 1200.0f;



//Referencia al sprite para poder cambiar la animacion segun el movimiento
    private Sprite2D sprite => GetNode<Sprite2D>("ProtaRediseño");

//Inicializo el nodo sprite
    public override void _Ready()
    {
       
        _globalState = GetNode<GlobalState>("/root/GlobalState");
        GD.Print($"Prota cargado con {_globalState.vidaActual} de vida");
    }


    public void recibirDaño(float daño)
    {
        _globalState.vidaActual -= daño;
        if (_globalState.vidaActual <= 0)
        {
            GD.Print("El jugador ha muerto");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        //Manejo del knockback
        if(_isBeingKnockedBack)
        {
            // Aplicar el knockback
            Velocity = _knockbackVelocity;
         
            // Reducir gradualmente el knockback
            _knockbackVelocity = _knockbackVelocity.Lerp(Vector2.Zero, 0.1f);
            

            if(_knockbackVelocity.Length()< 10f)
            {
                _isBeingKnockedBack = false;
            }
             MoveAndSlide(); 
        }
        else
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
      
        }
        MoveAndSlide();
        

    }

//Metodo para aplicar knockback al personaje
    public void AplicarKnockback(Vector2 direccion, float fuerza)
    {
        // Calculo la velocidad de knockback
        Vector2 direccionKnockback = (GlobalPosition - direccion).Normalized();
        // Aplico la fuerza del knockback
        _knockbackVelocity = direccionKnockback * fuerza;
        _isBeingKnockedBack = true;
    }

    public async Task ActivcarInvulnerabilidad(float duracion)
    {
        _IsInvulnerable = true;
        GetNode<Sprite2D>("Personaje").Modulate = new Color(1.0f, 0.5f, 0.5f);

        await ToSignal(GetTree().CreateTimer(duracion), "timeout");

        _IsInvulnerable = false;
        GetNode<Sprite2D>("Personaje").Modulate = new Color(1.0f, 1.0f, 1.0f);
      
    }

}
    