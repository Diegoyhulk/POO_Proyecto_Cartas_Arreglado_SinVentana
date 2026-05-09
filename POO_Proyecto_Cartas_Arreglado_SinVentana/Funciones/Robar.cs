namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;
using static System.Console;

public class ERobar
{
    public bool Robar(Jugador p, Jugador e, Mazo<Cartas> mazo, List<Cartas> cartas, int id)
    {
        int j = 0;
        int n = 0;
        Organos[] or = new Organos[4];
        
        if (p is Enemy)
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