using System;
public class CartaItem
{
	public static void Item(string carta, Jogador sender, Jogador receiver)
	{
		if(carta == "item1")
		{
			sender.forca += 1;
            sender.vida += 1;
        }
		else if (carta == "item2")
		{
			receiver.vida -= sender.forca;
		}
	}
}

