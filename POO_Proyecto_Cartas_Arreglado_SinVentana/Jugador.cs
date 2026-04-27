using System.ComponentModel;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Jugador
{
    public List<Cartas> cartasmano = new List<Cartas>();
    public Cartas[] organos = new Organos[4];
    public bool poner_organos(int i, Jugador player)
    {
        if (player is Player)
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

                        string input = ReadLine();
                        switch (input)
                        {
                            case "1":
                                organos[0] = cartasmano[i];
                                cartasmano.RemoveAt(i);
                                return true;
                            case "2":
                                organos[1] = cartasmano[i];
                                cartasmano.RemoveAt(i);
                                return true;
                            case "3":
                                organos[2] = cartasmano[i];
                                cartasmano.RemoveAt(i);
                                return true;
                            case "4":
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
        }
        else if (player is Enemy)
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
        }
        return false;
    }
    private static void Nombrar_Organo(Cartas organo, int i)
    {
        WriteLine($"Espacio {i}:{organo.Nombre}");
        WriteLine($"| tipo:{organo.Tipo}");
    }
    private static void ExistentOrgan()
    {
        WriteLine("Ya existe un organo!");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }
    private static void InputNotValid()
    {
        WriteLine("input no valido");
        WriteLine("Pulsa enter para continuar");
        ReadLine();
    }
}
public class Player : Jugador
{
    
}

public class Enemy : Jugador
{
    
}