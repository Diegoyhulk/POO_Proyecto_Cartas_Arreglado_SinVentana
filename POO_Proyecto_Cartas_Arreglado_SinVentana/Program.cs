using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI;
using Raylib_cs;
using raygui_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
class Program
{
    private static bool resultadoWin;
    private static bool resultadoLose;
    static bool endScreen = false;
    static int num;
    static void Main(string[] args)
    {
        Dictionary<Cartas, Texture2D> texturas = new Dictionary<Cartas, Texture2D>();
        Mazo<Cartas> mazo = new Mazo<Cartas>();
        Player player = new Player();
        Enemy[] enemy = new Enemy[] {new Enemy(), new Enemy(), new Enemy()};
        Coleccion coleccion = new Coleccion();
        EnemyAI ai = new EnemyAI();
        EspecialesC esp = new EspecialesC();
        List<Jugador> players = new List<Jugador>();
        players.Add(player);
        while (true){
            if (Elegirenemigos()){break;}
        }
        for (int j = 0; j < num; j++)
        {
            players.Add(enemy[j]);
        }
        coleccion.GenerarMazo();
        mazo.Shuffle(coleccion.cartas);
        mazo.CartasIniciales(player);
        int i = 1;
        foreach (Enemy enemy1 in enemy )
        {
            mazo.CartasIniciales(enemy1);
            if(num == i){break;}
            i++;
        }
        GameManager.Instance.player = player;
        GameManager.Instance.mazo = mazo;
        GameManager.Instance.num = num;
        GameManager.Instance.texturas = texturas;
        GameManager.Instance.coleccion = coleccion;
        GameManager.Instance.enemies = enemy;
        GameManager.Instance.ai = ai;
        GameManager.Instance.comando = esp;
        GameManager.Instance.players = players;
        Raylib.InitWindow(2560, 1500, "Pantalla");
        Raylib.SetTargetFPS(60);
        MaquinaEstado.Instance.ChangeState(new TurnoPlayer());
        foreach (var tex in texturas.Values)
            Raylib.UnloadTexture(tex);

        Raylib.CloseWindow();
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