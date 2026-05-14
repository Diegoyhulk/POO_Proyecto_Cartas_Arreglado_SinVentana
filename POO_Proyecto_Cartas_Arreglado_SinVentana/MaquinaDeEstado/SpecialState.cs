using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class SpecialState: IState
{
    
    private EDescartar edesc = new EDescartar();
    private EError err  = new EError();
    public void Enter(IState newState)
    {
        bool error = false;
        bool robo = false;
        bool transplante = false;
        bool contagio = false;
        switch (GameManager.Instance.esp.uso)
        {
            case Especiales.Uso.Descarte:
                edesc.Descartar(GameManager.Instance.players, GameManager.Instance.mazo, GameManager.Instance.coleccion.cartas, GameManager.Instance.num);
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new EnemyState());
                break;
            case Especiales.Uso.Error:
                error = true;
                break;
            case Especiales.Uso.Robo:
                robo = true;
                break;
            case Especiales.Uso.Transplante:
                transplante = true;
                break;
            case Especiales.Uso.Contagio:
                contagio = true;
                break;
        }

        if (GameManager.Instance.num > 1)
        {
            while (!Raylib.WindowShouldClose())
            {
                if (ElegirEnemigo(GameManager.Instance.num, ref GameManager.Instance.indiceenemigo))
                {
                    if (error)
                    {
                        if (err.Error(GameManager.Instance.player,
                                GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                                GameManager.Instance.mazo, GameManager.Instance.coleccion.cartas,
                                GameManager.Instance.indiceCarta))
                        {
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new EnemyState());
                        }
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
                    }
                    else if (contagio)
                    {
                        GameManager.Instance.comando.Contagio(GameManager.Instance.player,
                            GameManager.Instance.enemies[GameManager.Instance.indiceenemigo], GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo,
                            GameManager.Instance.indiceCarta);
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new EnemyState());
                    }
                    else if (robo)
                    {
                        while (!Raylib.WindowShouldClose())
                        {
                            int organoElegido = 0;
                            if (ElegirOrganoEnemigo(
                                    GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                                    ref organoElegido,
                                    GameManager.Instance.texturas))
                            {
                                if (GameManager.Instance.comando.Robo(GameManager.Instance.player,
                                        GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                                        organoElegido, GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo, GameManager.Instance.indiceCarta))
                                {
                                    Raylib.EndDrawing();
                                    MaquinaEstado.Instance.ChangeState(new EnemyState());
                                }
                            }
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
                        }
                    }
                    else if (transplante)
                    {
                        while (!Raylib.WindowShouldClose())
                        {
                            int organoElegido = 0;
                            if (ElegirOrganoEnemigo(
                                    GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                                    ref organoElegido,
                                    GameManager.Instance.texturas))
                            {
                                if (GameManager.Instance.comando.Transplante(GameManager.Instance.player,
                                        GameManager.Instance.enemies
                                            [GameManager.Instance.indiceenemigo],
                                        organoElegido, GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo,
                                        GameManager.Instance.indiceCarta))
                                {
                                    Raylib.EndDrawing();
                                    MaquinaEstado.Instance.ChangeState(new EnemyState());
                                }
                            }
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
                        }
                    }

                    Raylib.EndDrawing();
                }
                Raylib.EndDrawing();
            }
        }
        else
        {
            if (error)
            {
                err.Error(GameManager.Instance.player, GameManager.Instance.enemies[0], GameManager.Instance.mazo,
                    GameManager.Instance.coleccion.cartas,
                    GameManager.Instance.indiceCarta);
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new EnemyState());
            }
            else if (contagio)
            {
                GameManager.Instance.comando.Contagio(GameManager.Instance.player, GameManager.Instance.enemies[0], GameManager.Instance.coleccion.cartas,
                    GameManager.Instance.mazo, GameManager.Instance.indiceCarta);
                Raylib.EndDrawing();
                MaquinaEstado.Instance.ChangeState(new EnemyState());
            }
            else if (robo)
            {
                while (!Raylib.WindowShouldClose())
                {
                    int organoElegido = 0;
                    if (ElegirOrganoEnemigo(
                            GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                            ref organoElegido,
                            GameManager.Instance.texturas))
                    {
                        if (GameManager.Instance.comando.Robo(GameManager.Instance.player, GameManager.Instance.enemies[0],
                                organoElegido, GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo, GameManager.Instance.indiceCarta))
                        {
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new EnemyState());
                        }
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
                    }
                    Raylib.EndDrawing();
                    
                }
            }
            else if (transplante)
            {
                while (!Raylib.WindowShouldClose())
                {
                    int organoElegido = 0;
                    if (ElegirOrganoEnemigo(
                            GameManager.Instance.enemies[GameManager.Instance.indiceenemigo],
                            ref organoElegido,
                            GameManager.Instance.texturas))
                    {
                        if (GameManager.Instance.comando.Transplante(GameManager.Instance.player,
                                GameManager.Instance.enemies[0],
                                organoElegido, GameManager.Instance.coleccion.cartas, GameManager.Instance.mazo,
                                GameManager.Instance.indiceCarta))
                        {
                            Raylib.EndDrawing();
                            MaquinaEstado.Instance.ChangeState(new EnemyState());
                        }
                        Raylib.EndDrawing();
                        MaquinaEstado.Instance.ChangeState(new PlayerIdleState());
                    }
                    Raylib.EndDrawing();
                }
            }
        }
    }
    private bool ElegirOrganoEnemigo(Enemy enemigo,
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
                    return true;
                }
            }

            posX += (int)(t.Width * escala) + 40;
        }

        if (Raygui.GuiButton(new Rectangle(x + w - 140, y + h - 50, 120, 40), "Cancelar") != 0)
        {
            return false;
        }

        return false;
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

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}