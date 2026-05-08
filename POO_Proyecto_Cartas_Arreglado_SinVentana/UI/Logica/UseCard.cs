using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseCard
{
    UseCura cura = new UseCura();
    UseOrgano org = new UseOrgano();
    
    public bool Cardlicked(Player player,Enemy[] enemy,List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players, int id)
    {
        switch (player.cartasmano[id])
        {
            case Curas:
                if(cura.UsarCura(player, cartas, mazo,id)){return true;}
                Console.WriteLine("Carta Cura");
                break;
            case Organos:
                if(org.UsarOrgano(player, mazo,id)){return true;}
                Console.WriteLine("Carta Organo");
                break;
        }
        return false;
    }
}