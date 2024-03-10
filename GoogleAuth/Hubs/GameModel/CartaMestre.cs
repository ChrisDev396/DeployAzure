using System;

public class CartaMestre
{

    
        public static void PassarTurno(Jogador sender, Jogador receiver)
    {
        if (sender.jogadas == 0)
        {
            sender.turno = !sender.turno;
            receiver.turno = !receiver.turno;
        }
    }

    public static bool Resultado(Jogador jogador1, Jogador jogador2)
    {
        if (jogador1.itemStatus[0].vida <= 0)
        {
            return true;
        }
        else if (jogador2.itemStatus[0].vida <= 0)
        {
            return true;
        }
        return false;
    }
}


