namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class Organos : Cartas, IInfectable
{
    public override string Nombre { get; set; } = "Organo";
    public int HP { get; private set; } = 2;
    public bool inmunizado = false;

    public bool Infectar(Player player, List<Cartas> cartas, int id)
    {
        int i = 0;
        foreach (Organos org in player.organos)
        {
            if (org != null && !org.inmunizado && player.cartasmano[id].Type == org.Type || org != null && player.cartasmano[id].Type == "Comodín" && !org.inmunizado )
            {
                org.HP--;
                if (org.HP == 0)
                {
                    cartas.Add(org);
                    player.organos[i] = null;
                    org.HP = 2;
                }
                cartas.Add(player.cartasmano[id]);
                player.cartasmano.RemoveAt(id);
                return true;
            }

            if (org != null && org.inmunizado && player.cartasmano[id].Type == org.Type ||
                org != null && player.cartasmano[id].Type == "Comodín" && org.inmunizado)
            {
                WriteLine($"| El Organo {player.cartasmano[i].Type} esta inmunizado!");
                ReadLine();
                return false;
            }
            i++;
        }
        WriteLine("No hay organo que infectar");
        ReadLine();
        return false;
    }

    public bool Curar(Player player, List<Cartas> cartas, int id)
    {
        int i = 0;
        foreach (Organos org in player.organos)
        {
            if (org != null && player.cartasmano[id].Type == org.Type && !org.inmunizado || org != null && player.cartasmano[id].Type == "Comodín" && !org.inmunizado )
            {
                org.HP++;
                if (org.HP == 4)
                {
                    org.inmunizado = true;
                }
                cartas.Add(player.cartasmano[id]);
                player.cartasmano.RemoveAt(id);
                return true;
            }

            if (org != null && player.cartasmano[id].Type == org.Type && org.inmunizado ||
                org != null && player.cartasmano[id].Type == "Comodín" && org.inmunizado)
            {
                WriteLine($"| El Organo {player.cartasmano[i].Type} ya esta inmunizado!");
                ReadLine();  
                return false;
            }
            i++;
        }
        WriteLine("No hay organo que curar");
        ReadLine();
        return false;
    }
}