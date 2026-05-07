namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseEspecial
{
    private EspecialesC comando = new EspecialesC();
    public bool UsarCartaEspecial(Player player, Enemy[] enemy, List<Cartas> cartas, Mazo<Cartas> mazo, int num,
        List<Jugador> players, int id)
    {/*
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
            if (player.cartasmano.Count > 0 && player.cartasmano[0] is Especiales)
            {
                
            }
            if (player.cartasmano.Count > 1 && player.cartasmano[1] is Especiales)
            {
                
            }
            if (player.cartasmano.Count > 2 && player.cartasmano[2] is Especiales)
            {
                
            }
        }
        else
        {
            
            return false;
        }
        int id = 0;
        if(num > 1)
            Cuálenemigoatacar(num, ref id);
        object input;
        switch (input)
        {
             case 1 :
            {
                if(comando.UsarEspeciales(player,enemy[id],mazo,cartas,0, players)){return true;}
                return false;
            }
             case 2 :
            {
                if(comando.UsarEspeciales(player, enemy[id],mazo,cartas,1, players)){return true;}
                return false;
            }
             case 3 :
            {
                if(comando.UsarEspeciales(player,enemy[id],mazo,cartas,2, players)){return true;}
                return false;
            }
            default:
                
            break;
        }
        return false;
        return false;
    }
    private bool Cuálenemigoatacar(int num, ref int id)
    {
        if (num >= 2)
        {
        }
        if (num == 3)
        {
        }
        
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
        }*/
        return false;
    }
}