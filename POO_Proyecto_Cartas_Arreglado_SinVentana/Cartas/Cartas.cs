namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public abstract class Cartas
{
    public virtual string Nombre { get; set; }
    public enum Type
    {
        Sanguíneo,
        Gástrico,
        Ósseo,
        Neuronal,
        Comodín
    }
    public Type Tipo { get; set; }
    public string Cara {get; set;}
}
public class Curas : Cartas
{
    public override string Nombre { get; set; } = "Cura";
}
public class Bacterias : Cartas
{
    public override string Nombre { get; set; } = "Bacteria";
}