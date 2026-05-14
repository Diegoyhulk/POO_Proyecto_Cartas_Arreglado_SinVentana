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
        TurnoPlayer turnoPlayer = new TurnoPlayer();
        EnemyAI ai = new EnemyAI();
        EspecialesC esp = new EspecialesC();
        List<Jugador> players = new List<Jugador>();
        EnemyState estate = new EnemyState();
        players.Add(player);
        while (true){
            if (Elegirenemigos()){break;}
        }
        for (int j = 0; j < num; j++)
        {
            players.Add(enemy[j]);
        }
        estate.FinalizarPartida += AcabarPartida;
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
    public static void AcabarPartida(bool win, bool lose)
    {
        endScreen = true;
        resultadoWin = win;
        resultadoLose = lose;
    }
    public static bool DibujarFinPartida(bool win, bool lose)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkGreen);
        string mensaje = win
            ? "¡Tienes todos los órganos sanos!\n\n¡HAS GANADO!"
            : "El enemigo tiene todos los órganos sanos.\n\nHAS PERDIDO";

        Color color = win ? Color.Green : Color.Red;

        // Fondo semitransparente
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(),
            Raylib.ColorAlpha(Color.Black, 0.7f));

        int w = 900;
        int h = 400;
        int x = (Raylib.GetScreenWidth() - w) / 2;
        int y = (Raylib.GetScreenHeight() - h) / 2;

        Raylib.DrawRectangle(x, y, w, h, Color.DarkGray);
        Raylib.DrawRectangleLines(x, y, w, h, Color.White);

        Raylib.DrawText(mensaje, x + 40, y + 80, 40, color);

        Rectangle boton = new Rectangle(x + w/2 - 100, y + h - 80, 200, 50);
        bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), boton);

        Raylib.DrawRectangleRec(boton, hover ? Color.SkyBlue : Color.DarkBlue);
        Raylib.DrawRectangleLines((int)boton.X, (int)boton.Y, (int)boton.Width, (int)boton.Height, Color.White);
        Raylib.DrawText("SALIR", (int)boton.X + 55, (int)boton.Y + 10, 30, Color.White);

        if (hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Raylib.EndDrawing();
            return true;
        }
        
        Raylib.EndDrawing();
        return false;
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