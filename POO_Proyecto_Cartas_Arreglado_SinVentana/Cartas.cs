namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public abstract class Cartas
{
    public abstract string Nombre { get; set; }
    public string Type;
}
public class Curas : Cartas
{
    public override string Nombre { get; set; } = "Cura";
}
public class Bacterias : Cartas
{
    public override string Nombre { get; set; } = "Bacteria";
}