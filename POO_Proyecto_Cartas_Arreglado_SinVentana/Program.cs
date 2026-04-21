namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    private static bool win;
    static void Main(string[] args)
    {
        Organos org = new Organos();
        Mazo mazo = new Mazo();
        Player player = new Player();
        Enemy enemy = new Enemy();
        Coleccion coleccion = new Coleccion();
        Mesa mesa = new Mesa();
        coleccion.GenerarMazo();
        mazo.Shuffle(coleccion.cartas);
        mazo.CartasIniciales(player);
        mazo.CartasIniciales(enemy);
        org.EliminarOrgano += player.Eliminar_Organo;
        while (true)
        {
            mesa.Turno(ref coleccion,ref mazo,ref player,ref enemy, ref win);
            if(win){break;}
        }
        org.EliminarOrgano -= player.Eliminar_Organo;
        WriteLine("Tienes todos los organos sanos y ganas!");
    }
}