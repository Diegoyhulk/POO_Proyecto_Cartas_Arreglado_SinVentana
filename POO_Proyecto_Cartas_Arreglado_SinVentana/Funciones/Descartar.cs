namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EDescartar
{
    public void Descartar(Jugador p, Jugador e, Mazo mazo, List<Cartas> cartas)
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
}