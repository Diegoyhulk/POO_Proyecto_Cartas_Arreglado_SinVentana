using System.Numerics;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class Elegir
{
    public bool ElegirEnemigo(int numEnemigos,ref bool mostrarUI,ref int enemigoElegido)
    {
        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(),
            Raylib.ColorAlpha(Color.Black, 0.5f));

        int w = 500;
        int h = 250;
        int x = (Raylib.GetScreenWidth() - w) / 2;
        int y = (Raylib.GetScreenHeight() - h) / 2;

        Raylib.DrawRectangle(x, y, w, h, Color.DarkGray);
        Raylib.DrawRectangleLines(x, y, w, h, Color.White);

        Raylib.DrawText("¿A qué enemigo elegir?", x + 20, y + 20, 28, Color.White);

        int posX = x + 40;
        int posY = y + 100;

        for (int i = 0; i < numEnemigos; i++)
        {
            Rectangle rect = new Rectangle(posX + i * 150, posY, 120, 60);

            bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);

            Raylib.DrawRectangleRec(rect, hover ? Color.SkyBlue : Color.DarkBlue);
            Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, 120, 60, Color.White);

            Raylib.DrawText($"Enemigo {i + 1}", (int)rect.X + 10, (int)rect.Y + 20, 20, Color.White);

            if (hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                enemigoElegido = i;
                mostrarUI = false;
                return true;
            }
        }

        return false;
    }
    
    public bool Infectar(Player player, Enemy enemy, List<Cartas> cartas, int indiceCarta, int organoObjetivo)
    {
        Cartas bacteria = player.cartasmano[indiceCarta];

        if (enemy.organos[organoObjetivo] is Organos org)
        {
            if (org.inmunizado) return false;

            org.HP--;

            if (org.HP == 0)
            {
                cartas.Add(org);
                enemy.organos[organoObjetivo] = null;
                org.HP = 2;
            }

            cartas.Add(bacteria);
            player.cartasmano.RemoveAt(indiceCarta);
            return true;
        }

        return false;
    }

    public bool ElegirOrganoEnemigo(Enemy enemigo,
        ref bool mostrarUI,
        ref int organoElegido,
        Dictionary<Cartas, Texture2D> tex)
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

        for (int i = 0; i < enemigo.organos.Length; i++)
        {
            Cartas carta = enemigo.organos[i];
            if (carta == null) continue;

            Texture2D t = tex[carta];

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
                    organoElegido = i;
                    mostrarUI = false;
                    return true;
                }
            }

            posX += (int)(t.Width * escala) + 40;
        }

        if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 50, 120, 40), "Cancelar") != 0)
        {
            mostrarUI = false;
            return false;
        }

        return false;
    }
}