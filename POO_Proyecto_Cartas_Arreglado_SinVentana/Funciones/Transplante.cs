namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
public class ETransplante
{
    public bool Transplantar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas,int id)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
        Write("Que organo quieres transplantar?\n");
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
        Cartas suplente = new Organos();
        string input = ReadLine();
        switch (input)
        {
            case "1":
                if(or[0] is not null)
                {
                    if (p.organos[0] is not null)
                    {
                        suplente = e.organos[0];
                        e.organos[0] = p.organos[0];
                        p.organos[0] = suplente;
                        cartas.Add(p.cartasmano[id]);
                        p.cartasmano.Remove(p.cartasmano[id]);
                        mazo.CogerCarta(p);
                    }
                    else if (p.organos[0] is  null)
                    {
                        WriteLine($"|No tienes un organo ahí|");
                        ReadLine();
                    }
                }
                if (or[0] is null)
                {
                    WriteLine($"|No hay un organo|");
                    ReadLine();
                }
                break;
                    case "2":
                if(or[1] is not null)
                {
                    if (p.organos[1] is not null)
                    {
                        suplente = e.organos[1];
                        e.organos[1] = p.organos[1];
                        p.organos[1] = suplente;
                        cartas.Add(p.cartasmano[id]);
                        p.cartasmano.Remove(p.cartasmano[id]);
                        mazo.CogerCarta(p);
                    }
                    else if (p.organos[1] is  null)
                    {
                        WriteLine($"|No tienes un organo ahí|");
                        ReadLine();
                    }
                }
                if (or[1] is null)
                {
                    WriteLine($"|No hay un organo|");
                    ReadLine();
                }
                break;
                    case "3":
                if(or[2] is not null)
                {
                    if (p.organos[2] is not null)
                    {
                        suplente = e.organos[2];
                        e.organos[2] = p.organos[2];
                        p.organos[2] = suplente;
                        cartas.Add(p.cartasmano[id]);
                        p.cartasmano.Remove(p.cartasmano[id]);
                        mazo.CogerCarta(p);
                    }
                    else if (p.organos[2] is  null)
                    {
                        WriteLine($"|No tienes un organo ahí|");
                        ReadLine();
                    }
                }
                if (or[2] is null)
                {
                    WriteLine($"|No hay un organo|");
                    ReadLine();
                }
                break;
                    case "4":
                if(or[3] is not null)
                {
                    if (p.organos[3] is not null)
                    {
                        suplente = e.organos[3];
                        e.organos[3] = p.organos[3];
                        p.organos[3] = suplente;
                        cartas.Add(p.cartasmano[id]);
                        p.cartasmano.Remove(p.cartasmano[id]);
                        mazo.CogerCarta(p);
                    }
                    else if (p.organos[3] is  null)
                    {
                        WriteLine($"|No tienes un organo ahí|");
                        ReadLine();
                    }
                }
                if (or[3] is null)
                {
                    WriteLine($"|No hay un organo|");
                    ReadLine();
                }
                break;
                    default:
                        WriteLine("Input no valido");
                        ReadLine();
                        return false;
        }
        return false;
    }
}