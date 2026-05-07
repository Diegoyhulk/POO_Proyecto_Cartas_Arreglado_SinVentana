using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseOrgano
{
    public bool UsarOrgano(Player player, Mazo<Cartas> mazo, int id)
    {
        if( player.poner_organos(id) ){mazo.CogerCarta(player);return true;}
        return false;
    }
}