namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public interface IInfectable
{
    public bool Infectar(Jugador player,Jugador enemy,List<Cartas> cartas, int id);
    
}