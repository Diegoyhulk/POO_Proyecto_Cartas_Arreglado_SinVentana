using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class EndState: IState
{
    public void Enter(IState newState)
    {
        AcabarPartida(GameManager.Instance.winstate);
    }
    public static void AcabarPartida(bool win)
    {
        while (!Raylib.WindowShouldClose())
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

            Rectangle boton = new Rectangle(x + w / 2 - 100, y + h - 80, 200, 50);
            bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), boton);

            Raylib.DrawRectangleRec(boton, hover ? Color.SkyBlue : Color.DarkBlue);
            Raylib.DrawRectangleLines((int)boton.X, (int)boton.Y, (int)boton.Width, (int)boton.Height, Color.White);
            Raylib.DrawText("SALIR", (int)boton.X + 55, (int)boton.Y + 10, 30, Color.White);

            if (hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Raylib.EndDrawing();
                Raylib.CloseWindow();
            }
            Raylib.EndDrawing();
        }
    }
    public void Update()
    {
    }

    public void Exit()
    {
    }
}