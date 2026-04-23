namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Mesa
{
    int orgsal = 0;
    public IInfectable iinf = new Organos();
    private EnemyAI eai = new EnemyAI();
    public int turnos { get; set; }
    public void Turno(ref Coleccion coleccion,ref Mazo mazo,ref Player player,ref Enemy enemy, ref bool win)
    {
        orgsal = 0;
        if (mazo.coleccion.Count == 0)
        {
            mazo.Shuffle(coleccion.cartas);
        }
        //Turno
        while (true)
        {
            mazo.LLamarCartas();
            WriteLine($"{mazo.CantidadMazo} cartas");
            Write($"Turno: {turnos}\n");
            MostrarOrganos(player);
            MostrarMano(player);
            Write($"(1)CogerCarta\n" +
             $"(2)Descartar carta\n" +
             $"(3)Usar carta");
            string input = ReadLine();
            if(input == "1"){if(mazo.CogerCarta(player))break;}
            if (input == "2" &&  player.cartasmano.Count > 0){if (Descarte(coleccion.cartas ,mazo, player)) break;}
            if (input == "3")
            {
                if(UsarCarta(player, coleccion.cartas)) break;
            }
            else{InputNotValid();}
        }
        foreach (var cart in  player.organos)
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
        //ETurno(enemy, mazo);
        
        //Turno Enemigo
        //eai.ETurno(enemy, mazo);
        //Acaba el turno
        turnos++;
    }
    //Acciones Player
    private bool UsarCarta(Player player,List<Cartas> cartas)
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
                if(UsarBacteria(player, cartas )){return true;}
                return false;
            case "2":
                if(UsarCura(player, cartas)){return true;}
                return false;
            case "3":
                if(UsarOrgano(player)){return true;}
                return false;
            default:
                InputNotValid();
                return false;
        }
    }
    private bool UsarBacteria(Player player, List<Cartas> cartas)
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
                if(iinf.Infectar(player, cartas, 0)){return true;}
                break;
            }
            case "2" when player.cartasmano[1] is Bacterias:
            {
                if(iinf.Infectar(player, cartas, 1)){return true;}
                break;
            }
            case "3" when player.cartasmano[2] is Bacterias:
            {
                if(iinf.Infectar(player, cartas, 2)){return true;}
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
    private bool UsarCura(Player player, List<Cartas> cartas)
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
            WriteLine("Que bacteria quieres usar?");
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
                if(iinf.Curar(player, cartas, 0)){return true;}
                break;
            }
            case "2" when player.cartasmano[1] is Curas:
            {
                if(iinf.Curar(player, cartas, 1)){return true;}
                break;
            }
            case "3" when player.cartasmano[2] is Curas:
            {
                if(iinf.Curar(player, cartas, 2)){return true;}
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
    private bool UsarOrgano(Player player)
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
                if( player.poner_organos(0) ){return true;}
                return false;
            }
            case "2" when player.cartasmano[1] is Organos:
            {
                if( player.poner_organos(1) ){return true;}
                return false;
            }
            case "3" when player.cartasmano[2] is Organos:
            {
                if( player.poner_organos(2) ){return true;}
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
            return true;
        }
        if (input1 == "2" && player.cartasmano.Count > 1)
        {
            mazo.DescartarCarta(cartas,player, 1);
            return true;
        }

        if (input1 == "3" && player.cartasmano.Count > 2)
        {
            mazo.DescartarCarta(cartas, player, 2);
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
            WriteLine($"| tipo:{player.cartasmano[i].Type}");
        }
        if (player.cartasmano[i] is Especiales esp)
        {
            WriteLine($"| uso:{esp.Uso}");
        }
    }

    private void MostrarMano(Player player)
    {
        WriteLine("Cartas: ");
        if (player.cartasmano.Count > 0)
        {
            if (player.cartasmano[0] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[0].Nombre} | {esp.Uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[0].Nombre} | {player.cartasmano[0].Type}");
            }
        }
        if (player.cartasmano.Count > 1)
        {
            if (player.cartasmano[1] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[1].Nombre} | {esp.Uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[1].Nombre} | {player.cartasmano[1].Type}");
            }
        }
        if (player.cartasmano.Count > 2)
        {
            if (player.cartasmano[2] is Especiales esp)
            {
                WriteLine($"{player.cartasmano[2].Nombre} | {esp.Uso}");
            }
            else
            {
                WriteLine($"{player.cartasmano[2].Nombre} | {player.cartasmano[2].Type}");
            }
        }
        else
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
                WriteLine($"|{org.Nombre} {org.Type} esta malo");
            }
            if (org.HP == 2)
            {
                WriteLine($"|{org.Nombre} {org.Type} esta sano");
            }
            if (org.HP == 3)
            {
                WriteLine($"|{org.Nombre} {org.Type} esta sano con un antibiótico");
            }
            if (org.HP == 4)
            {
                WriteLine($"|{org.Nombre} {org.Type} esta inmunizado!");
            }
        }
    }

    private static void InputNotValid()
    {
        WriteLine("input no valido");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }
}