using System.Drawing;
using POO_Proyecto_Cartas_Arreglado_SinVentana.ASSCCI;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Mesa
{
    private bool win;
    private bool lose;
    private Program program;
    private int orgsal = 0;
    private int eorgsal = 0;
    private IInfectable iinf = new Infecta();
    private ICurable icur = new Cura();
    private EnemyAI eai = new EnemyAI();
    private Printer print = new Printer();
    public event Action<bool,bool> FinalizarPartida;
    public int turnos { get; set; }
    public void Turno(ref Coleccion coleccion,ref Mazo<Cartas> mazo,ref Player player,ref Enemy[] enemy,ref EnemyAI ai,ref EspecialesC comando, int num, List<Jugador> players)
    {
        ComprobarOrganosSaludables(player,enemy,ref win,ref lose, num);
        Clear();
        if (mazo.coleccion.Count == 0)
        {
            mazo.Shuffle(coleccion.cartas);
        }
        //Turno
        while (true)
        {
            ForegroundColor = ConsoleColor.Red;
            Write("///////////////////////////////ORGANOS DEL ENEMIGO////////////////////////////\n");
            ForegroundColor = ConsoleColor.Gray;
            if (num >= 1){print.PrintOrganos(enemy[0].organos);}

            if (num >= 2)
            { 
                ForegroundColor = ConsoleColor.Red;
                Write("///////////////////////////////ORGANOS DEL ENEMIGO2///////////////////////////\n");
                ForegroundColor = ConsoleColor.Gray;
                print.PrintOrganos(enemy[1].organos);
            }

            if (num == 3)
            {
                ForegroundColor = ConsoleColor.Red;
                Write("///////////////////////////////ORGANOS DEL ENEMIGO3///////////////////////////\n");
                ForegroundColor = ConsoleColor.Gray;
                print.PrintOrganos(enemy[2].organos);
            }
            WriteLine($"{mazo.CantidadMazo} cartas");
            Write($"Turno: {turnos}\n");
            Write("///////////////////////////////TUS CARTAS////////////////////////////////////\n");
            print.PrintCartasMano(player.cartasmano);
            Write("///////////////////////////////TUS ORGANOS///////////////////////////////////\n");
            print.PrintOrganos(player.organos);
            Write("////////////////////////////QUE QUIERES HACER?///////////////////////////////\n");
            Write($"(1)Descartar carta\n" +
             $"(2)Usar carta\n");
            ConsoleKey input = ReadKey(true).Key;
            if (input == ConsoleKey.D1 &&  player.cartasmano.Count > 0){if (Descarte(coleccion.cartas ,mazo, player)) break;}
            else if (input == ConsoleKey.D2){if(UsarCarta(player,enemy, coleccion.cartas, mazo, comando, num, players)) break;}
            else {InputNotValid();}
        }
        //Turno Enemigo
        ForegroundColor = ConsoleColor.Red;
        if (num >= 1){ai.ETurno(enemy,mazo,coleccion.cartas,player,comando, players,0, num);}
        if (num >= 2){ai.ETurno(enemy,mazo,coleccion.cartas,player,comando, players,1, num);}
        if (num == 3){ai.ETurno(enemy,mazo,coleccion.cartas,player,comando, players,2, num);}
        ForegroundColor = ConsoleColor.Gray;
        //Acaba el turno
        turnos++;
    }
    private void ComprobarOrganosSaludables(Player player,Enemy[] enemy,ref bool win, ref bool lose, int num)
    {
        orgsal = 0;
        eorgsal = 0;
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
        foreach (Cartas cart in  enemy[0].organos)
        {
            if (cart is Organos org)
            {
                if(org.HP >= 2)
                    eorgsal += 1;
            }
            if (eorgsal == 4)
            {
                lose = true;
                FinalizarPartida(win,lose);
            }
        }
        if(num == 1){return;}
        eorgsal = 0;
        foreach (Cartas cart in  enemy[2].organos)
        {
            if (cart is Organos org)
            {
                if(org.HP >= 2)
                    eorgsal += 1;
            }
            if (eorgsal == 4)
            {
                lose = true;
                FinalizarPartida(win,lose);
            }
        }
        if (num == 2){return;}
        eorgsal = 0;
        foreach (Cartas cart in  enemy[1].organos)
        {
            if (cart is Organos org)
            {
                if(org.HP >= 2)
                    eorgsal += 1;
            }
            if (eorgsal == 4)
            {
                lose = true;
                FinalizarPartida(win,lose);
            }
        }
    }
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Acciones Player
    private bool UsarCarta(Player player,Enemy[] enemy,List<Cartas> cartas, Mazo<Cartas> mazo, EspecialesC comando,int num, List<Jugador> players)
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
            case ConsoleKey.D1:
                if(UsarBacteria(player,enemy, cartas,mazo,num)){return true;}
                return false;
            case ConsoleKey.D2:
                if(UsarCura(player,cartas,mazo)){return true;}
                return false;
            case ConsoleKey.D3:
                if(UsarOrgano(player,mazo)){return true;}
                return false;
            case ConsoleKey.D4:
                if (UsarCartaEspecial(player, enemy, cartas, mazo,comando, num, players)){return true;}
                return false;
            default:
                InputNotValid();
                return false;
        }
    }

    private bool UsarCartaEspecial(Player player, Enemy[] enemy, List<Cartas> cartas, Mazo<Cartas> mazo, EspecialesC comando, int num, List<Jugador> players)
    {
        bool hascard = false;
        foreach (var c in player.cartasmano)
        {
            if (c is Especiales)
            {
                hascard = true;
                break;
            }
        }
        if (hascard)
        {
            WriteLine("Que Especial quieres usar?");
            if (player.cartasmano.Count > 0 && player.cartasmano[0] is Especiales)
            {
                WriteLine($"(1):"); Nombrar_Carta(player, 0);
            }
            if (player.cartasmano.Count > 1 && player.cartasmano[1] is Especiales)
            {
                WriteLine($"(2):"); Nombrar_Carta(player, 1);
            }
            if (player.cartasmano.Count > 2 && player.cartasmano[2] is Especiales)
            {
                WriteLine($"(3):"); Nombrar_Carta(player, 2);
            }
            WriteLine("(Enter)Atras");
        }
        else
        {
            WriteLine("No tienes cartas Especiales!");
            WriteLine("Pulsa enter para continuar");
            ReadLine();
            return false;
        }
        ConsoleKey input = ReadKey().Key;
        int id = 0;
        if(num > 1)
            Cuálenemigoatacar(num, ref id);
        switch (input)
        {
            case ConsoleKey.D1 when player.cartasmano[0] is Especiales:
            {
                if(comando.UsarEspeciales(player,enemy[id],mazo,cartas,0, players)){return true;}
                return false;
            }
            case ConsoleKey.D2 when player.cartasmano[1] is Especiales:
            {
                if(comando.UsarEspeciales(player, enemy[id],mazo,cartas,1, players)){return true;}
                return false;
            }
            case ConsoleKey.D3 when player.cartasmano[2] is Especiales:
            {
                if(comando.UsarEspeciales(player,enemy[id],mazo,cartas,2, players)){return true;}
                return false;
            }
            default:
            WriteLine("input no valido");
            WriteLine("Pulsa enter para continuar");
            ReadLine();
            break;
        }
        return false;
        return false;
    }

    private bool UsarBacteria(Player player,Enemy[] enemy, List<Cartas> cartas, Mazo<Cartas> mazo, int num)
    {
        bool hascard = false;
        foreach (var c in player.cartasmano)
        {
            if (c is Bacterias)
            {
                hascard = true;
                break;
            }
        }
        if (hascard)
        {
            WriteLine("Que bacteria quieres usar?");
            if (player.cartasmano.Count > 0 && player.cartasmano[0] is Bacterias)
            {
                WriteLine($"(1):"); Nombrar_Carta(player, 0);
            }
            if (player.cartasmano.Count > 1 && player.cartasmano[1] is Bacterias)
            {
                WriteLine($"(2):"); Nombrar_Carta(player, 1);
            }
            if (player.cartasmano.Count > 2 && player.cartasmano[2] is Bacterias)
            {
                WriteLine($"(3):"); Nombrar_Carta(player, 2);
            }
            WriteLine("(Enter)Atras");
        }
        else
        {
            WriteLine("No tienes bacterias!");
            WriteLine("Pulsa enter para continuar");
            ReadLine();
            return false;
        }
        ConsoleKey input = ReadKey().Key;
        int id = 0;
        if(num > 1)
            Cuálenemigoatacar(num, ref id);
        switch (input)
        {
            case ConsoleKey.D1 when player.cartasmano[0] is Bacterias:
            {
                if (iinf.Infectar(player, enemy[id], cartas, 0)){mazo.CogerCarta(player);return true;}
                break;
            }
            case ConsoleKey.D2 when player.cartasmano[1] is Bacterias:
            {
                if (iinf.Infectar(player, enemy[id], cartas, 1)){mazo.CogerCarta(player);return true;}
                break;
            }
            case ConsoleKey.D3 when player.cartasmano[2] is Bacterias:
            {
                if(iinf.Infectar(player,enemy[id], cartas, 2)){mazo.CogerCarta(player);return true;}
                break;
            }
            default:
                    WriteLine("input no valido");
                    WriteLine("Pulsa enter para continuar");
                    ReadLine();
                    break;
        }
        return false;
    }
    private bool UsarCura(Player player, List<Cartas> cartas,Mazo<Cartas> mazo)
    {
        bool hascard = false;
        foreach (var c in player.cartasmano)
        {
            if (c is Curas)
            {
                hascard = true;
                break;
            }
        }
        if (hascard)
        {
            WriteLine("Que Cura quieres usar?");
            if (player.cartasmano.Count > 0 && player.cartasmano[0] is Curas)
            {
                WriteLine($"(1):"); Nombrar_Carta(player, 0);
            }
            if (player.cartasmano.Count > 1 && player.cartasmano[1] is Curas)
            {
                WriteLine($"(2):"); Nombrar_Carta(player, 1);
            }
            if (player.cartasmano.Count > 2 && player.cartasmano[2] is Curas)
            {
                WriteLine($"(3):"); Nombrar_Carta(player, 2);
            }
            WriteLine("(Enter)Atras");
        }
        else
        {
            WriteLine("No tienes bacterias!");
            WriteLine("Pulsa enter para continuar");
            ReadLine();
            return false;
        }

        ConsoleKey input = ReadKey(true).Key;
        switch (input)
        {
            case ConsoleKey.D1 when player.cartasmano[0] is Curas:
            {
                if(icur.Curar(player, cartas, 0)){mazo.CogerCarta(player);return true;}
                break;
            }
            case ConsoleKey.D2 when player.cartasmano[1] is Curas:
            {
                if(icur.Curar(player, cartas, 1)){mazo.CogerCarta(player);return true;}
                break;
            }
            case ConsoleKey.D3 when player.cartasmano[2] is Curas:
            {
                if(icur.Curar(player, cartas, 2)){mazo.CogerCarta(player);return true;}
                break;
            }
            default:
                    WriteLine("input no valido");
                    WriteLine("Pulsa enter para continuar");
                    ReadLine();
                    break;
        }
        return false;
    }
    private bool UsarOrgano(Player player,Mazo<Cartas> mazo)
    {
        bool hascard = false;
        foreach (var c in player.cartasmano)
        {
            if (c is Organos)
            {
                hascard = true;
                break;
            }
        }
        if (hascard)
        {
            WriteLine("Que organo quieres usar?");
            if (player.cartasmano.Count > 0 && player.cartasmano[0] is Organos)
            {
                WriteLine($"(1):"); Nombrar_Carta(player, 0);
            }
            if (player.cartasmano.Count > 1 && player.cartasmano[1] is Organos)
            {
                WriteLine($"(2):"); Nombrar_Carta(player, 1);
            }
            if (player.cartasmano.Count > 2 && player.cartasmano[2] is Organos)
            {
                WriteLine($"(3):"); Nombrar_Carta(player, 2);
            }
            WriteLine("(Enter)Atras");
        }
        else
        {
            WriteLine("No tienes organos!");
            WriteLine("Pulsa enter para continuar");
            ReadLine();
            return false;
        }
        ConsoleKey input = ReadKey(true).Key;
        switch (input)
        {
            case ConsoleKey.D1 when player.cartasmano[0] is Organos:
            {
                if( player.poner_organos(0,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            case ConsoleKey.D2 when player.cartasmano[1] is Organos:
            {
                if( player.poner_organos(1,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            case ConsoleKey.D3 when player.cartasmano[2] is Organos:
            {
                if( player.poner_organos(2,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            default:
                InputNotValid();
                return false;
        }
    }
    private static bool Descarte(List<Cartas> cartas ,Mazo<Cartas> mazo, Player player)
    {
        WriteLine("Que carta quieres descartar?");
        if (player.cartasmano.Count > 0){Nombrar_Carta(player, 0);}
        if (player.cartasmano.Count > 1){Nombrar_Carta(player, 1);}
        if (player.cartasmano.Count > 2){Nombrar_Carta(player, 2);}
        WriteLine("(Enter)Atras");
        var input1 = ReadKey(true).Key;
        if (input1 == ConsoleKey.D1)
        {
            mazo.DescartarCarta(cartas,player, 0);
            mazo.CogerCarta(player);
            return true;
        }
        if (input1 == ConsoleKey.D2 && player.cartasmano.Count > 1)
        {
            mazo.DescartarCarta(cartas,player, 1);
            mazo.CogerCarta(player);
            return true;
        }

        if (input1 == ConsoleKey.D3 && player.cartasmano.Count > 2)
        {
            mazo.DescartarCarta(cartas, player, 2);
            mazo.CogerCarta(player);
            return true;
        }
        else
        {InputNotValid();}

        return false;
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