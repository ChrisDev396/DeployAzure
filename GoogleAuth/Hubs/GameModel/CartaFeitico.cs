using System;
public class CartaFeitico
{
	public static void Feitico(string carta, Jogador atacante, Jogador alvo)
	{
		if(carta == "feitico1")
		{
            atacante.itemStatus[0].forca += 1;
            atacante.itemStatus[0].vida += 1;
        }
		else if (carta == "feitico2")
		{
			alvo.itemStatus[0].vida -= atacante.itemStatus[0].forca;
		}
	}
}

