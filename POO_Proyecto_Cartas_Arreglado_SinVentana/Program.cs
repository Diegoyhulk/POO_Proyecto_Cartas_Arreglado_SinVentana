using POO_Proyecto_Cartas_Arreglado_SinVentana.UI;
using Raylib_cs;
using raygui_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    public static bool end;
    static int num;
    static void Main(string[] args)
    {
        Dictionary<Cartas, Texture2D> texturas = new Dictionary<Cartas, Texture2D>();
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
        while (true){
            if (Elegirenemigos()){break;}
        }
        int i = 1;
        foreach (Enemy enemy1 in enemy )
        {
            mazo.CartasIniciales(enemy1);
            if(num == i){break;}
            i++;
        }
        Raylib.InitWindow(2560, 1500, "Pantalla");
        Raylib.SetTargetFPS(60);
        

        while (true)
        {
            mesa.Turno(ref coleccion,ref mazo,ref player,ref enemy,ref ai,ref esp, num, players, texturas);
            if (end){break;}
        }
        foreach (var tex in texturas.Values)
            Raylib.UnloadTexture(tex);

        Raylib.CloseWindow();
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
            Raylib.InitWindow(800, 600, "Selecciona enemigos");
            Raylib.SetTargetFPS(60);


            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGreen);

                Raylib.DrawText("¿Cuántos enemigos quieres en la partida?", 80, 80, 30, Color.White);

                // Botón 1 enemigo
                if (Raygui.GuiButton(new Rectangle(250, 200, 300, 60), "1 Enemigo") != 0)
                {
                    Raylib.CloseWindow();
                    num = 1;
                    return true;
                }

                // Botón 2 enemigos
                if (Raygui.GuiButton(new Rectangle(250, 300, 300, 60), "2 Enemigos") != 0)
                {
                    num = 2;
                    Raylib.CloseWindow();
                    return true;
                }

                // Botón 3 enemigos
                if (Raygui.GuiButton(new Rectangle(250, 400, 300, 60), "3 Enemigos") != 0)
                {
                    num = 3;
                    Raylib.CloseWindow();
                    return true;
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
            return false;
    }
}