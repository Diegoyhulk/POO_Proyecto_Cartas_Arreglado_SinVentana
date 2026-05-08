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
    private Program program;
    private int orgsal = 0;
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
        ComprobarOrganosSaludables(players,ref win,ref lose, num);
    }

    private static void TurnosEnemigos(Coleccion coleccion, Mazo<Cartas> mazo, Player player, Enemy[] enemy, EnemyAI ai, int num, List<Jugador> players,  EspecialesC comando, Dictionary<Cartas, Texture2D> texturas)
    {
        Random rng = new Random();
        int tiempo = rng.Next(1000, 2000); // entre 0.5 y 1.5 segundos
        if (num >= 1)
        {
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

    private void ComprobarOrganosSaludables(List<Jugador> players,ref bool win, ref bool lose, int num)
    {
        orgsal = 0;
        foreach (Jugador player in players)
        {
            orgsal=0;
            foreach (Cartas cart in  player.organos)
            {
                if (cart is Organos org)
                {
                    if(org.HP >= 2)
                        orgsal++;
                }
                if (orgsal == 4)
                {
                    win = true;
                    FinalizarPartida(win,lose);
                }
            }
        }
        
    }
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Acciones Player
    private bool UsarCarta(Player player,Enemy[] enemy,List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players)
    {
        WriteLine("Que carta quieres usar?\n" +
                      "(1)Bacteria\n" +
                      "(2)Cura\n" +
                      "(3)Organo\n" +
                      "(4)Especial\n" +
                      "(Enter)Volver");
        ConsoleKey input = ReadKey(true).Key;
        switch (input)
        {
            
        }
        return  true;
    }
    //Cosas que pueden ser despues quitadas
    private static void Nombrar_Carta(Player player, int i)
    {
        WriteLine($"Carta {i+1}:{player.cartasmano[i].Nombre}");
        if (player.cartasmano[i].Nombre != "Especial")
        {
            WriteLine($"| Tipo:{player.cartasmano[i].Tipo}");
        }
        if (player.cartasmano[i] is Especiales esp)
        {
            WriteLine($"| Uso:{esp.uso}");
        }
    }

    private void MostrarManoEnemiga(Enemy e)
    {
        WriteLine("Cartas del enemigo: ");
        ForegroundColor = ConsoleColor.Red;
        if (e.cartasmano.Count > 0)
        {
            if (e.cartasmano[0] is Especiales esp)
            {
                WriteLine($"{e.cartasmano[0].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{e.cartasmano[0].Nombre} | {e.cartasmano[0].Tipo}");
            }
        }
        if (e.cartasmano.Count > 1)
        {
            if (e.cartasmano[1] is Especiales esp)
            {
                WriteLine($"{e.cartasmano[1].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{e.cartasmano[1].Nombre} | {e.cartasmano[1].Tipo}");
            }
        }
        if (e.cartasmano.Count > 2)
        {
            if (e.cartasmano[2] is Especiales esp)
            {
                WriteLine($"{e.cartasmano[2].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{e.cartasmano[2].Nombre} | {e.cartasmano[2].Tipo}");
            }
        }
        else if (e.cartasmano.Count < 1)
        {
            WriteLine("No tienes cartas\n");
        }
        ForegroundColor = ConsoleColor.Gray;
    }

    private static void InputNotValid()
    {
        WriteLine("Input no valido");
        WriteLine("Pulsa enter para continuar");
        var consoleKey = Console.ReadKey(true).Key;
    }
    private bool Cuálenemigoatacar(int num, ref int id)
    {
        WriteLine("Elige a que enemigo quieres atacar");
        WriteLine($"(1)|Enemigo numero 1|");
        if (num >= 2)
        {
            WriteLine($"(2)|Enemigo numero 2|");
        }
        if (num == 3)
        {
            WriteLine($"(3)Enemigo numero 3|");
        }
        ConsoleKey input = ReadKey(true).Key;
        switch (input)
        {
            case ConsoleKey.D1:
                id = 0;
                return true;
            case ConsoleKey.D2:
                id = 1;
                return true;
            case ConsoleKey.D3:
                id = 2;
                return true;
        }
        return false;
    }
}