using System.ComponentModel;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;
using static System.Console;

public class Mazo<T> where T : Cartas
{
    public Queue<T> coleccion = new Queue<T>();
    public int CantidadMazo => coleccion.Count;

    public void Shuffle(List<T> cartas)
    {
        Random rng = new Random();
        while (cartas.Count > 0)
        {
            int rand = rng.Next(cartas.Count);
            coleccion.Enqueue(cartas[rand]);
            cartas.Remove(cartas[rand]);
        }
    }
    public bool CogerCarta(Jugador p)
    {
        if (p is Player)
        {
            if (p.cartasmano.Count == 3){WriteLine("Tienes no puedes coger más!");
                WriteLine("Pulsa cualquier tecla para continuar");
                ReadLine(); return false;}
            WriteLine($"Has cogido la carta {coleccion.Peek().Nombre}");
            p.cartasmano.Add(coleccion.Dequeue());
            WriteLine($"Pulsa cualquier tecla para continuar");
            ReadLine();
            return true;
        }
        else
        {
            if (p.cartasmano.Count == 3)
            {
                return false;
            }
            p.cartasmano.Add(coleccion.Dequeue());
            return true;
        }
    }

    public void CartasIniciales(Jugador p)
    {
        for (int i = 0; i < 3; i++)
        {
            WriteLine($"Has cogido la carta {coleccion.Peek().Nombre}");
            p.cartasmano.Add(coleccion.Dequeue());
        }
    }
    public void DescartarCarta(List<T> cartas,Jugador p, int i)
    {
        if (p is Player)
        {
            WriteLine($"Carta eliminada: {p.cartasmano[i].Nombre}");
            cartas.Add((T)p.cartasmano[i]);
            p.cartasmano.Remove(p.cartasmano[i]);
            WriteLine("Pulsa cualquier tecla para continuar");
            ReadLine();
        }
        else
        {
            cartas.Add((T)p.cartasmano[i]);
            p.cartasmano.Remove(p.cartasmano[i]);
        }
        
    }
}