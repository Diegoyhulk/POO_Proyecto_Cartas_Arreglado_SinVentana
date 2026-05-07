namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseBacteria
{
    
    public bool UsarBacteria(Player player, Enemy[] enemy, List<Cartas> cartas, Mazo<Cartas> mazo, int num, int id)
    {/*
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
        }*/
        return false;
    }
}