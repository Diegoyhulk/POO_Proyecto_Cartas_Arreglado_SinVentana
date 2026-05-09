using System.Drawing;
using POO_Proyecto_Cartas_Arreglado_SinVentana.ASSCCI;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Mesa
{
    private bool win;
    private bool lose;
    private Program program = new  Program();
    private int eorgsal = 0;
    private static Printer print = new Printer();
    public event Action<bool,bool> FinalizarPartida;
    public int turnos { get; set; }
    public void Turno(ref Coleccion coleccion, ref Mazo<Cartas> mazo, ref Player player, ref Enemy[] enemy,
        ref EnemyAI ai, ref EspecialesC comando, int num, List<Jugador> players, Dictionary<Cartas, Texture2D> texturas)
    {
        Clear();
        if (mazo.coleccion.Count == 0)
        {
            mazo.Shuffle(coleccion.cartas);
        }
        //Turno
        while(true)
            if(print.TurnoPlayer(player,enemy, coleccion.cartas, mazo, num, players, texturas)){break;}
        //Turno Enemigo
        ForegroundColor = ConsoleColor.Red;
        TurnosEnemigos(coleccion, mazo, player, enemy, ai, num, players, comando, texturas);
        ForegroundColor = ConsoleColor.Gray;
        //Acaba el turno
        turnos++;
        ComprobarOrganosSaludables(players,ref win,ref lose);
    }

    private static void TurnosEnemigos(Coleccion coleccion, Mazo<Cartas> mazo, Player player, Enemy[] enemy, EnemyAI ai, int num, List<Jugador> players,  EspecialesC comando, Dictionary<Cartas, Texture2D> texturas)
    {
        Random rng = new Random();
        int tiempo = rng.Next(1000, 2000); // entre 0.5 y 1.5 segundos
        if (num >= 1)
        {
            print.PrintearMesa(player, enemy,texturas);
            Thread.Sleep(tiempo);
            ai.ETurno(enemy,mazo,coleccion.cartas,player, comando,players,0, num);
            print.PrintearMesa(player, enemy, texturas);
            Thread.Sleep(tiempo);
        }
        if (num >= 2)
        {
            ai.ETurno(enemy,mazo,coleccion.cartas,player, comando, players,1, num);
            print.PrintearMesa(player, enemy, texturas);
            Thread.Sleep(tiempo);
        }
        if (num == 3)
        {
            ai.ETurno(enemy, mazo, coleccion.cartas, player, comando, players, 2, num);
            print.PrintearMesa(player, enemy, texturas);
            Thread.Sleep(tiempo);
        }
    }

    private void ComprobarOrganosSaludables(List<Jugador> players,ref bool win, ref bool lose)
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
                        win = true;
                    }
                    else if (player is Enemy)
                    {
                        lose = true;
                    }
                    FinalizarPartida?.Invoke(win,lose);
                }
            }
        }
        
    }
}