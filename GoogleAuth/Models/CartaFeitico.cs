using System;
public class CartaFeitico
{
	public static void Feitico(string carta, Jogador atacante, Jogador alvo)
	{
		if(carta == "feitico1")
		{
            atacante.forca += 1;
            atacante.vida += 1;
        }
		else if (carta == "feitico2")
		{
			alvo.vida -= atacante.forca;
		}
	}
}

