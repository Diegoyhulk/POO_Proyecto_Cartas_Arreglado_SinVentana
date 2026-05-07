namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

using static System.Console;
public class Infecta: IInfectable
{
    public bool Infectar(Jugador player,Jugador enemy, List<Cartas> cartas, int id)
    {
        if (player is Player)
        {
            int i = 0;
            if (player.cartasmano[id].Tipo == Organos.Type.Comodín)
            {
                Organos[] or = new Organos[5];
                int j = 0;
                foreach (Organos org in enemy.organos)
                {
                    j++;
                }

                ConsoleKey input = ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.D1:
                        try
                        {
                            or[0].HP--;
                            if (or[0].HP == 0)
                            {

                                cartas.Add(or[0]);
                                player.organos[0] = null;
                                or[0].HP = 2;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                        }
                        break;
                    case ConsoleKey.D2:
                        try
                        {
                            or[1].HP--;
                            if (or[1].HP == 0)
                            {

                                cartas.Add(or[1]);
                                player.organos[1] = null;
                                or[1].HP = 2;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                        }
                        break;
                    case ConsoleKey.D3:
                        try
                        {
                            or[2].HP--;
                            if (or[2].HP == 0)
                            {

                                cartas.Add(or[2]);
                                player.organos[2] = null;
                                or[2].HP = 2;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                        }
                        break;
                    case ConsoleKey.D4:
                        try
                        {
                            or[3].HP--;
                            if (or[3].HP == 0)
                            {

                                cartas.Add(or[3]);
                                player.organos[3] = null;
                                or[3].HP = 2;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                        }
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                foreach (Organos org in enemy.organos)
                {
                    if (org != null && !org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && !org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        org.HP--;
                        if (org.HP == 0)
                        {
                            cartas.Add(org);
                            enemy.organos[i] = null;
                            org.HP = 2;
                        }
                        cartas.Add(player.cartasmano[id]);
                        player.cartasmano.RemoveAt(id);
                        return true;
                    }
                    if (org != null && org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        return false;
                    }

                    i++;

                }
                return false;
            }
            
        }
        else if (player is Enemy)
        {
            int i = 0;
            if (player.cartasmano[id].Tipo == Organos.Type.Comodín)
            {
                Organos[] or = new Organos[4];
                int j = 0;
                foreach (Organos org in enemy.organos)
                {
                    if (org != null)
                    {
                        or[j] = org;
                    }
                    j++;
                }
                int input = 0;
                while (true)
                {
                    if (input == 4)
                    {
                        return false;
                    }
                    switch (input)
                    {
                        case 0:
                            try
                            {
                                or[0].HP--;
                                if (or[0].HP == 0)
                                {

                                    cartas.Add(or[0]);
                                    player.organos[0] = null;
                                    or[0].HP = 2;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException){}
                            input++;
                            break;
                        case 1:
                            try
                            {
                                or[1].HP--;
                                if (or[1].HP == 0)
                                {

                                    cartas.Add(or[1]);
                                    player.organos[1] = null;
                                    or[1].HP = 2;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException){}
                            input++;
                            break;
                        case 2:
                            try
                            {
                                or[2].HP--;
                                if (or[2].HP == 0)
                                {

                                    cartas.Add(or[2]);
                                    player.organos[2] = null;
                                    or[2].HP = 2;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException){}
                            input++;
                            break;
                        case 3:
                            try
                            {
                                or[3].HP--;
                                if (or[3].HP == 0)
                                {

                                    cartas.Add(or[3]);
                                    player.organos[3] = null;
                                    or[3].HP = 2;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException){}
                            input++;
                            break;
                    }
                }
            }
            else
            {
                foreach (Organos org in enemy.organos)
                {
                    if (org != null && !org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && !org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        org.HP--;
                        if (org.HP == 0)
                        {
                            cartas.Add(org);
                            player.organos[i] = null;
                            org.HP = 2;
                        }
                        cartas.Add(player.cartasmano[id]);
                        player.cartasmano.RemoveAt(id);
                        return true;
                    }

                    if (org != null && org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        return false;
                    }

                    i++;

                }
            }
            return false;
        }
        return false;
    }
}