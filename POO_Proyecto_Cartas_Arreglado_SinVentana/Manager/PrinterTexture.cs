using System.Numerics;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;

public class PrinterTexture
{
    private static PrinterTexture _instance;
    public static PrinterTexture Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PrinterTexture();

            return _instance;
        }
    }
    private PrinterTexture() { } 
    public void DibujarOrganosPlayer(Player player, Dictionary<Cartas, Texture2D> tex)
    {
        int posX = 830;
        int posY = 1150;
        float escala = 0.5f;

        foreach (var carta in player.organos)
        {
            if(carta == null){continue;}
            CargTextura(carta,tex);
            
            Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), 0f, escala, Color.White);
            
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

    public void DibujarOrganosEnemigos(Enemy[] enemies, Dictionary<Cartas, Texture2D> tex)
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
                CargTextura(carta,tex);
                Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), rotation, escala, Color.White);

                if (carta is Organos organo)
                {
                    if (i == 0)
                    {
                        string estado = PrinterTexture.Instance.GetEstadoSalud(organo.HP);

                        int textoX = posX - (int)(tex[carta].Width * escala);
                        int textoY = posY;

                        Raylib.DrawText(estado, textoX, textoY, 20, Color.White);
                        
                    }
                    else  if (i == 1)
                    {
                        string estado = PrinterTexture.Instance.GetEstadoSalud(organo.HP);

                        int textoX = posX;
                        int textoY = posY;

                        Raylib.DrawText(estado, textoX, textoY, 20, Color.White);
                    }
                    else if (i == 2)
                    {
                        string estado = PrinterTexture.Instance.GetEstadoSalud(organo.HP);

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
    public void CargarTextura(Dictionary<Cartas, Texture2D> texturas, Player player, Enemy[] enemies)
    {
        foreach (var carta in player.organos)
            CargTextura(carta, texturas);
        
        foreach (var carta in player.cartasmano)
            CargTextura(carta, texturas);

        foreach (var enemi in enemies)
        foreach (var carta in enemi.organos)
            CargTextura(carta, texturas);
    }

    public void CargTextura(Cartas c, Dictionary<Cartas, Texture2D> texturas)
    {
        if (c == null) return;

        if (!texturas.ContainsKey(c))
            texturas[c] = Raylib.LoadTexture(c.Cara);
    }
    public string GetEstadoSalud(int hp)
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
}