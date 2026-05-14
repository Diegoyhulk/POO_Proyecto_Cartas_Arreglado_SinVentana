using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class BComodinState: IState
{
    private int organoelegido;
    public void Enter(IState newState)
    {
        while (!Raylib.WindowShouldClose())
        {
            
            Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(),
                Raylib.ColorAlpha(Color.Black, 0.5f));

            int w = 800;
            int h = 400;
            int x = (Raylib.GetScreenWidth() - w) / 2;
            int y = (Raylib.GetScreenHeight() - h) / 2;

            Raylib.DrawRectangle(x, y, w, h, Color.DarkGray);
            Raylib.DrawRectangleLines(x, y, w, h, Color.White);

            Raylib.DrawText("Elige un órgano del enemigo",
                x + 20, y + 20, 28, Color.White);

            int posX = x + 40;
            int posY = y + 120;
            float escala = 0.4f;

            for (int i = 0; i < GameManager.Instance.enemies[GameManager.Instance.indiceenemigo].organos.Length; i++)
            {
                Cartas carta = GameManager.Instance.enemies[GameManager.Instance.indiceenemigo].organos[i];
                if (carta == null) continue;

                Texture2D t = GameManager.Instance.texturas[carta];

                Rectangle rect = new Rectangle(posX, posY,
                    t.Width * escala,
                    t.Height * escala);

                bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);

                Raylib.DrawTextureEx(t, new Vector2(posX, posY), 0f, escala, Color.White);

                if (hover)
                {
                    Raylib.DrawRectangleLines(posX - 4, posY - 4,
                        (int)(t.Width * escala) + 8,
                        (int)(t.Height * escala) + 8,
                        Color.Yellow);

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        organoelegido = i;
                        if (Infectar(GameManager.Instance.player,GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],GameManager.Instance.coleccion.cartas,GameManager.Instance.indiceCarta))
                        {
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new EnemyState());
                        }
                    }
                }

                posX += (int)(t.Width * escala) + 40;
            }

            if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 50, 120, 40), "Cancelar") != 0)
            {
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
            }
            Raylib.EndDrawing();
        }
    }
    public bool Infectar(Player player, Enemy enemy, List<Cartas> cartas, int indiceCarta)
    {
        Cartas bacteria = player.cartasmano[indiceCarta];

        if (enemy.organos[organoelegido] is Organos org)
        {
            if (org.inmunizado) return false;

            org.HP--;

            if (org.HP == 0)
            {
                cartas.Add(org);
                enemy.organos[organoelegido] = null;
                org.HP = 2;
            }
            cartas.Add(bacteria);
            player.cartasmano.RemoveAt(indiceCarta);
            GameManager.Instance.mazo.CogerCarta(player);
            return true;
        }

        return false;
    }
    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}