using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public interface IState
{
    public void Enter(IState newState);
    public void Update();
    public void Exit();
}