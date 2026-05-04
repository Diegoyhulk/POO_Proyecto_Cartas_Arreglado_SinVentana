namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EDescartar
{
    public void Descartar(List<Jugador> players, Mazo<Cartas> mazo, List<Cartas> cartas)
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (Jugador player in players)
            {
                cartas.Add(player.cartasmano[i]);
                player.cartasmano.Remove(player.cartasmano[i]);
                player.cartasmano.Add(mazo.coleccion.Dequeue());
            }
        }
        
        WriteLine("Se han descartado todas las cartas en mano");
        ReadLine();
    }
}