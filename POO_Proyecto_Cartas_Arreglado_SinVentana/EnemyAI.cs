namespace POO_Proyecto_Cartas_Arreglado_SinVentana;

public class EnemyAI
{
    public void ETurno(Enemy enemy, Mazo mazo)
    {
        while (true)
        {
            int rnd = new Random().Next(0, 4);
            if (rnd == 0 && enemy.cartasmano.Count < 3){mazo.ECogerCarta(enemy);break;}
            if (rnd == 1){if (EUsarBacteria()){break;}}
            if (rnd == 2){if (EUsarCura()){break;}}
            if (rnd == 3){if (EUsarOrgano(enemy)){break;}}
            //if(rnd == 4){if(EDescartar(enemy){break;})}
        }
    }
    private bool EDescartar(List<Cartas> cartas ,Mazo mazo, Enemy e)
    {
        throw new NotImplementedException();
    }

    private bool EUsarBacteria()
    {
        throw new NotImplementedException();
    }

    private bool EUsarCura()
    {
        throw new NotImplementedException();
    }

    private bool EUsarOrgano(Enemy e)
    {
        int i = 0;
        foreach (var cart in e.cartasmano)
        {
            if (e.cartasmano[i] is Organos)
            {
                if(e.poner_organos(i)){return true;}
            }
            i++;
        }
        return false;
    }
}