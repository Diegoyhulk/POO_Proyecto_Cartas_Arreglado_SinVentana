using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EspecialesC
{
    ERobar rob = new ERobar();
    EDescartar desc = new EDescartar();
    ETransplante tran = new ETransplante();
    EError er = new EError();
    EContagio cont = new EContagio();
    public bool UsarEspeciales(Jugador p, Jugador e,Mazo<Cartas> mazo, List<Cartas> cartas, int id, List<Jugador> players, int num)
    {
        if (p.cartasmano[id] is Especiales esp)
        {
            if (esp.uso is Especiales.Uso.Robo)
            {
                if(rob.Robar(p,e,mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Descarte)
            {
                desc.Descartar(players, mazo, cartas, num);
                return true;
            }
            if (esp.uso is Especiales.Uso.Transplante)
            {
                if(tran.Transplantar(p, e, mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Error)
            {
                if(er.Error(p, e, mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Contagio)
            {
                if(cont.Contagiar(p, e, mazo, cartas, id)){return true;}
                return false;
            }
        }
        else
        {
        }
        return false;
        
    }

    public void Contagio(Player p, Enemy e, List<Cartas> cartas, Mazo<Cartas> mazo, int id)
    {
        cont.Contagiar(p, e, mazo, cartas, id);
    }

    public bool Robo(Player player, Enemy enemy, int organoElegido, List<Cartas> cartas, Mazo<Cartas> mazo, int indiceCartaComodin)
    {
        
        if (enemy.organos[organoElegido] is not Organos organoRobado)
            return false;
        
        int hueco = -1;
        for (int i = 0; i < player.organos.Length; i++)
        {
            if (i == organoElegido && player.organos[i] is null)
            {
                hueco = i;
                break;
            }
            else if (i == organoElegido && player.organos[i] is not null)
            {
                return false;
            }
        }

        if (hueco == -1)
            return false;

        player.organos[hueco] = organoRobado;
        enemy.organos[organoElegido] = null;

        
        cartas.Add(player.cartasmano[indiceCartaComodin]);
        player.cartasmano.RemoveAt(indiceCartaComodin);
        mazo.CogerCarta(player);
        return true;
    }

    public bool Transplante(Player player, Enemy enemy, int organoElegido, List<Cartas> cartas, Mazo<Cartas> mazo, int indiceCartaComodin)
    {
        if (enemy.organos[organoElegido] is not Organos organicambiado)
            return false;

        
        int hueco = -1;
        for (int i = 0; i < player.organos.Length; i++)
        {
            if (i == organoElegido && player.organos[i] is not null)
            {
                hueco = i;
                break;
            }
        }

        if (hueco == -1)
            return false;

        var suplente = player.organos[hueco];
        player.organos[hueco] = organicambiado;
        enemy.organos[organoElegido] = suplente;
        
        cartas.Add(player.cartasmano[indiceCartaComodin]);
        player.cartasmano.RemoveAt(indiceCartaComodin);
        mazo.CogerCarta(player);
        return true;
    }
}