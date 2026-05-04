namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public interface IUsarEspecial
{
    public bool UsarEspecial(Jugador p, Jugador e, Mazo<Cartas> mazo, List<Cartas> cartas, int id);
}