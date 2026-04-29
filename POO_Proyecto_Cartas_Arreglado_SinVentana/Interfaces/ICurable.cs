namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public interface ICurable
{
    public bool Curar(Jugador player, List<Cartas> cartas, int id);
}