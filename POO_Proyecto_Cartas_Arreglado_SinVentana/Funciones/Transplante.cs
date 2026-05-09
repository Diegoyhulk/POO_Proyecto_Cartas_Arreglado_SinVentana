namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;
public class ETransplante
{
    public bool Transplantar(Jugador p, Jugador e, Mazo<Cartas> mazo, List<Cartas> cartas, int id)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
        if (p is Enemy)
        {
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
                {
                }

                j++;
            }

            Cartas suplente = new Organos();
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
                            if (p.organos[0] is not null && e.organos[0] is not null)
                            {
                                suplente = e.organos[0];
                                e.organos[0] = p.organos[0];
                                p.organos[0] = suplente;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                                return true;
                            }
                            else if (p.organos[0] is null)
                            {}
                        }
                        catch (NullReferenceException){}
                        input++;
                        break;
                    case 1:
                        try
                        {
                            if (p.organos[1] is not null && e.organos[1] is not null)
                            {
                                suplente = e.organos[1];
                                e.organos[1] = p.organos[1];
                                p.organos[1] = suplente;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                                return true;
                            }
                            else if (p.organos[1] is null)
                            {}
                        }
                        catch (NullReferenceException){}
                        input++;
                        break;
                    case 2:
                        try
                        {
                            if (p.organos[2] is not null && e.organos[2] is not null)
                            {
                                suplente = e.organos[2];
                                e.organos[2] = p.organos[2];
                                p.organos[2] = suplente;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                                return true;
                            }
                            else if (p.organos[2] is null)
                            {}
                        }
                        catch (NullReferenceException){}
                        input++;
                        break;
                    case 3:
                        try
                        {
                            if (p.organos[3] is not null && e.organos[3] is not null)
                            {
                                suplente = e.organos[3];
                                e.organos[3] = p.organos[3];
                                p.organos[3] = suplente;
                                cartas.Add(p.cartasmano[id]);
                                p.cartasmano.Remove(p.cartasmano[id]);
                                mazo.CogerCarta(p);
                                return true;
                            }
                            else if (p.organos[3] is null)
                            {}
                        }
                        catch (NullReferenceException){}
                        input++;
                        break;
                    default:
                        WriteLine("Input no valido");
                        ReadLine();
                        return false;
                }
            }
        }

        return false;
    }
}