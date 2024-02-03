using System;
public class CartaTalento
{
	public static void Talento(string carta, Jogador atacante, Jogador alvo)
	{
        if (atacante.itemStatus[0].nome == "brutaniuz" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {

        }
        else if (atacante.itemStatus[0].nome == "tecnita" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {

        }
        else if (atacante.itemStatus[0].nome == "ariedam" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {

        }
        else if (atacante.itemStatus[0].nome == "menrart" && Array.Exists(atacante.baralho, elemento => elemento == carta))
        {
        }
    }
}

