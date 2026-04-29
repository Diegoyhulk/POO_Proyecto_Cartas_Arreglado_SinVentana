namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class Organos : Cartas
{   
    public override string Nombre { get; set; } = "Organo";
    public int HP { get; set; } = 2;
    public bool inmunizado = false;
}