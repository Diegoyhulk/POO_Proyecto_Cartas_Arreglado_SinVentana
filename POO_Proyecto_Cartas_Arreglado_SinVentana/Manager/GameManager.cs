using Raylib_cs;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Manager;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();

            return _instance;
        }
    }
    private GameManager() { }

    public Player player;
    public Enemy[] enemies;
    public Mazo<Cartas> mazo;
    public int num;
    public List<Jugador> players;
    public Dictionary<Cartas, Texture2D> texturas;
    public Coleccion coleccion;
    public EnemyAI ai;
    public EspecialesC comando;
    public int indiceCarta;
    public int indiceenemigo;
    public Especiales esp;
}