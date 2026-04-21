namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class Organos : Coleccion.Cartas, IInfectable
{
    public override string Nombre { get; set; } = "Organo";
    private int cantidaddebacterias = 0;
    public bool sano = true;
    public event Action<int, List<Coleccion.Cartas>> EliminarOrgano;

    public bool Infectar(Player player, List<Coleccion.Cartas> cartas)
    {
        bool hascard = false;
        foreach (var c in player.cartasmano)
        {
                if (c is Bacterias)
                {
                    hascard = true;
                    break;
                }
        }
        if (hascard)
        {
                WriteLine("Que bacteria quieres usar?");
                if (player.cartasmano.Count > 0 && player.cartasmano[0] is Bacterias)
                {
                    WriteLine($"(1):"); Nombrar_Carta(player, 0);
                }
                if (player.cartasmano.Count > 1 && player.cartasmano[1] is Bacterias)
                {
                    WriteLine($"(2):"); Nombrar_Carta(player, 1);
                }
                if (player.cartasmano.Count > 2 && player.cartasmano[2] is Bacterias)
                {
                    WriteLine($"(3):"); Nombrar_Carta(player, 2);
                }
                WriteLine("(Enter)Atras");
        }
        else
        {
                WriteLine("No tienes bacterias!");
                WriteLine("Pulsa enter para continuar");
                ReadLine();
                return false;
        }
        string input = ReadLine();
        switch (input)
        {
            case "1" when player.cartasmano[0] is Bacterias:
            {
                int i = 0;
                foreach (Organos org in player.organos)
                {
                    if (org != null && player.cartasmano[0].Type == org.Type || org != null && player.cartasmano[0].Type == "Comodín" )
                    {
                        org.sano = false;
                        org.cantidaddebacterias++;
                        if (org.cantidaddebacterias == 2)
                        {
                            EliminarOrgano?.Invoke(i,cartas);
                        }

                        cartas.Add(player.cartasmano[0]);
                        player.cartasmano.RemoveAt(0);
                        return true;
                    }
                    i++;
                }
                WriteLine("No hay organo que infectar");
                WriteLine("Pulsa enter para continuar");
                ReadLine();
                break;
            }
            case "2" when player.cartasmano[1] is Bacterias:
            {
                int i = 0;
                foreach (Organos org in player.organos)
                {
                    if (org != null && player.cartasmano[1].Type == org.Type || org != null && player.cartasmano[1].Type == "Comodín")
                    {
                        org.sano = false;
                        org.cantidaddebacterias++;
                        if (org.cantidaddebacterias == 2)
                        {
                            EliminarOrgano?.Invoke(i, cartas);
                        }

                        cartas.Add(player.cartasmano[0]);
                        player.cartasmano.RemoveAt(1);
                        return true;
                    }
                    i++;
                }
                WriteLine("No hay organo que infectar");
                WriteLine("Pulsa enter para continuar");
                ReadLine();
                break;
            }
            case "3" when player.cartasmano[2] is Bacterias:
            {
                int i = 0;
                foreach (Organos org in player.organos)
                {
                    if (org != null && player.cartasmano[2].Type == org.Type || org != null && player.cartasmano[2].Type == "Comodín")
                    {
                        org.sano = false;
                        org.cantidaddebacterias++;
                        if (org.cantidaddebacterias == 2)
                        {
                            EliminarOrgano?.Invoke(i,cartas);
                            org.sano = true;
                            org.cantidaddebacterias = 0;
                        }
                        cartas.Add(player.cartasmano[0]);
                        player.cartasmano.RemoveAt(2);
                        return true;
                    }
                    i++;
                }
                WriteLine("No hay organo que infectar");
                WriteLine("Pulsa enter para continuar");
                ReadLine();
                break;
            }
            default:
                    WriteLine("input no valido");
                    WriteLine("Pulsa enter para continuar");
                    ReadLine();
                    break;
        }
        return false;
    }
    private void Nombrar_Carta(Player player, int i)
    {
        WriteLine($"Carta {i+1}:{player.cartasmano[i].Nombre}");
        if (player.cartasmano[i].Nombre != "Especial")
        {
            WriteLine($"| tipo:{player.cartasmano[i].Type}");
        }
        if (player.cartasmano[i] is Especiales esp)
        {
            WriteLine($"| uso:{esp.Uso}");
        }
    }
}