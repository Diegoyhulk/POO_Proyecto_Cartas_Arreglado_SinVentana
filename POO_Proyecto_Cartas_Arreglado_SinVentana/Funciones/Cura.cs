namespace POO_Proyecto_Cartas_Arreglado_SinVentana.Funciones;

using static System.Console;
public class Cura: ICurable
{
    public bool Curar(Jugador player, List<Cartas> cartas, int id)
    {
        if (player is Player)
        {
            int i = 0;
            if (player.cartasmano[id].Tipo == Organos.Type.Comodín)
            {
                Organos[] or = new Organos[5];
                int j = 0;
                WriteLine("Elige que organo quieres curar o inmunizar");
                foreach (Organos org in player.organos)
                {
                    if (org != null)
                    {
                        or[j] = org;
                        WriteLine($"|({j + 1}){org.Nombre} {org.Tipo} ");
                        if (org.HP == 2)
                            WriteLine($"sin ninguna bacteria|");
                        else if (org.HP < 2)
                            WriteLine($"con una bacteria|");
                        else if (org.HP == 4)
                            WriteLine($"inmunizado, no puedes infectar|");
                        else if (org.HP > 2)
                            WriteLine($"con un antibiótico|");
                    }

                    j++;
                }

                ConsoleKey input = ReadKey(true).Key;
                switch (input){
                    
                    case ConsoleKey.D1:
                        try
                        {
                            or[0].HP++;
                            if (or[0].HP == 4)
                            {
                                or[0].inmunizado = true;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                            WriteLine("No hay organo quieres inmunizar");
                        }
                        break;
                    case ConsoleKey.D2:
                        try
                        {
                            or[1].HP++;
                            if (or[1].HP == 4)
                            {
                                or[1].inmunizado = true;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                            WriteLine("No hay organo quieres inmunizar");
                        }
                        break;
                    case ConsoleKey.D3:
                        try
                        {
                            or[2].HP++;
                            if (or[2].HP == 4)
                            {
                                or[2].inmunizado = true;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                            WriteLine("No hay organo quieres inmunizar");
                        }
                        break;
                    case ConsoleKey.D4:
                        try
                        {
                            or[3].HP++;
                            if (or[3].HP == 4)
                            {
                                or[3].inmunizado = true;
                            }

                            cartas.Add(player.cartasmano[id]);
                            player.cartasmano.RemoveAt(id);
                            return true;
                        }
                        catch (NullReferenceException)
                        {
                            WriteLine("No hay organo quieres inmunizar");
                        }
                        break;
                    default:
                        WriteLine("Input no valido");
                        ReadLine();
                        return false;
                
                }
            }
            else
            {
                foreach (Organos org in player.organos)
                {
                    if (org != null && !org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && !org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        org.HP++;
                        if (org.HP == 4)
                        {
                            org.inmunizado = true;
                        }

                        cartas.Add(player.cartasmano[id]);
                        player.cartasmano.RemoveAt(id);
                        return true;
                    }

                    if (org != null && org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        WriteLine($"| El Organo {player.cartasmano[i].Tipo} ya esta inmunizado!");
                        ReadLine();
                        return false;
                    }

                    i++;
                }
                WriteLine("No hay organo que curar");
                ReadLine();
                return false;
            }
        }
        else if (player is Enemy)
        {
            int i = 0;
            if (player.cartasmano[id].Tipo == Organos.Type.Comodín)
            {
                Organos[] or = new Organos[5];
                int j = 0;
                foreach (Organos org in player.organos)
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
                                or[0].HP++;
                                if (or[0].HP == 4)
                                {
                                    or[0].inmunizado = true;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException)
                            {
                            }
                            input++;
                            break;
                        case 1:
                            try
                            {
                                or[1].HP++;
                                if (or[1].HP == 4)
                                {
                                    or[1].inmunizado = true;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException)
                            {
                            }
                            input++;
                            break;
                        case 2:
                            try
                            {
                                or[2].HP++;
                                if (or[2].HP == 4)
                                {
                                    or[2].inmunizado = true;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException)
                            {
                            }
                            input++;
                            break;
                        case 3:
                            try
                            {
                                or[3].HP++;
                                if (or[3].HP == 4)
                                {
                                    or[3].inmunizado = true;
                                }

                                cartas.Add(player.cartasmano[id]);
                                player.cartasmano.RemoveAt(id);
                                return true;
                            }
                            catch (NullReferenceException)
                            {
                            }
                            input++;
                            break;
                    }
                }
            }
            else
            {
                foreach (Organos org in player.organos)
                {
                    if (org != null && !org.inmunizado && player.cartasmano[id].Tipo == org.Tipo||
                        org != null && !org.inmunizado && player.cartasmano[id].Tipo == Organos.Type.Comodín)
                    {
                        org.HP++;
                        if (org.HP == 4)
                        {
                            org.inmunizado = true;
                        }
                        cartas.Add(player.cartasmano[id]);
                        player.cartasmano.RemoveAt(id);
                        return true;
                    }
                    if (org != null && org.inmunizado && player.cartasmano[id].Tipo == org.Tipo)
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