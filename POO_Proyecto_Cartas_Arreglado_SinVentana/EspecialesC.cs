namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EspecialesC
{
    public bool UsarEspeciales(Jugador p, Jugador e,Mazo mazo, List<Cartas> cartas, int id)
    {
        if (p.cartasmano[id] is Especiales esp)
        {
            if (esp.uso is Especiales.Uso.Robo)
            {
                if(Robar(p,e,mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Descarte)
            {
                Descartar(p, e, mazo, cartas);
                return true;
            }
            if (esp.uso is Especiales.Uso.Transplante)
            {
                if(Transplantar(p, e, mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Error)
            {
                if(Error(p, e, mazo, cartas, id)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Contagio)
            {
                if(Contagiar(p, e, mazo, cartas, id)){return true;}
                return false;
            }
        }
        else
        {
            WriteLine("Algo anda mal");
            ReadLine();
        }
        return false;
    }
    bool Robar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas, int id)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
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
        string input = ReadLine();
        switch (input)
        {
            case "1":
                        if(or[0] is not null)
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
                        if (or[0] is null)
                        {
                            WriteLine($"|No hay un organo|");
                            ReadLine();
                        }
                        break;
                    case "2":
                        if (or[1] is not null)
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
                        if (or[1] is null)
                        {
                            WriteLine($"|No hay un organo|");
                            ReadLine();
                        }
                        break;
                    case "3":
                        if (or[2] is not null)
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
                        if (or[2] is null)
                        {
                            WriteLine($"|No hay un organo|");
                            ReadLine();
                        }
                        break;
                    case "4":
                        if (or[3] is not null)
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
    void Descartar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas)
    {
        for (int i = 0; i < 3; i++)
        {
            cartas.Add(p.cartasmano[i]);
            p.cartasmano.Remove(p.cartasmano[i]);
            p.cartasmano.Add(mazo.coleccion.Dequeue());
            cartas.Add(e.cartasmano[i]);
            e.cartasmano.Remove(e.cartasmano[i]);
            e.cartasmano.Add(mazo.coleccion.Dequeue());
        }
        WriteLine("Se han descartado todas las cartas en mano");
        ReadLine();
    }
    bool Transplantar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas,int id)
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
    bool Error(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas, int id)
    {
        Cartas[] cuerposuplente = new Organos[4];
        cuerposuplente = e.organos;
        e.organos = p.organos;
        p.organos = cuerposuplente;
        cartas.Add(p.cartasmano[id]);
        p.cartasmano.Remove(p.cartasmano[id]);
        mazo.CogerCarta(p);
        WriteLine("Habeis intercambiado cuerpos!");
        ReadLine();
        return true;
    }
    bool Contagiar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas, int id)
    {
        int i = 0;
        int j = 0;
        foreach (Organos organo in p.organos)
        {
            foreach (Organos or in e.organos)
            {
                if (organo is null || or is null){continue;}

                if (organo.Tipo == or.Tipo || organo.Tipo == Cartas.Type.Comodín || or.Tipo == Cartas.Type.Comodín)
                {
                    if (organo.HP < 2)
                    {
                        if (or.HP <= 2)
                        {
                            or.HP--;
                            organo.HP++;
                            if (or.HP == 0)
                            {
                                cartas.Add(or);
                                e.organos[j] = null;
                                or.HP = 2;
                            }
                        }
                    }
                    j++;
                }
            }
            i++;
        }
        WriteLine("Has infectado todas tus bacterias posibles al enemigo!");
        return true;
    }
}