using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class UseCard
{
    UseBacteria bact = new UseBacteria();
    UseCura cura = new UseCura();
    UseEspecial esp = new UseEspecial();
    UseOrgano org = new UseOrgano();
    
    public bool Cardlicked(Player player,Enemy[] enemy,List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players, int id)
    {
        switch (player.cartasmano[id])
        {
            case Bacterias:
                if(bact.UsarBacteria(player, enemy, cartas, mazo, num,id)){return true;}
                Console.WriteLine("Carta Bacteria");
                break;
            case Curas:
                if(cura.UsarCura(player, cartas, mazo,id)){return true;}
                Console.WriteLine("Carta Cura");
                break;
            case Organos:
                if(org.UsarOrgano(player, mazo,id)){return true;}
                Console.WriteLine("Carta Organo");
                break;
            case Especiales:
                if(esp.UsarCartaEspecial(player, enemy, cartas, mazo, num, players,id)){return true;}
                Console.WriteLine("Carta Especial");
                break;
            
        }
        return false;
    }
}