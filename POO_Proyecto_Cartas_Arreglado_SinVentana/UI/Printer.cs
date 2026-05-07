using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using Raylib_cs;
using raygui_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI;

public class Printer
{
    private UseCard car = new UseCard();
    private Descartar desc =  new Descartar();
    private bool mostrardescarte = false;
    bool mostrarorganocomodín;
    private int indiceCartaSeleccionada = 0;
    private bool mostrarcuracomodin = false;

    public bool TurnoPlayer(Player player,Enemy[] enemies,
        List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players, Dictionary<Cartas, Texture2D> texturas)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkGreen);

        // Cargar texturas dinámicamente
        foreach (var carta in player.organos)
            CargarTextura(carta, texturas);
        
        foreach (var carta in player.cartasmano)
            CargarTextura(carta, texturas);

        foreach (var enemi in enemies)
        foreach (var carta in enemi.organos)
            CargarTextura(carta, texturas);

        // Dibujar
        DibujarOrganosPlayer(player, texturas);
        DibujarOrganosEnemigos(enemies, texturas);

        if (DibujarCartasPlayer(player, texturas, enemies, cartas, mazo, num, players)){Raylib.EndDrawing();return true;}
        DibujarBotonDescarte(() => mostrardescarte = true);

        if (mostrardescarte)
            if (desc.Descarte(cartas, mazo, player, ref mostrardescarte, texturas)){Raylib.EndDrawing();return true;}
        
        if (mostrarorganocomodín)
        {
            if (ElegirOrganoComodin((Player)player, indiceCartaSeleccionada,
                    ref mostrarorganocomodín, texturas, mazo))
            {
                Raylib.EndDrawing();
                return true;
            }
        }
        if (mostrarcuracomodin)
        {
            if (ElegirCuraComodín((Player)player, indiceCartaSeleccionada,
                    ref mostrarcuracomodin, texturas, mazo, cartas))
            {
                Raylib.EndDrawing();
                return true;
            }
        }
        
        Raylib.EndDrawing();
        return false;
    }

    void CargarTextura(Cartas c, Dictionary<Cartas, Texture2D> texturas)
    {
        if (c == null) return; // ← EVITA EL CRASH

        if (!texturas.ContainsKey(c))
            texturas[c] = Raylib.LoadTexture(c.Cara);
    }

    public void DibujarBotonDescarte(Action accion)
    {
        int ancho = 200;
        int alto = 60;

        int posX = Raylib.GetScreenWidth() - ancho - 30;
        int posY = Raylib.GetScreenHeight() - alto - 30;

        Rectangle rect = new Rectangle(posX, posY, ancho, alto);

        bool hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);

        // Color según hover
        Color fondo = hover ? Color.SkyBlue : Color.DarkBlue;

        // Dibujar botón
        Raylib.DrawRectangleRec(rect, fondo);
        Raylib.DrawRectangleLines(posX, posY, ancho, alto, Color.White);

        Raylib.DrawText("DESCARTE",
            posX + 20,
            posY + 18,
            28,
            Color.White);

        // Clic
        if (hover && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            accion();
        }
    }

    private bool DibujarCartasPlayer(Player player, Dictionary<Cartas, Texture2D> tex,Enemy[] enemy,
        List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players)
    {
        
        int posX = 930;
        int posY = 600;
        float escala = 0.7f;
        for (int i = 0; i < player.cartasmano.Count; i++)
        {
            Cartas carta = player.cartasmano[i];
            Texture2D t = tex[carta];
            
            if(carta == null){continue;}
            CargarTextura(carta,tex);
            
            Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), 0f, escala, Color.White);
            if (!mostrardescarte)
            {
                if (!mostrarcuracomodin)
                {
                    if (!mostrarorganocomodín)
                    {
                        if (CartaClicada(posX, posY, tex[carta], escala))
                        {
                            if (car.Cardlicked(player, enemy, cartas, mazo, num, players, i))
                            {
                                Raylib.EndDrawing();
                                return true;
                            }

                            if (carta is Organos && carta.Tipo == Cartas.Type.Comodín)
                            {
                                mostrarorganocomodín = true;
                                indiceCartaSeleccionada = i;
                                return false;
                            }

                            if (carta is Curas && carta.Tipo == Cartas.Type.Comodín)
                            {
                                mostrarcuracomodin = true;
                                indiceCartaSeleccionada = i;
                                return false;
                            }
                        }

                        if (CartaHover(posX, posY, tex[carta], escala))
                        {
                            Raylib.DrawRectangleLines(posX - 4, posY - 4,
                                (int)(tex[carta].Width * escala) + 8,
                                (int)(tex[carta].Height * escala) + 8,
                                Color.Yellow);
                        }
                    }
                }
            }

            posX += (int)(tex[carta].Width * escala) + 20;
        }
        return false;
    }
    private void DibujarOrganosPlayer(Player player, Dictionary<Cartas, Texture2D> tex)
    {
       
        int posX = 830;
        int posY = 1150;
        float escala = 0.5f;

        foreach (var carta in player.organos)
        {
            if(carta == null){continue;}
            CargarTextura(carta,tex);
            
            // Dibujar carta escalada
            Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), 0f, escala, Color.White);

            // Si es un órgano, dibujar texto debajo
            if (carta is Organos organo)
            {
                string estado = GetEstadoSalud(organo.HP);

                int textoX = posX;
                int textoY = posY + (int)(tex[carta].Height * escala) + 10;
                Raylib.DrawText(estado, textoX, textoY, 24, Color.White);
                
            }

            posX += (int)(tex[carta].Width * escala) + 40;
        }
    }

    private string GetEstadoSalud(int hp)
    {
        return hp switch
        {
            1 => "Tiene un virus",
            2 => "Está saludable",
            3 => "Tiene un antibiótico",
            4 => "Está inmunizado",
            _ => "Estado desconocido"
        };
    }

    private void DibujarOrganosEnemigos(Enemy[] enemies, Dictionary<Cartas, Texture2D> tex)
    {
        int posY = 300;
        float rotation = 180;
        int posX = 980;
        int i = 0;

        foreach (var enemy in enemies)
        {
            float escala = 0.5f;

            foreach (Cartas carta in enemy.organos)
            {
                if(carta is null){continue;}
                CargarTextura(carta,tex);
                Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), rotation, escala, Color.White);

                if (carta is Organos organo)
                {
                    if (i == 0)
                    {
                        string estado = GetEstadoSalud(organo.HP);

                        int textoX = posX - (int)(tex[carta].Width * escala);
                        int textoY = posY;

                        Raylib.DrawText(estado, textoX, textoY, 20, Color.White);
                        
                    }
                    else  if (i == 1)
                    {
                        string estado = GetEstadoSalud(organo.HP);

                        int textoX = posX;
                        int textoY = posY;

                        Raylib.DrawText(estado, textoX, textoY, 20, Color.White);
                    }
                    else if (i == 2)
                    {
                        string estado = GetEstadoSalud(organo.HP);

                        int textoX = posX - (int)(tex[carta].Width * escala * 1.5f);
                        int textoY = posY + (int)(tex[carta].Width * escala);

                        Raylib.DrawText(estado, textoX, textoY, 20, Color.White);
                    }
                }

                if (i == 0)
                {
                    posX += (int)(tex[carta].Width * escala) + 20;
                }
                else if (i >= 1)
                {
                    posY += (int)(tex[carta].Width * escala) + 20;
                }
            }

            if (i == 0)
            {
                posX = 1880;
                posY = 450;
                rotation += 90;
            }
            else if (i == 1)
            {
                posY = 330;
                posX = 730;
                rotation += 180;
            }

            i++;
        }
    }
    private bool CartaClicada(int x, int y, Texture2D tex, float escala)
    {
        int ancho = (int)(tex.Width * escala);
        int alto  = (int)(tex.Height * escala);
        Rectangle hitbox = new Rectangle(x, y, ancho, alto);
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox)
               && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }
    private bool CartaHover(int x, int y, Texture2D tex, float escala)
    {
        int ancho = (int)(tex.Width * escala);
        int alto  = (int)(tex.Height * escala);

        Rectangle hitbox = new Rectangle(x, y, ancho, alto);

        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), hitbox);
    }
    public bool ElegirOrganoComodin(Player player, int indiceCarta,
                                ref bool mostrarUI,
                                Dictionary<Cartas, Texture2D> texturas, Mazo<Cartas> mazo)
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

    Raylib.DrawText("Elige dónde colocar el órgano", x + 20, y + 20, 28, Color.White);

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

        
        Color fondo = ocupado ? Color.Red : Color.DarkGreen;
        if (hover) fondo = ocupado ? Color.Maroon : Color.Green;

        Raylib.DrawRectangleRec(rect, fondo);
        Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, espacioW, espacioH, Color.White);

        
        Raylib.DrawText($"Espacio {slot + 1}", (int)rect.X + 10, (int)rect.Y + 10, 20, Color.White);

        
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
            if (ocupado)
            {
                // Mostrar mensaje de error
                Raylib.DrawText("Ese espacio ya está ocupado",
                    x + 20, y + h - 40, 24, Color.Yellow);
            }
            else
            {
                // Colocar órgano
                player.organos[slot] = carta;
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
