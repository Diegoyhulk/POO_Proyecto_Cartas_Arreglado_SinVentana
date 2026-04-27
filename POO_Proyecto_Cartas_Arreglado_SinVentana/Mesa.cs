using System.Drawing;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Mesa
{
    private int orgsal = 0;
    private int eorgsal = 0;
    public IInfectable iinf = new Organos();
    private EnemyAI eai = new EnemyAI();
    public int turnos { get; set; }
    public void Turno(ref Coleccion coleccion,ref Mazo mazo,ref Player player,ref Enemy enemy,ref EnemyAI ai, ref bool win, ref bool lose)
    {
        
        if (mazo.coleccion.Count == 0)
        {
            mazo.Shuffle(coleccion.cartas);
        }
        //Turno
        while (true)
        {
            mazo.LLamarCartas();
            MostrarManoEnemiga(enemy);
            MostrarOrganosEnemigo(enemy);
            WriteLine($"{mazo.CantidadMazo} cartas");
            Write($"Turno: {turnos}\n");
            MostrarOrganos(player);
            MostrarMano(player);
            Write($"(1)Descartar carta\n" +
             $"(2)Usar carta");
            string input = ReadLine();
            if (input == "1" &&  player.cartasmano.Count > 0){if (Descarte(coleccion.cartas ,mazo, player)) break;}
            if (input == "2")
            {
                if(UsarCarta(player,enemy, coleccion.cartas, mazo)) break;
            }
            else{InputNotValid();}
        }
        //Turno Enemigo
        ForegroundColor = ConsoleColor.Red;
        ai.ETurno(enemy,mazo,coleccion.cartas,player);
        ForegroundColor = ConsoleColor.Gray;
        //Acaba el turno
        ComprobarOrganosSaludables(player,enemy,ref win,ref lose);
        turnos++;
    }

    private void ComprobarOrganosSaludables(Player player,Enemy enemy,ref bool win, ref bool lose)
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
            }
        }
        foreach (Cartas cart in  enemy.organos)
        {
            if (cart is Organos org)
            {
                if(org.HP >= 2)
                    eorgsal += 1;
            }
            if (eorgsal == 4)
            {
                lose = true;
            }
        }
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Acciones Player
    private bool UsarCarta(Player player,Enemy enemy,List<Cartas> cartas, Mazo mazo)
    {
        WriteLine("Que carta quieres usar?\n" +
                      "(1)Bacteria\n" +
                      "(2)Cura\n" +
                      "(3)Organo\n" +
                      "(Enter)Volver");
        string input = ReadLine();
        switch (input)
        {
            case "1":
                if(UsarBacteria(player,enemy, cartas,mazo )){return true;}
                return false;
            case "2":
                if(UsarCura(player,enemy, cartas,mazo)){return true;}
                return false;
            case "3":
                if(UsarOrgano(player,mazo)){return true;}
                return false;
            default:
                InputNotValid();
                return false;
        }
    }
    private bool UsarBacteria(Player player,Enemy enemy, List<Cartas> cartas, Mazo mazo)
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
        string input = ReadLine();
        switch (input)
        {
            case "1" when player.cartasmano[0] is Bacterias:
            {
                if (iinf.Infectar(player, enemy, cartas, 0)){mazo.CogerCarta(player);return true;}
                break;
            }
            case "2" when player.cartasmano[1] is Bacterias:
            {
                if (iinf.Infectar(player, enemy, cartas, 1)){mazo.CogerCarta(player);return true;}
                break;
            }
            case "3" when player.cartasmano[2] is Bacterias:
            {
                if(iinf.Infectar(player,enemy, cartas, 2)){mazo.CogerCarta(player);return true;}
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
    private bool UsarCura(Player player,Enemy enemy, List<Cartas> cartas,Mazo mazo)
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
        string input = ReadLine();
        switch (input)
        {
            case "1" when player.cartasmano[0] is Curas:
            {
                if(iinf.Curar(player, cartas, 0)){mazo.CogerCarta(player);return true;}
                break;
            }
            case "2" when player.cartasmano[1] is Curas:
            {
                if(iinf.Curar(player, cartas, 1)){mazo.CogerCarta(player);return true;}
                break;
            }
            case "3" when player.cartasmano[2] is Curas:
            {
                if(iinf.Curar(player, cartas, 2)){mazo.CogerCarta(player);return true;}
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
    private bool UsarOrgano(Player player,Mazo mazo)
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
        string input = ReadLine();
        switch (input)
        {
            case "1" when player.cartasmano[0] is Organos:
            {
                if( player.poner_organos(0,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            case "2" when player.cartasmano[1] is Organos:
            {
                if( player.poner_organos(1,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            case "3" when player.cartasmano[2] is Organos:
            {
                if( player.poner_organos(2,player) ){mazo.CogerCarta(player);return true;}
                return false;
            }
            default:
                InputNotValid();
                return false;
        }
    }
    private static bool Descarte(List<Cartas> cartas ,Mazo mazo, Player player)
    {
        WriteLine("Que carta quieres descartar?");
        if (player.cartasmano.Count > 0){Nombrar_Carta(player, 0);}
        if (player.cartasmano.Count > 1){Nombrar_Carta(player, 1);}
        if (player.cartasmano.Count > 2){Nombrar_Carta(player, 2);}
        WriteLine("(Enter)Atras");
        var input1 = ReadLine();
        if (input1 == "1")
        {
            mazo.DescartarCarta(cartas,player, 0);
            mazo.CogerCarta(player);
            return true;
        }
        if (input1 == "2" && player.cartasmano.Count > 1)
        {
            mazo.DescartarCarta(cartas,player, 1);
            mazo.CogerCarta(player);
            return true;
        }

        if (input1 == "3" && player.cartasmano.Count > 2)
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
            WriteLine($"| tipo:{player.cartasmano[i].Tipo}");
        }
        if (player.cartasmano[i] is Especiales esp)
        {
            WriteLine($"| uso:{esp.uso}");
        }
    }

    private void MostrarMano(Player player)
    {
        WriteLine("Cartas: ");
        if (player.cartasmano.Count > 0)
        {
            if (player.cartasmano[0] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[0].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[0].Nombre} | {player.cartasmano[0].Tipo}");
            }
        }
        if (player.cartasmano.Count > 1)
        {
            if (player.cartasmano[1] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[1].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[1].Nombre} | {player.cartasmano[1].Tipo}");
            }
        }
        if (player.cartasmano.Count > 2)
        {
            if (player.cartasmano[2] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[2].Nombre} | {esp.uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[2].Nombre} | {player.cartasmano[2].Tipo}");
            }
        }
        else if(player.cartasmano.Count < 1)
        {
            WriteLine("No tienes cartas\n");
        }
    }

    private void MostrarOrganos(Player player)
    {
        foreach (Organos org in player.organos)
        {
            if (org == null){continue;}
            if (org.HP < 2)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta malo");
            }
            if (org.HP == 2)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta sano");
            }
            if (org.HP == 3)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta sano con un antibiótico");
            }
            if (org.HP == 4)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta inmunizado!");
            }
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
    private void MostrarOrganosEnemigo(Enemy e)
    {
        ForegroundColor = ConsoleColor.Red;
        foreach (Organos org in e.organos)
        {
            if (org == null){continue;}
            if (org.HP < 2)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta malo");
            }
            if (org.HP == 2)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta sano");
            }
            if (org.HP == 3)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta sano con un antibiótico");
            }
            if (org.HP == 4)
            {
                WriteLine($"|{org.Nombre} {org.Tipo} esta inmunizado!");
            }
        }
        ForegroundColor = ConsoleColor.Gray;
    }

    private static void InputNotValid()
    {
        WriteLine("input no valido");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }
}