namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    public static bool end;
    static void Main(string[] args)
    {
        Organos organo = new Organos();
        Mazo mazo = new Mazo();
        Player player = new Player();
        Enemy enemy = new Enemy();
        Coleccion coleccion = new Coleccion();
        Mesa mesa = new Mesa();
        EnemyAI ai = new EnemyAI();
        EspecialesC esp = new EspecialesC();
        mesa.FinalizarPartida += AcabarPartida;
        coleccion.GenerarMazo();
        mazo.Shuffle(coleccion.cartas);
        mazo.CartasIniciales(player);
        mazo.CartasIniciales(enemy);
        while (true)
        {
            mesa.Turno(ref coleccion,ref mazo,ref player,ref enemy,ref ai,ref esp);
            if (end){break;}
        }
        
    }

    private static void AcabarPartida(bool win, bool lose)
    {
        if (win)
        {
            WriteLine("Tienes todos los organos sanos y ganas!");
            end = true;
        }
        else if (lose)
        {
            WriteLine("El enemigo tiene todos los organos sanos y pierdes!");
            end = true;
        }
    }
}