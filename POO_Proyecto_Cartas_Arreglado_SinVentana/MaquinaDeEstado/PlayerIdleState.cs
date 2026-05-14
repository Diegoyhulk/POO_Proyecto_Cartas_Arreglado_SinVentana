using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class PlayerIdleState : IState
{
    private UseCard car = new UseCard();
    private IInfectable iinf = new Infecta();
    public void Enter(IState newState)
    {
        PrinterTexture.Instance.CargarTextura(GameManager.Instance.texturas, GameManager.Instance.player, GameManager.Instance.enemies);
        PrinterTexture.Instance.DibujarOrganosPlayer(GameManager.Instance.player, GameManager.Instance.texturas);
        PrinterTexture.Instance.DibujarOrganosEnemigos(GameManager.Instance.enemies, GameManager.Instance.texturas);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGreen);
            PrinterTexture.Instance.DibujarOrganosPlayer(GameManager.Instance.player, GameManager.Instance.texturas);
            PrinterTexture.Instance.DibujarOrganosEnemigos(GameManager.Instance.enemies, GameManager.Instance.texturas);
            DibujarCartasPlayer(GameManager.Instance.player,GameManager.Instance.texturas,GameManager.Instance.enemies, GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo, GameManager.Instance.num, GameManager.Instance.players);
            DibujarBotonDescarte( ()=> MaquinaEstado.Instance.ChangeState(new DescarteState()));
            Raylib.EndDrawing();
        }
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        
    }

    private void DibujarCartasPlayer(Player player, Dictionary<Cartas, Texture2D> tex, Enemy[] enemy,
        List<Cartas> cartas, Mazo<Cartas> mazo, int num, List<Jugador> players)
    {
        int posX = 930;
        int posY = 600;
        float escala = 0.7f;
        for (int i = 0; i < player.cartasmano.Count; i++)
        {
            Cartas carta = player.cartasmano[i];
            if (carta == null) continue;
            PrinterTexture.Instance.CargarTextura(tex, player, enemy);
            Texture2D t = tex[carta];

            Raylib.DrawTextureEx(t, new Vector2(posX, posY), 0f, escala, Color.White);

            if (CartaClicada(posX, posY, tex[carta], escala))
            {
                if (car.Cardlicked(player, enemy, cartas, mazo, num, players, i))
                {
                    Raylib.EndDrawing();
                    MaquinaEstado.Instance.ChangeState(new EnemyState());
                }
                else if (carta is Organos && carta.Tipo == Cartas.Type.Comodín)
                {
                    GameManager.Instance.indiceCarta = i;
                    Raylib.EndDrawing();
                    MaquinaEstado.Instance.ChangeState(new OComodinState());
                }
                else if (carta is Curas && carta.Tipo == Cartas.Type.Comodín)
                {
                    GameManager.Instance.indiceCarta = i;
                    Raylib.EndDrawing();
                    MaquinaEstado.Instance.ChangeState(new CComodinState());
                }
                else if (carta.Tipo == Cartas.Type.Comodín && carta is Bacterias)
                {
                    if (num > 1)
                    {
                        while (!Raylib.WindowShouldClose())
                        {
                            if (ElegirEnemigo(num, ref GameManager.Instance.indiceenemigo))
                            {
                                GameManager.Instance.indiceCarta = i;
                                Raylib.EndDrawing();
                                MaquinaEstado.Instance.ChangeState(new BComodinState());
                            }

                            Raylib.EndDrawing();
                        }
                    }
                    else
                    {
                        GameManager.Instance.indiceCarta = i;
                        GameManager.Instance.indiceenemigo = 0;
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new BComodinState());
                    }
                }
                else if (carta is Bacterias && carta.Tipo != Cartas.Type.Comodín)
                {
                    if (num > 1)
                    {
                        while (!Raylib.WindowShouldClose())
                        {
                            if (ElegirEnemigo(num, ref GameManager.Instance.indiceenemigo))
                            {
                                GameManager.Instance.indiceCarta = i;
                                Raylib.EndDrawing();
                                if (iinf.Infectar(player, enemy[GameManager.Instance.indiceenemigo], cartas, i))
                                {
                                    mazo.CogerCarta(player);
                                    MaquinaEstado.Instance.ChangeState(new EnemyState());
                                }
                            }

                            Raylib.EndDrawing();
                        }
                    }
                    else
                    {
                        if (iinf.Infectar(player, enemy[0], cartas, i))
                        {
                            mazo.CogerCarta(player);
                            MaquinaEstado.Instance.ChangeState(new EnemyState());
                        }
                    }
                }
                else if (carta is Especiales esp)
                {
                    GameManager.Instance.indiceCarta = i;
                    GameManager.Instance.esp = esp;
                    MaquinaEstado.Instance.ChangeState(new SpecialState());
                }
               
            }
            if (CartaHover(posX, posY, t, escala))
            {
                Raylib.DrawRectangleLines(posX - 4, posY - 4,
                    (int)(t.Width * escala) + 8,
                    (int)(t.Height * escala) + 8,
                    Color.Yellow);
            }
            posX += (int)(tex[carta].Width * escala) + 20;
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
    public bool ElegirEnemigo(int numEnemigos,ref int enemigoElegido)
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
                return true;
            }
            if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 60, 120, 40), "Atrás") != 0)
            {
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
            }
        }

        return false; 
    }

    
}