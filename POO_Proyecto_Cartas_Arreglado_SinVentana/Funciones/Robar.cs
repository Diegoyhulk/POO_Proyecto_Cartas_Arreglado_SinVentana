namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using static System.Console;

public class ERobar
{
    public bool Robar(Jugador p, Jugador e, Mazo<Cartas> mazo, List<Cartas> cartas, int id)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
        if (p is Player)
        {
            Write("Que organo quieres robar?\n");
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Organos del enemigo:");
            foreach (Organos org in e.organos)
            {
                if (org != null)
                {
                    or[j] = org;
                    WriteLine($"|({j + 1}){org.Nombre} {org.Tipo} ");
                    if (org.HP == 2)
                        WriteLine($"sin ninguna bacteria|");
                    else if (org.HP < 2)
                        WriteLine($"con una bacteria|");
                    else if (org.HP == 4)
                        WriteLine($"inmunizado|");
                    else if (org.HP > 2)
                        WriteLine($"con un antibiótico|");
                }
                else if (org == null)
                {
                    n++;
                }

                if (n == 4)
                {
                    WriteLine("|El enemigo no tiene ningún organo|");
                    ReadLine();
                }

                j++;
            }

            ForegroundColor = ConsoleColor.Gray;
            WriteLine("Tus Organos:");
            j = 0;
            foreach (Organos org in p.organos)
            {
                if (org != null)
                {
                    WriteLine($"|({j + 1}){org.Nombre} {org.Tipo} ");
                    if (org.HP == 2)
                        WriteLine($"sin ninguna bacteria|");
                    else if (org.HP < 2)
                        WriteLine($"con una bacteria|");
                    else if (org.HP == 4)
                        WriteLine($"inmunizado|");
                    else if (org.HP > 2)
                        WriteLine($"con un antibiótico|");
                }

                j++;
            }

            ConsoleKey input = ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    try
                    {
                        if (p.organos[0] is null)
                        {
                            p.organos[0] = e.organos[0];
                            e.organos[0] = null;
                            cartas.Add(p.cartasmano[id]);
                            p.cartasmano.Remove(p.cartasmano[id]);
                            mazo.CogerCarta(p);
                        }
                        else if (p.organos[0] is not null)
                        {
                            WriteLine($"|Ya tienes un organo ahí|");
                            ReadLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        WriteLine($"|No hay un organo|");
                    }

                    break;
                case ConsoleKey.D2:
                    try
                    {
                        if (p.organos[1] is null)
                        {
                            p.organos[1] = e.organos[1];
                            e.organos[1] = null;
                            cartas.Add(p.cartasmano[id]);
                            p.cartasmano.Remove(p.cartasmano[id]);
                            mazo.CogerCarta(p);
                        }
                        else if (p.organos[1] is not null)
                        {
                            WriteLine($"|Ya tienes un organo ahí|");
                            ReadLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        WriteLine($"|No hay un organo|");
                    }

                    break;
                case ConsoleKey.D3:
                    try
                    {
                        if (p.organos[2] is null)
                        {
                            p.organos[2] = e.organos[2];
                            e.organos[2] = null;
                            cartas.Add(p.cartasmano[id]);
                            p.cartasmano.Remove(p.cartasmano[id]);
                            mazo.CogerCarta(p);
                        }
                        else if (p.organos[2] is not null)
                        {
                            WriteLine($"|Ya tienes un organo ahí|");
                            ReadLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        WriteLine($"|No hay un organo|");
                    }

                    break;
                case ConsoleKey.D4:
                    try
                    {
                        if (p.organos[3] is null)
                        {
                            p.organos[3] = e.organos[3];
                            e.organos[3] = null;
                            cartas.Add(p.cartasmano[id]);
                            p.cartasmano.Remove(p.cartasmano[id]);
                            mazo.CogerCarta(p);
                        }
                        else if (p.organos[3] is not null)
                        {
                            WriteLine($"|Ya tienes un organo ahí|");
                            ReadLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        WriteLine($"|No hay un organo|");
                    }

                    break;
                default:
                    WriteLine("Input no valido");
                    ReadLine();
                    return false;
            }
        }
        else if (p is Enemy)
        {
            j = 0;
            foreach (Organos org in e.organos)
            {
                if (org != null)
                {
                    or[j] = org;
                }
                else if (org == null)
                {
                    n++;
                }
                if (n == 4)
                {}
                j++;
            }
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
                        try
                        {
                            if (p.organos[0] is null)
                            {
                                p.organos[0] = e.organos[0];
                                e.organos[0] = null;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                            }
                            else if (p.organos[0] is not null)
                            {}
                        }
                        catch (NullReferenceException)
                        {}
                        input++;
                        break;
                    case 1:
                        try
                        {
                            if (p.organos[1] is null)
                            {
                                p.organos[1] = e.organos[1];
                                e.organos[1] = null;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                            }
                            else if (p.organos[1] is not null)
                            {}
                        }
                        catch (NullReferenceException)
                        {}

                        input++;
                        break;
                    case 2:
                        try
                        {
                            if (p.organos[2] is null)
                            {
                                p.organos[2] = e.organos[2];
                                e.organos[2] = null;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                            }
                            else if (p.organos[2] is not null)
                            {}
                        }
                        catch (NullReferenceException)
                        {}

                        input++;
                        break;
                    case 3:
                        try
                        {
                            if (p.organos[3] is null)
                            {
                                p.organos[3] = e.organos[3];
                                e.organos[3] = null;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                            }
                            else if (p.organos[3] is not null)
                            {}
                        }
                        catch (NullReferenceException)
                        {}
                        input++;
                        break;
                    default:
                        ReadLine();
                        return false;
                }
            }

            return false;
        }

        return false;
    }
}