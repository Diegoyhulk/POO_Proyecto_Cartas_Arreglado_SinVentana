using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class MaquinaEstado
{
    private static MaquinaEstado _instance;
    public static MaquinaEstado Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MaquinaEstado();

            return _instance;
        }
    }
    private MaquinaEstado() { } 
    private IState _currentState;
    
    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(newState);
    }

    public void Update()
    {
        _currentState?.Update();
    }

}