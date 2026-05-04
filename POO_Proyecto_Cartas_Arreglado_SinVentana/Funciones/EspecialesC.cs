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
    public bool UsarEspeciales(Jugador p, Jugador e,Mazo<Cartas> mazo, List<Cartas> cartas, int id, List<Jugador> players)
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
                desc.Descartar(players, mazo, cartas);
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
            WriteLine("Algo anda mal");
            ReadLine();
        }
        return false;
        
    }
}