using POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EnemyAI
{
    bool usadocarta;
    public IInfectable iinf = new Infecta();
    public ICurable icur = new Cura();
    public void ETurno(Enemy[] enemy, Mazo<Cartas> mazo, List<Cartas> cartas, Player player, EspecialesC comando, List<Jugador> players, int id, int num)
    {
        Jugador objective = new Jugador();
        while (true)
        {
            if(ElegirObjetivo(enemy,player,ref objective,id, num)){break;}
        }
        int rnd = Random.Shared.Next(0, 2);
        while (true)
        {
            if (rnd == 0) //Attack mode
            {
                if (EUsarBacteria(enemy[id],objective,mazo,cartas)){break;}
                else if (EUsarEspecial(objective,enemy[id],cartas,mazo,comando, players)){break;}
                else if (EUsarCura(enemy[id],mazo,cartas)){break;}
                else if (EUsarOrgano(enemy[id],mazo)){break;}
                else if (EDescartar(cartas, mazo, enemy[id]))
                {
                    WriteLine("He Descartado una carta");
                    ReadLine();
                    break;
                }
            }
            else //Defense Mode
            {
                if (EUsarCura(enemy[id],mazo,cartas)){break;}
                else if (EUsarOrgano(enemy[id],mazo)){break;}
                else if (EUsarBacteria(enemy[id],objective,mazo,cartas)){break;}
                else if (EUsarEspecial(objective, enemy[id], cartas, mazo, comando, players)){break;}
                else if (EDescartar(cartas, mazo, enemy[id]))
                {
                    WriteLine("He Descartado una carta");
                    ReadLine();
                    break;
                }
            }
        }
    }

    private bool ElegirObjetivo(Enemy[] enemies, Player player, ref Jugador objective, int id, int num)
    {
        int rnd = Random.Shared.Next(0, 4);
        switch (rnd)
        {
            case 0:
                objective = player;
                return true;
            case 1:
                if(enemies[0] == enemies[id]){return false;}
                objective = enemies[0];
                return true;
            case 2:
                if(enemies[1] == enemies[id] || num < 2){return false;}
                objective = enemies[1];
                return true;
            case 3:
                if(enemies[2] == enemies[id] || num < 3){return false;}
                objective = enemies[2];
                return true;
        }
        return false;
    }
    private bool EDescartar(List<Cartas> cartas ,Mazo<Cartas> mazo, Enemy e)
    {
        int i = 0;
        foreach (Cartas carta in e.cartasmano)
        {
            if (carta is Bacterias)
            {
                mazo.DescartarCarta(cartas,e,i); 
                mazo.CogerCarta(e);
                return true;
            }
            i++;
        }
        i = 0;
        foreach (Cartas carta in e.cartasmano)
        {
            if (carta is Curas)
            {
                mazo.DescartarCarta(cartas,e,i); 
                mazo.CogerCarta(e);
                return true;
            }
            i++;
        }
        i = 0;
        foreach (Cartas carta in e.cartasmano)
        {
            if (carta is Organos)
            {
                mazo.DescartarCarta(cartas,e,i);
                mazo.CogerCarta(e);
                return true;
            }
            i++;
        }
        i = 0;
        foreach (Cartas carta in e.cartasmano)
        {
            if (carta is Especiales)
            {
                mazo.DescartarCarta(cartas,e,i); 
                mazo.CogerCarta(e);
                return true;
            }
            i++;
        }
        i = 0;
        return true;
    }

    private bool EUsarBacteria(Enemy e,Jugador player,Mazo<Cartas> mazo, List<Cartas> cartas)
    {
        int i = 0;
        foreach (Organos org in e.organos)
        {
            i = 0;
            if (org != null)
            {
                foreach (Cartas bact in e.cartasmano)
                {
                    if (bact is not Bacterias){i++;continue;}
                    if (iinf.Infectar(e, player, cartas, i))
                    {
                        WriteLine("He usado una Bacteria");
                        ReadLine();
                        mazo.CogerCarta(e);
                        return true;
                    }
                }
                return false;
            }
            
        }
        return false;
    }

    private bool EUsarCura(Enemy e,Mazo<Cartas> mazo, List<Cartas> cartas)
    {
        int i = 0;
        foreach (Organos org in e.organos)
        {
            i = 0;
            if (org != null)
            {
                foreach (Cartas cura in e.cartasmano)
                {
                    if (cura is not Curas){i++;continue;}
                    if (icur.Curar(e, cartas, i))
                    {
                        WriteLine("He usado una Cura");
                        ReadLine();
                        mazo.CogerCarta(e);
                        return true;
                    }
                }
            }
            
        }
        return false;
    }
    private bool EUsarEspecial(Jugador player,Enemy enemy,List<Cartas> cartas, Mazo<Cartas> mazo, EspecialesC comando, List<Jugador> players)
    {
        
        int id = 0;
        foreach (var cart in enemy.cartasmano)
        {
            if (enemy.cartasmano[id] is Especiales esp)
            {
                comando.UsarEspeciales(enemy,player,mazo,cartas,id, players);
                WriteLine($"He usado una carta Especial de {esp.uso}");
                ReadLine();
                mazo.DescartarCarta(cartas,enemy,id);
                mazo.CogerCarta(enemy);
                return true;
            }
            id++;
        }
        
        return false;
    }
    private bool EUsarOrgano(Enemy e, Mazo<Cartas> mazo)
    {
        int i = 0;
        foreach (var cart in e.cartasmano)
        {
            if (e.cartasmano[i] is Organos)
            {
                if (e.poner_organos(i, e))
                {
                    WriteLine("He puesto un Organo");
                    ReadLine();
                    mazo.CogerCarta(e);
                    return true;
                }
                return false;
            }
            i++;
        }
        return false;
    }
}