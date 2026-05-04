namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    public static bool end;
    static int num;
    static void Main(string[] args)
    {
        Mazo<Cartas> mazo = new Mazo<Cartas>();
        Player player = new Player();
        Enemy[] enemy = new Enemy[] {new Enemy(), new Enemy(), new Enemy()};
        Coleccion coleccion = new Coleccion();
        Mesa mesa = new Mesa();
        EnemyAI ai = new EnemyAI();
        EspecialesC esp = new EspecialesC();
        List<Jugador> players = new List<Jugador>() {player, enemy[0], enemy[1], enemy[2]};
        mesa.FinalizarPartida += AcabarPartida;
        coleccion.GenerarMazo();
        mazo.Shuffle(coleccion.cartas);
        mazo.CartasIniciales(player);
        foreach (Enemy enemy1 in enemy)
        {
            mazo.CartasIniciales(enemy1);
        }
        Clear();
        while (true){
            if (Elegirenemigos()){break;}
        }
        while (true)
        {
            mesa.Turno(ref coleccion,ref mazo,ref player,ref enemy,ref ai,ref esp, num, players);
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

    private static bool Elegirenemigos()
    {
        WriteLine("Cuántos enemigos quieres en la parida?\n" +
                  "(1) 1 Enemigo\n" +
                  "(2) 2 Enemigos\n" +
                  "(3) 3 Enemigos\n" );
        ConsoleKey input = ReadKey(true).Key;
        switch (input)
        {
            case ConsoleKey.D1:
                num = 1;
                return true;
            case ConsoleKey.D2:
                num = 2;
                return true;
            case ConsoleKey.D3:
                num = 3;
                return true;
        }
        return false;
    }
}