using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using Raylib_cs;
using raygui_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI;

public class Printer
{
    private UseCard car = new UseCard();
    private Descartar desc =  new Descartar();
    private IInfectable iinf = new Infecta();
    private EspecialesC comando = new EspecialesC();
    private EDescartar edesc = new EDescartar();
    private EError err =  new EError();
    private Comodín com = new Comodín();
    private CuraCom curcom = new CuraCom();
    private Elegir el = new Elegir();
    private bool mostrardescarte = false;
    bool mostrarorganocomodín;
    
    private int indiceCartaSeleccionada = 0;
    private bool mostrarcuracomodin = false;
    
    private bool mostrarelegirbacteria = false;
    private int indiceBacteriaElegida = -1;

    private bool mostrarelegirenemigo = false;
    private int enemigoElegido = -1;
    
    private bool bacteriacomodin = false;
    private bool mostrarElegirOrganoComodin = false;
    private int indiceCartaComodin = -1;
    private int enemigoObjetivo = -1;
    
    private bool cartaespecial = false;
    private bool mostrarelegirespecial = false;
    private bool error;
    private bool robo;
    private bool contagio;
    private bool transplante;
    
    public void PrintearMesa(Player player, Enemy[] enemies, Dictionary<Cartas,Texture2D> texturas)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkGreen);
        
        foreach (var carta in player.organos)
            CargarTextura(carta, texturas);
        
        foreach (var carta in player.cartasmano)
            CargarTextura(carta, texturas);

        foreach (var enemi in enemies)
        foreach (var carta in enemi.organos)
            CargarTextura(carta, texturas);
        
        DibujarOrganosPlayer(player, texturas);
        DibujarOrganosEnemigos(enemies, texturas);
        PlayerCartasManoFueraTurno(player, texturas);
        
        Raylib.EndDrawing();
    }
    
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
            if (com.ElegirOrganoComodin(player, indiceCartaSeleccionada,
                    ref mostrarorganocomodín, texturas, mazo))
            {
                Raylib.EndDrawing();
                return true;
            }
        }

        if (mostrarcuracomodin)
        {
            if (curcom.ElegirCuraComodín(player, indiceCartaSeleccionada,
                    ref mostrarcuracomodin, texturas, mazo, cartas))
            {
                Raylib.EndDrawing();
                return true;
            }
        }

        if (mostrarelegirenemigo)
        {
            if (el.ElegirEnemigo(num, ref mostrarelegirenemigo, ref enemigoObjetivo))
            {
                if (bacteriacomodin)
                {
                    mostrarElegirOrganoComodin = true;
                    bacteriacomodin = false;
                    Raylib.EndDrawing();
                    return false;
                }
                if (!cartaespecial && !bacteriacomodin)
                {
                    if (iinf.Infectar(player, enemies[enemigoObjetivo], cartas, indiceCartaComodin))
                    {
                        mazo.CogerCarta(player);
                        Raylib.EndDrawing();
                        return true;
                    }
                }
                if (cartaespecial)
                {
                    if (error)
                    {
                        err.Error(player, enemies[enemigoObjetivo], mazo, cartas, indiceCartaComodin);
                        Raylib.EndDrawing();
                        return true;
                    }
                    if (contagio)
                    {
                        comando.Contagio(player, enemies[enemigoObjetivo], cartas, mazo, indiceCartaComodin);
                        Raylib.EndDrawing();
                        return true;
                    }
                    mostrarelegirespecial = true;
                    Raylib.EndDrawing();
                    return false;
                }
            }
        }


        if (mostrarElegirOrganoComodin)
        {
            int organoElegido = -1;

            if (el.ElegirOrganoEnemigo(enemies[enemigoObjetivo],
                    ref mostrarElegirOrganoComodin,
                    ref organoElegido,
                    texturas))
            {
                if (el.Infectar(player, enemies[enemigoObjetivo], cartas, indiceCartaComodin, organoElegido))
                {
                    mazo.CogerCarta(player);
                    bacteriacomodin = false;
                    Raylib.EndDrawing();
                    return true;
                }
            }
        }
        if (mostrarelegirespecial)
        {
            int organoElegido = -1;

            if (el.ElegirOrganoEnemigo(enemies[enemigoObjetivo],
                    ref mostrarelegirespecial,
                    ref organoElegido,
                    texturas))
            {
                if (robo)
                {
                    if (comando.Robo(player, enemies[enemigoObjetivo], organoElegido, cartas, mazo, indiceCartaComodin))
                    {
                        Raylib.EndDrawing();
                        return true;
                    }
                }
                else if (transplante)
                {
                    if (comando.Transplante(player, enemies[enemigoObjetivo], organoElegido, cartas, mazo,
                            indiceCartaComodin))
                    {
                        Raylib.EndDrawing();
                        return true;
                    }
                }

                cartaespecial = false;
                Raylib.EndDrawing();
                return true;
            }
        }
        Raylib.EndDrawing();
        return false;
    }

    void CargarTextura(Cartas c, Dictionary<Cartas, Texture2D> texturas)
    {
        if (c == null) return;

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

    private bool DibujarCartasPlayer(Player player, Dictionary<Cartas, Texture2D> tex,Enemy[] enemy,List<Cartas> cartas, Mazo<Cartas> mazo,int num, List<Jugador> players)
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
                        if (!mostrarelegirenemigo)
                        {
                            if (!mostrarElegirOrganoComodin)
                            {
                                if (!mostrarelegirespecial) //Ya se que esta anidado pero es que no funcona de otra forma
                                {
                                    mostrarcuracomodin = false;
                                    mostrarelegirbacteria = false;
                                    mostrarelegirenemigo = false;
                                    bacteriacomodin = false;
                                    mostrarElegirOrganoComodin = false;
                                    cartaespecial = false;
                                    mostrarelegirespecial = false;
                                    error = false;
                                    robo = false;
                                    contagio = false;
                                    transplante = false;
                                    
                                    
                                    if (CartaClicada(posX, posY, tex[carta], escala))
                                    {
                                        if (car.Cardlicked(player, enemy, cartas, mazo, num, players, i))
                                        {
                                            Raylib.EndDrawing();
                                            return true;
                                        }
                                        else if (carta is Organos && carta.Tipo == Cartas.Type.Comodín)
                                        {
                                            mostrarorganocomodín = true;
                                            indiceCartaSeleccionada = i;
                                            return false;
                                        }
                                        else if (carta is Curas && carta.Tipo == Cartas.Type.Comodín)
                                        {
                                            mostrarcuracomodin = true;
                                            indiceCartaSeleccionada = i;
                                            return false;
                                        }
                                        else if (carta.Tipo == Cartas.Type.Comodín && carta is Bacterias)
                                        {
                                            bacteriacomodin = true;
                                            indiceCartaComodin = i;


                                            if (num > 1)
                                            {
                                                mostrarelegirenemigo = true;
                                            }
                                            else
                                            {
                                                enemigoObjetivo = 0;
                                                mostrarElegirOrganoComodin = true;
                                            }

                                            return false;
                                        }

                                        else if (carta is Bacterias && carta.Tipo != Cartas.Type.Comodín)
                                        {
                                            bacteriacomodin = false;
                                            indiceCartaComodin = i;

                                            if (num > 1)
                                            {
                                                mostrarelegirenemigo = true;
                                            }
                                            else
                                            {
                                                enemigoObjetivo = 0;
                                                if (iinf.Infectar(player, enemy[0], cartas, i))
                                                {
                                                    mazo.CogerCarta(player);
                                                    return true;
                                                }
                                            }

                                            return false;
                                        }
                                        else if (carta is Especiales esp)
                                        {
                                            cartaespecial = true;
                                            indiceCartaComodin = i;
                                            switch (esp.uso)
                                            {
                                                case Especiales.Uso.Descarte:
                                                    cartaespecial = false;
                                                    edesc.Descartar(players, mazo, cartas, num);
                                                    return true;

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

                                            if (num > 1)
                                            {
                                                mostrarelegirenemigo = true;
                                            }
                                            else
                                            {
                                                enemigoObjetivo = 0;
                                                if (error || contagio)
                                                {
                                                    mostrarelegirespecial = true;
                                                }
                                                else
                                                {
                                                    mostrarElegirOrganoComodin = true;
                                                }
                                            }

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
            <4 => "Hp mayor de lo normal",
            >1 => "Org Zombie"
        };
    }
    private bool PlayerCartasManoFueraTurno(Player player, Dictionary<Cartas, Texture2D> tex)
    {
        int posX = 130;
        int posY = 1200;
        float escala = 0.3f;
        for (int i = 0; i < player.cartasmano.Count; i++)
        {
            Cartas carta = player.cartasmano[i];
            Texture2D t = tex[carta];

            if (carta == null)
            {
                continue;
            }

            CargarTextura(carta, tex);

            Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), 0f, escala, Color.White);
            posX += (int)(tex[carta].Width * escala) + 20;
        }
        return false;
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
}