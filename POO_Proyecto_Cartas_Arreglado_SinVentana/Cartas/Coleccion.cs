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
                        Tipo = Cartas.Type.Sanguíneo
                    });
                    break;
                case <= 10 and >5:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Ósseo
                    });
                    break;
                case <= 15 and > 10:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Neuronal
                    });
                    break;
                case <= 20 and >15:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Gástrico
                    });
                    break;
                case <= 21 and >20:
                    cartas.Add(new Organos
                    {
                        Tipo = Cartas.Type.Comodín
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
                        Tipo = Cartas.Type.Sanguíneo
                    });
                    break;
                case <= 8 and >4:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Ósseo
                    });
                    break;
                case <= 12 and > 8:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Neuronal
                    });
                    break;
                case <= 16 and >12:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Gástrico
                    });
                    break;
                case <= 17 and >16:
                    cartas.Add(new Bacterias
                    {
                        Tipo = Cartas.Type.Comodín
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
                        Tipo = Cartas.Type.Sanguíneo
                    });
                    break;
                case <= 8 and >4:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Ósseo
                    });
                    break;
                case <= 12 and > 8:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Neuronal
                    });
                    break;
                case <= 16 and >12:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Gástrico
                    });
                    break;
                case <= 20 and >16:
                    cartas.Add(new Curas
                    {
                        Tipo = Cartas.Type.Comodín
                    });
                    break;
            }
        }
        for (int i = 1; i <= 10; i++)
        {/*
            if (i <= 10)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Robo});
            }
            if (i is <= 4 and >2)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Descarte});
            }
            if (i is <= 6 and >4)
            {
                cartas.Add(new Especiales{uso = Especiales.Uso.Transplante});
            }
            if (i is <= 8 and >6)
            {
                cartas.Add(new Especiales{uso =Especiales.Uso.Error});
            }*/
            if (true)
            {
                cartas.Add(new Especiales{uso =Especiales.Uso.Contagio});
            }
        }
    }
}