using System.ComponentModel;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Jugador
{
    public List<Cartas> cartasmano = new List<Cartas>();
    public Cartas[] organos = new Organos[4];

    protected static void Nombrar_Organo(Cartas organo, int i)
    {
        WriteLine($"Espacio {i}:{organo.Nombre}");
        WriteLine($"| tipo:{organo.Tipo}");
    }

    protected static void ExistentOrgan()
    {
        WriteLine("Ya existe un organo!");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }

    protected static void InputNotValid()
    {
        WriteLine("input no valido");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }
}
public class Player : Jugador
{
    public bool poner_organos(int i, Jugador player)
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
                        ExistentOrgan();
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
                        ExistentOrgan();
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
                        ExistentOrgan();
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
                        ExistentOrgan();
                        break;
                    }
                }
                case Cartas.Type.Comodín:
                {
                    int j = 1;
                    WriteLine("Elige que organo quieres poner");
                    foreach (var org in organos)
                    {
                        if (org == null)
                        {
                            WriteLine($"Espacio {j} libre");
                        }

                        if (org != null)
                        {
                            Nombrar_Organo(org, j);
                        }

                        j++;
                    }

                    ConsoleKey input = ReadKey(true).Key;
                    switch (input)
                    {
                        case ConsoleKey.D1:
                            organos[0] = cartasmano[i];
                            cartasmano.RemoveAt(i);
                            return true;
                        case ConsoleKey.D2:
                            organos[1] = cartasmano[i];
                            cartasmano.RemoveAt(i);
                            return true;
                        case ConsoleKey.D3:
                            organos[2] = cartasmano[i];
                            cartasmano.RemoveAt(i);
                            return true;
                        case ConsoleKey.D4:
                            organos[3] = cartasmano[i];
                            cartasmano.RemoveAt(i);
                            return true;
                        default:
                            InputNotValid();
                            break;
                    }

                    break;
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