using System;
public class CartaItem
{
    public static void Item(string carta, Jogador atacante, Jogador alvo)
	{
		if(carta == "item1")
		{
			atacante.itemStatus[0].forca += 1;
            atacante.itemStatus[0].vida += 1;
        }
		else if (carta == "item2")
		{
			alvo.itemStatus[0].vida -= atacante.itemStatus[0].forca;
		}
		else if (carta == "item3")
		{
			//atacante.countJogadas += 1;
			atacante.itemStatus.Add(new ItemStatus(carta));
		}
	}
}

