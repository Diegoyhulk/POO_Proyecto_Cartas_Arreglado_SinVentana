namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public interface IInfectable
{
    public bool Infectar(Player player,List<Cartas> cartas, int id);
    public bool Curar(Player player,List<Cartas> cartas, int id);
}