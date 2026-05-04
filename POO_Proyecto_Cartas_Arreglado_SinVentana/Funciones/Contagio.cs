namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EContagio
{
    public bool Contagiar(Jugador p, Jugador e, Mazo<Cartas> mazo, List<Cartas> cartas, int id)
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
                    if (organo.HP < 2 && or.HP <= 2)
                    {
                        or.HP--;
                        organo.HP++;
                        if (or.HP == 0) 
                        {
                            cartas.Add(or);
                            e.organos[j] = null;
                            or.HP = 2;
                        }
                        cartas.Add(p.cartasmano[id]);
                        p.cartasmano.Remove(p.cartasmano[id]);
                        mazo.CogerCarta(p);
                        if (p is Player)
                        {
                            WriteLine("No has infectado al enemigo!");
                        }
                        return true;
                    }
                    j++;
                }
            }
            i++;
        }

        if (p is Player)
        {
            WriteLine("No has podido infectar al enemigo!");
        }
        return false;
    }
}