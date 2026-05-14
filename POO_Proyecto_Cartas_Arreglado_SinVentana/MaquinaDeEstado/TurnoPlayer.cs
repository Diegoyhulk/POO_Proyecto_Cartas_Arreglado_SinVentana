using System.Drawing;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class TurnoPlayer : IState
{
    public void Enter(IState newState)
    {
        if (GameManager.Instance.mazo.coleccion.Count == 0)
        {
            GameManager.Instance.mazo.Shuffle(GameManager.Instance.coleccion.cartas);
        }
        //Turno
        MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}