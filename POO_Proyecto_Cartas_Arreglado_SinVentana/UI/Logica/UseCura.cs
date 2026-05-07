using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseCura
{
    ICurable icur = new Cura();
    public bool UsarCura(Player player, List<Cartas> cartas, Mazo<Cartas> mazo, int id)
    {
        if(icur.Curar(player, cartas, id)){mazo.CogerCarta(player);return true;}
        return false;
    }
}