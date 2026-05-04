namespace POO_Proyecto_Cartas_Arreglado_SinVentana.ASSCCI;
using static System.Console;
public class Printer
{
    private static string[] Organo =
    {
        "+---------------------+",
        "|                     |",
        "|                     |",
        "|        OOOOO        |",
        "|       O     O       |",
        "|       O     O       |",
        "|       O     O       |",
        "|        OOOOO        |",
        "|                     |",
        "|                     |",
        "+---------------------+"
    };
    static string[] Bacteria = {
        "+---------------------+",
        "|                     |",
        "|                     |",
        "|       BBBBBB        |",
        "|       B     B       |",
        "|       BBBBBB        |",
        "|       B     B       |",
        "|       BBBBBB        |",
        "|                     |",
        "|                     |",
        "+---------------------+"
    };
    static string[] Cura = {
        "+---------------------+",
        "|                     |",
        "|                     |",
        "|        CCCCC        |",
        "|       C             |",
        "|       C             |",
        "|       C             |",
        "|        CCCCC        |",
        "|                     |",
        "|                     |",
        "+---------------------+"
    };
    private static string[] Especial =
    {
        "+---------------------+",
        "|                     |",
        "|                     |",
        "|       EEEEEEE       |",
        "|       E             |",
        "|       EEEEE         |",
        "|       E             |",
        "|       EEEEEEE       |",
        "|                     |",
        "|                     |",
        "+---------------------+"
    };
    string[][] mazo = { Organo, Bacteria, Cura, Especial };
    public void PrintCartasMano(List<Cartas> cartas)
    {
        int filas = mazo[0].Length;

        for (int i = 0; i < filas; i++)
        {
            foreach (Cartas carta in cartas)
            {
                int tipoIndex = carta switch
                {
                    Organos => 0,
                    Bacterias => 1,
                    Curas => 2,
                    Especiales => 3,
                    _ => 0
                };
                PrintColor(carta);
                Write(mazo[tipoIndex][i] + "   ");
            }
            WriteLine();
        }
        foreach (Cartas carta in cartas)
        {
            if (carta is Especiales esp)
            {
                string nombre = carta.Nombre;
                string use = esp.uso.ToString();
                int ancho = mazo[0][0].Length;
                int padding = (ancho - nombre.Length - use.Length - 1) / 2;
                if (padding < 0) padding = 0;
                PrintColor(carta);
                Write(new string(' ', padding) + nombre + ' ' + use + new string(' ', padding) + "   ");
            }
            else
            {
                string nombre = carta.Nombre;
                string type = carta.Tipo.ToString();
                int ancho = mazo[0][0].Length;
                int padding = (ancho - nombre.Length - type.Length - 1) / 2;
                if (padding < 0) padding = 0;
                PrintColor(carta);
                Write(new string(' ', padding) + nombre + ' ' + type + new string(' ', padding) + "   ");
            }
        }
        ForegroundColor = ConsoleColor.Gray;
        WriteLine();
    }

    public void PrintOrganos(Cartas[] organos)
    {
        int filas = mazo[0].Length;

        for (int i = 0; i < filas; i++)
        {
            foreach (Cartas carta in organos)
            {
                if(carta is null) { continue; }
                PrintColor(carta);
                Write(mazo[0][i] + "   ");
            }
            WriteLine();
        }
        foreach (Organos carta in organos)
        {
            if (carta is null) continue;

            string texto = $"{carta.Nombre} {carta.Tipo}";
            int ancho = mazo[0][0].Length;
            int padding = (ancho - texto.Length) / 2;
            if (padding < 0) padding = 0;

            PrintColor(carta);
            Write(new string(' ', padding) + texto + new string(' ', padding) + "   ");
        }
        WriteLine();
        foreach (Organos carta in organos)
        {
            if (carta is null) continue;

            string salud = carta.HP switch
            {
                2 => "Esta Saludable",
                1 => "Esta con una bacteria",
                3 => "Esta con un antibiótico",
                4 => "Esta inmunizado!",
                _ => "Esta Saludable"
            };

            int ancho = mazo[0][0].Length;
            int padding = (ancho - salud.Length) / 2;
            if (padding < 0) padding = 0;

            PrintColor(carta);
            Write(new string(' ', padding) + salud + new string(' ', padding) + "   ");
        }
        ForegroundColor = ConsoleColor.Gray;
        WriteLine();
    }
    

    private static void PrintColor(Cartas carta)
    {
        if (carta is Especiales) { ForegroundColor = ConsoleColor.White; return; }
        switch (carta.Tipo)
        {
            case Cartas.Type.Sanguíneo:
                ForegroundColor = ConsoleColor.Red;
                break;
            case Cartas.Type.Ósseo:
                ForegroundColor = ConsoleColor.Yellow;
                break;
            case Cartas.Type.Gástrico:
                ForegroundColor = ConsoleColor.Green;
                break;
            case Cartas.Type.Neuronal:
                ForegroundColor = ConsoleColor.Blue;
                break;
            case Cartas.Type.Comodín:
                ForegroundColor = ConsoleColor.Magenta;
                break;
        }
    }
}