using System;
public class CartaPassiva
{
	public static void Passiva(string carta, Jogador atacante, Jogador alvo)
	{
        if (carta == "brutaniuz" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {
           
        }
        else if (carta == "tecnita" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {
           
        }
        else if (carta == "ariedam" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {

        }
        else if(carta == "menrart" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {
        }
    }
}

