using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class EnemyState: IState
{
    public void Enter(IState newState)
    {
        //Turno Enemigo
        TurnosEnemigos(GameManager.Instance.coleccion, GameManager.Instance.mazo, GameManager.Instance.player, GameManager.Instance.enemies
            , GameManager.Instance.ai, GameManager.Instance.num, GameManager.Instance.players, GameManager.Instance.comando, GameManager.Instance.texturas);
        //Acaba el turno
        ComprobarOrganosSaludables(GameManager.Instance.players);
        MaquinaEstado.Instance.ChangeState(new TurnoPlayer());
    }
    private static void TurnosEnemigos(Coleccion coleccion, Mazo<Cartas> mazo, Player player, Enemy[] enemy, EnemyAI ai,
        int num, List<Jugador> players,  EspecialesC comando, Dictionary<Cartas, Texture2D> texturas)
    {
        Random rng = new Random();
        int tiempo = rng.Next(1000, 2000); // entre 0.5 y 1.5 segundos
        if (num >= 1)
        {
            MaquinaEstado.Instance.ChangeState(new NoPlayerTunro());
            Thread.Sleep(tiempo);
            ai.ETurno(enemy,mazo,coleccion.cartas,player, comando,players,0, num);
            MaquinaEstado.Instance.ChangeState(new NoPlayerTunro());
            Thread.Sleep(tiempo);
        }
        if (GameManager.Instance.mazo.coleccion.Count == 0)
        {
            GameManager.Instance.mazo.Shuffle(GameManager.Instance.coleccion.cartas);
        }
        if (num >= 2)
        {
            ai.ETurno(enemy,mazo,coleccion.cartas,player, comando, players,1, num);
            MaquinaEstado.Instance.ChangeState(new NoPlayerTunro());
            Thread.Sleep(tiempo);
        }
        if (GameManager.Instance.mazo.coleccion.Count == 0)
        {
            GameManager.Instance.mazo.Shuffle(GameManager.Instance.coleccion.cartas);
        }
        if (num == 3)
        {
            ai.ETurno(enemy, mazo, coleccion.cartas, player, comando, players, 2, num);
            MaquinaEstado.Instance.ChangeState(new NoPlayerTunro());
            Thread.Sleep(tiempo);
        }
    }
    private void ComprobarOrganosSaludables(List<Jugador> players)
    {
        foreach (Jugador player in players)
        {
            int orgsal=0;
            foreach (Cartas cart in  player.organos)
            {
                if (cart is Organos org)
                {
                    if(org.HP >= 2)
                        orgsal++;
                }
                if (orgsal == 4)
                {
                    if (player is Player)
                    {
                        GameManager.Instance.winstate = true;
                    }
                    else if (player is Enemy)
                    {
                        GameManager.Instance.winstate = false;
                    }
                    MaquinaEstado.Instance.ChangeState(new EndState());
                }
            }
        }
        
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}