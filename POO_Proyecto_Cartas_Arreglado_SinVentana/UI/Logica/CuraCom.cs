using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class CuraCom
{
    public bool ElegirCuraComodín(Player player, int indiceCarta, ref bool mostrarUI, Dictionary<Cartas, Texture2D> texturas, Mazo<Cartas> mazo, List<Cartas> cartas)
{
    // Fondo semitransparente
    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(),
                         Raylib.ColorAlpha(Color.Black, 0.5f));

    int w = 600;
    int h = 300;
    int x = (Raylib.GetScreenWidth() - w) / 2;
    int y = (Raylib.GetScreenHeight() - h) / 2;

    // Ventana
    Raylib.DrawRectangle(x, y, w, h, Color.DarkGray);
    Raylib.DrawRectangleLines(x, y, w, h, Color.White);

    Raylib.DrawText("Elige que organo curar", x + 20, y + 20, 28, Color.White);

    // Coordenadas de los 4 espacios
    int espacioX = x + 40;
    int espacioY = y + 100;
    int espacioW = 120;
    int espacioH = 160;

    Cartas carta = player.cartasmano[indiceCarta];
    Texture2D tex = texturas[carta];

    for (int slot = 0; slot < 4; slot++)
    {
        Rectangle rect = new Rectangle(espacioX + slot * (espacioW + 20), espacioY, espacioW, espacioH);

        bool ocupado = player.organos[slot] != null;
        bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);

        
        Color fondo = ocupado ? Color.DarkGreen : Color.Red;
        if (hover) fondo = ocupado ? Color.Green : Color.Maroon;

        Raylib.DrawRectangleRec(rect, fondo);
        Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, espacioW, espacioH, Color.White);

        
        Raylib.DrawText($"Espacio {slot + 1}", (int)rect.X + 10, (int)rect.Y + 10, 20, Color.White);

        
        if (!ocupado)
        {
            Raylib.DrawText("Sin Organo",
                (int)rect.X + 10,
                (int)rect.Y + 40,
                18,
                Color.White);
        } 
        if (ocupado)
        {
            Raylib.DrawText(player.organos[slot].Nombre +"\n"+ player.organos[slot].Tipo,
                (int)rect.X + 10,
                (int)rect.Y + 40,
                18,
                Color.White);
        }

        
        if (hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            if (!ocupado)
            {
                // Mostrar mensaje de error
                Raylib.DrawText("No hay organo ahí",
                    x + 20, y + h - 40, 24, Color.Yellow);
            }
            else if (player.organos[slot] is Organos or)
            {
                or.HP++;
                if (or.HP == 4)
                {
                    or.inmunizado = true;
                }
                cartas.Add(player.cartasmano[indiceCarta]);
                player.cartasmano.RemoveAt(indiceCarta);
                mazo.CogerCarta(player);
                mostrarUI = false;
                return true;
            }
        }
    }

    // Botón cancelar
    if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 50, 120, 40), "Cancelar") != 0)
    {
        mostrarUI = false;
        return false;
    }

    return false;
}
}