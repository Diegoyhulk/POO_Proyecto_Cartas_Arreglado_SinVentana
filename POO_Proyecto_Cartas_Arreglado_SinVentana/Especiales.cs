namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public class Especiales : Cartas
{
    public override string Nombre { get; set; } = "Especial";

    public enum Uso
    {
        Robo,
        Descarte,
        Transplante,
        Error,
        Contagio
    }
    
    public Uso uso { get; set; }
}