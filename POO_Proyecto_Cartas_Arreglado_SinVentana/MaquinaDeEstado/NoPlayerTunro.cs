using System.Numerics;
using POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.MaquinaDeEstado;

public class NoPlayerTunro: IState
{
    public void Enter(IState newState)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.DarkGreen);
        PrinterTexture.Instance.CargarTextura(GameManager.Instance.texturas, GameManager.Instance.player, GameManager.Instance.enemies);
        PrinterTexture.Instance.DibujarOrganosPlayer(GameManager.Instance.player, GameManager.Instance.texturas);
        PrinterTexture.Instance.DibujarOrganosEnemigos(GameManager.Instance.enemies, GameManager.Instance.texturas);
        PlayerCartasManoFueraTurno(GameManager.Instance.player, GameManager.Instance.texturas);
        
        Raylib.EndDrawing();
    }
    public void Update()
    {
    }
    public void Exit()
    {
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

            PrinterTexture.Instance.CargTextura(carta, tex);

            Raylib.DrawTextureEx(tex[carta], new Vector2(posX, posY), 0f, escala, Color.White);
            posX += (int)(tex[carta].Width * escala) + 20;
        }
        return false;
    }
}