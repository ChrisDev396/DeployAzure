using System;

public class CartaMestre
{
    public static void passarTurno(Jogador sender, Jogador receiver)
    {
        if (sender.jogadas == 0)
        {
            sender.turno = !sender.turno;
            receiver.turno = !receiver.turno;
        }
    }


}


