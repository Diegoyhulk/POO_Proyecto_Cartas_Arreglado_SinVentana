namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EDescartar
{
    public void Descartar(List<Jugador> players, Mazo<Cartas> mazo, List<Cartas> cartas, int num)
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (Jugador player in players)
            {
                if (mazo.coleccion.Count > 0)
                {
                    cartas.Add(player.cartasmano[i]);
                    player.cartasmano.Remove(player.cartasmano[i]);
                    player.cartasmano.Add(mazo.coleccion.Dequeue());
                }
                else
                {
                    mazo.Shuffle(cartas);
                    cartas.Add(player.cartasmano[i]);
                    player.cartasmano.Remove(player.cartasmano[i]);
                    player.cartasmano.Add(mazo.coleccion.Dequeue());
                }
            }
        }
        
    }
}