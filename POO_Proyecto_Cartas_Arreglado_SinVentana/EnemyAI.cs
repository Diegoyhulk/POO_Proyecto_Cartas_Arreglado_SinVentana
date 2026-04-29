namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

using static System.Console;
public class EnemyAI
{
    bool usadocarta;
    public IInfectable iinf = new Organos();
    public void ETurno(Enemy enemy, Mazo mazo, List<Cartas> cartas, Player player)
    {
        int rnd = Random.Shared.Next(0, 2);
        while (true)
        {
            if (rnd == 0) //Attack mode
            {
                if (EUsarBacteria(enemy,player,mazo,cartas)){break;}
                else if (EUsarEspecial(enemy,cartas,mazo)){break;}
                else if (EUsarCura(enemy,mazo,cartas)){break;}
                else if (EUsarOrgano(enemy,mazo)){break;}
                else if (EDescartar(cartas, mazo, enemy))
                {
                    WriteLine("He Descartado una carta");
                    ReadLine();
                    break;
                }
            }
            else //Defense Mode
            {
                if (EUsarCura(enemy,mazo,cartas)){break;}
                else if (EUsarOrgano(enemy,mazo)){break;}
                else if (EUsarBacteria(enemy,player,mazo,cartas)){break;}
                else if (EUsarEspecial(enemy,cartas,mazo)){break;}
                else if (EDescartar(cartas, mazo, enemy))
                {
                    WriteLine("He Descartado una carta");
                    ReadLine();
                    break;
                }
            }
        }
    }

    private bool EDescartar(List<Cartas> cartas ,Mazo mazo, Enemy e)
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

    private bool EUsarBacteria(Enemy e,Player player,Mazo mazo, List<Cartas> cartas)
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

    private bool EUsarCura(Enemy e,Mazo mazo, List<Cartas> cartas)
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
                    if (iinf.Curar(e, cartas, i))
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
    private bool EUsarEspecial(Enemy enemy,List<Cartas> cartas, Mazo mazo)
    {
        
        int i = 0;
        foreach (var cart in enemy.cartasmano)
        {
            if (enemy.cartasmano[i] is Especiales)
            {
                WriteLine("He usado una carta especial");
                ReadLine();
                mazo.DescartarCarta(cartas,enemy,i);
                mazo.CogerCarta(enemy);
                return true;
            }
            i++;
        }
        
        return false;
    }
    private bool EUsarOrgano(Enemy e, Mazo mazo)
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