namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    private static bool lose = false;
    private static bool win = false;
    static void Main(string[] args)
    {
        Organos org = new Organos();
        Mazo mazo = new Mazo();
        Player player = new Player();
        Enemy enemy = new Enemy();
        Coleccion coleccion = new Coleccion();
        Mesa mesa = new Mesa();
        EnemyAI ai = new EnemyAI();
        coleccion.GenerarMazo();
        mazo.Shuffle(coleccion.cartas);
        mazo.CartasIniciales(player);
        mazo.CartasIniciales(enemy);
        while (true)
        {
            mesa.Turno(ref coleccion,ref mazo,ref player,ref enemy,ref ai, ref win, ref lose);
            if(win){break;}
            else if(lose){break;}
        }
        if (win)
        {
            WriteLine("Tienes todos los organos sanos y ganas!");
        }
        else if (lose)
        {
            WriteLine("El enemigo todos los organos sanos y pierdes!");
        }
        
    }
}