namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EspecialesC
{
    public bool UsarEspeciales(Player p, Enemy e,Mazo mazo, List<Cartas> cartas, int id)
    {
        if (p.cartasmano[id] is Especiales esp)
        {
            if (esp.uso is Especiales.Uso.Robo)
            {
                if(Robar(p,e,mazo, cartas)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Descarte)
            {
                Descartar(p, e, mazo, cartas);
                return true;
            }
            if (esp.uso is Especiales.Uso.Transplante)
            {
                if(Transplantar(p, e, mazo, cartas)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Error)
            {
                if(Error(p, e, mazo, cartas)){return true;}
                return false;
            }
            if (esp.uso is Especiales.Uso.Contagio)
            {
                if(Contagiar(p, e, mazo, cartas)){return true;}
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
    bool Robar(Player p, Enemy e, Mazo mazo, List<Cartas> cartas)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
        Write("Que organo quieres robar?");
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
        }
        ForegroundColor = ConsoleColor.Gray;
        WriteLine("Tus Organos:");
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
        }
        string input = ReadLine();
        switch (input)
        {
            case "1":
                        if(or[0] is not null)
                        {
                            if (p.organos[0] is null)
                            {
                                
                            }
                            else if (p.organos[0] is not null)
                            {
                                WriteLine($"|Ya tienes un organo ahí|");
                            }
                        }
                        break;
                    case "2":
                        if (or[1] is not null)
                        {
                            if (p.organos[1] is null)
                            {
                                
                            }
                            else if (p.organos[1] is not null)
                            {
                                WriteLine($"|Ya tienes un organo ahí|");
                            } 
                        }
                        break;
                    case "3":
                        if (or[2] is not null)
                        {
                            if (p.organos[2] is null)
                            {
                                
                            }
                            else if (p.organos[2] is not null)
                            {
                                WriteLine($"|Ya tienes un organo ahí|");
                            }
                        }
                        break;
                    case "4":
                        if (or[3] is not null)
                        {
                            if (p.organos[3] is null)
                            {
                                
                            }
                            else if (p.organos[3] is not null)
                            {
                                WriteLine($"|Ya tienes un organo ahí|");
                            }
                        }
                        break;
                    default:
                        WriteLine("Input no valido");
                        ReadLine();
                        return false;
        }
        return false;
    }
    void Descartar(Player p, Enemy e, Mazo mazo, List<Cartas> cartas)
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
    bool Transplantar(Player p, Enemy e, Mazo mazo, List<Cartas> cartas)
    {
        return false;
    }
    bool Error(Player p, Enemy e, Mazo mazo, List<Cartas> cartas)
    {
        return false;
    }
    bool Contagiar(Player p, Enemy e, Mazo mazo, List<Cartas> cartas)
    {
        return false;
    }
}