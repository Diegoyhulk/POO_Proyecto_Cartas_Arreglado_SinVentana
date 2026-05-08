namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;

public class Coleccion
{
    public List<Cartas> cartas { get; set; } = new List<Cartas>();
    public void GenerarMazo()
    {
        for (int i = 1; i <= 21; i++)
        {
            switch (i)
            {
                case <= 5:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Sanguíneo,
                        Cara = "Texturas/OrganoRojo.jpg"
                    });
                    break;
                case <= 10 and >5:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Ósseo,
                        Cara = "Texturas/OrganoAmarillo.jpg"
                    });
                    break;
                case <= 15 and > 10:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Neuronal,
                        Cara = "Texturas/OrganoAzul.jpg"
                    });
                    break;
                case <= 20 and >15:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Gástrico,
                        Cara = "Texturas/OrganoVerde.jpg"
                    });
                    break;
                case <= 21 and >20:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Comodín,
                        Cara = "Texturas/OrganoComodin.jpg"
                    });
                    break;
            }
        }
        for (int i = 1; i <= 17; i++)
        {
            switch (i)
            {
                case <= 4:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Sanguíneo,
                        Cara = "Texturas/VirusRojo.jpg"
                    });
                    break;
                case <= 8 and >4:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Ósseo,
                        Cara = "Texturas/VirusAmarillo.jpg"
                    });
                    break;
                case <= 12 and > 8:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Neuronal,
                        Cara = "Texturas/VirusAzul.jpg"
                    });
                    break;
                case <= 16 and >12:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Gástrico,
                        Cara = "Texturas/VirusVerde.jpg"
                    });
                    break;
                case <= 17 and > 16:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Comodín,
                        Cara = "Texturas/VirusComodin.jpg"
                    });
                    break;
            }
        }
        for (int i = 1; i <= 20; i++)
        {
            switch (i)
            {
                case <= 4:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Sanguíneo,
                        Cara = "Texturas/CuraRoja.jpg"
                    });
                    break;
                case <= 8 and >4:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Ósseo,
                        Cara = "Texturas/CuraAmarilla.jpg"
                    });
                    break;
                case <= 12 and > 8:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Neuronal,
                        Cara = "Texturas/CuraAzul.jpg"
                    });
                    break;
                case <= 16 and >12:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Gástrico,
                        Cara = "Texturas/CuraVerde.jpg"
                    });
                    break;
                case <= 20 and >16:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Comodín,
                        Cara = "Texturas/CuraComodin.jpg"
                    });
                    break;
            }
        }
        for (int i = 1; i <= 10; i++)
        {
            if (i <= 2)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Robo,
                    Cara = "Texturas/Robo.jpg"});
            }
            if (i is <= 4 and >2)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Descarte,
                    Cara = "Texturas/Descarte.jpg"});
            }
            if (i is <= 6 and >4)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Transplante,
                    Cara = "Texturas/Transplante.jpg"});
            }
            if (i is <= 8 and >6)
            {
                cartas.Add(new Especiales{uso =Especiales.Uso.Error,
                    Cara = "Texturas/Error.jpg"});
            }
            if (i is <= 10 and > 8)
            {
                cartas.Add(new Especiales{uso =Especiales.Uso.Contagio,
                    Cara = "Texturas/Contagio.jpg"});
            }
        }
    }
}