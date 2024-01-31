using System;
public class Generica
{
	public static void usarGenerica(string carta, Jogador sender, Jogador receiver)
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

