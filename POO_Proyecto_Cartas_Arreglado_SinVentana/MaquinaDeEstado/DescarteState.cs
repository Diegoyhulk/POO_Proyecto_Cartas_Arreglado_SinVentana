using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class DescarteState: IState
{
    public void Enter(IState newState)
    {
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGreen);

            void CargarTextura(Cartas c)
            {
                if (!GameManager.Instance.texturas.ContainsKey(c))
                    GameManager.Instance.texturas[c] = Raylib.LoadTexture(c.Cara);
            }

            foreach (var carta in GameManager.Instance.player.cartasmano)
                CargarTextura(carta);

            int w = 700;
            int h = 350;
            int x = (Raylib.GetScreenWidth() - w) / 2;
            int y = (Raylib.GetScreenHeight() - h) / 2;
            int posX = x + 40;
            int posY = y + 100;
            float escala = 0.35f;
            float escalaHover = escala * 1.15f;

            Raylib.DrawRectangle(x, y, w, h, Color.DarkGray);
            Raylib.DrawRectangleLines(x, y, w, h, Color.White);

            Raylib.DrawText("Selecciona una carta para descartar", x + 20, y + 20, 28, Color.White);

            for (int i = 0; i < GameManager.Instance.player.cartasmano.Count; i++)
            {
                Cartas carta = GameManager.Instance.player.cartasmano[i];
                Texture2D tex = GameManager.Instance.texturas[carta];

                bool hover = CartaHover(posX, posY, tex, escala);

                if (hover)
                {
                    int offsetX = (int)(tex.Width * (escalaHover - escala) / 2);
                    int offsetY = (int)(tex.Height * (escalaHover - escala) / 2);

                    Raylib.DrawTextureEx(tex, new Vector2(posX - offsetX, posY - offsetY), 0f, escalaHover,
                        Color.White);

                    Raylib.DrawRectangleLines(
                        posX - offsetX - 4,
                        posY - offsetY - 4,
                        (int)(tex.Width * escalaHover) + 8,
                        (int)(tex.Height * escalaHover) + 8,
                        Color.Yellow
                    );

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        GameManager.Instance.mazo.DescartarCarta(GameManager.Instance.coleccion.cartas, GameManager.Instance.player, i);
                        GameManager.Instance.mazo.CogerCarta(GameManager.Instance.player);
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new EnemyState());
                    }
                }
                else
                {
                    Raylib.DrawTextureEx(tex, new Vector2(posX, posY), 0f, escala, Color.White);
                }

                posX += (int)(tex.Width * escala) + 40;
            }

            //Boton atras
            if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 60, 120, 40), "Atrás") != 0)
            {
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
            }

            bool CartaHover(int x, int y, Texture2D tex, float escala)
            {
                int ancho = (int)(tex.Width * escala);
                int alto = (int)(tex.Height * escala);

                Rectangle hitbox = new Rectangle(x, y, ancho, alto);

                return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox);
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