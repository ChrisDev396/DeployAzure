using System;
public class CartaItem
{

	public int vida = 0;
	public int forca = 0;
	public string tipo = "passiva";//ativo//passiva-ativo

    public static void Item(string carta, Jogador atacante, Jogador alvo)
	{
		if(carta == "item1")
		{
			atacante.forca += 1;
            atacante.vida += 1;
        }
		else if (carta == "item2")
		{
            alvo.vida -= atacante.forca;
		}
		else if (carta == "item3")
		{
			atacante.countJogadas += 1;
			atacante.itemStatus.Add(new ItemStatus(carta));
		}
	}
}

