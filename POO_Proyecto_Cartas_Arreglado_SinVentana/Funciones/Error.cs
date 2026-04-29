namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

using static System.Console;
public class EError
{
    public bool Error(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas, int id)
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
}