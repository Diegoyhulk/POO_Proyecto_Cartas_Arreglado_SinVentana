using System.ComponentModel;
using POO_Proyecto_Cartas_Arreglado_SinVentana.UI.Logica;
using raygui_cs;
using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Jugador
{
    public List<Cartas> cartasmano = new List<Cartas>();
    public Cartas[] organos = new Organos[4];
}
public class Player : Jugador
{
    public bool poner_organos(int i)
    {
        if (cartasmano[i] is Organos organo)
        {
            switch (organo.Tipo)
            {
                case Cartas.Type.Sanguíneo:
                {
                    if (organos[0] == null)
                    {
                        organos[0] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Ósseo:
                {
                    if (organos[1] == null)
                    {
                        organos[1] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Neuronal:
                {
                    if (organos[2] == null)
                    {
                        organos[2] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Gástrico:
                {
                    if (organos[3] == null)
                    {
                        organos[3] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return false;
        }

        return false;
    }
}

public class Enemy : Jugador
{
   public bool poner_organos(int i, Jugador player)
    {
        if (cartasmano[i] is Organos organo)
        {
            switch (organo.Tipo)
            {
                case Cartas.Type.Sanguíneo:
                {
                    if (organos[0] is null)
                    {
                        organos[0] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Ósseo:
                {
                    if (organos[1] is null)
                    {
                        organos[1] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Neuronal:
                {
                    if (organos[2] is null)
                    {
                        organos[2] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Gástrico:
                {
                    if (organos[3] is null)
                    {
                        organos[3] = cartasmano[i];
                        cartasmano.RemoveAt(i);
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
                case Cartas.Type.Comodín:
                {
                    int input = 0;
                    while (true)
                    {
                        if (input == 4)
                        {
                            return false;
                        }
                        switch (input)
                        {
                            case 0:
                                if (organos[0] is null)
                                {
                                    organos[0] = cartasmano[i];
                                    cartasmano.RemoveAt(i);
                                    return true;
                                }
                                input++;
                                break;
                            case 1:
                                if (organos[1] is null)
                                {
                                    organos[1] = cartasmano[i];
                                    cartasmano.RemoveAt(i);
                                    return true;
                                }
                                input++;
                                break;
                            case 2:
                                if (organos[2] is null)
                                {
                                    organos[2] = cartasmano[i];
                                    cartasmano.RemoveAt(i);
                                    return true;
                                }
                                input++;
                                break;
                            case 3:
                                if (organos[3] is null)
                                {
                                    organos[3] = cartasmano[i];
                                    cartasmano.RemoveAt(i);
                                    return true;
                                }
                                input++;
                                break;
                        }
                    }
                }
            }

            return false;
        }
        return false;
    } 
}