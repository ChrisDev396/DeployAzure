using System;

public class CartaMestre
{

    public static void GetHeroi(string heroi,int vida, int forca)
    {

        if (heroi == "brutaniuz")
        {
            vida = 15;
            forca = 3;
        }
        else if (heroi == "tecnita")
        {
            vida = 14;
            forca = 2;
        }
        else if (heroi == "ariedam")
        {
            vida = 16;
            forca = 1;
        }
        else if (heroi == "menrart")
        {
            vida = 14;
            forca = 3;
        }
    }
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
        if (jogador1.vida <= 0)
        {
            return true;
        }
        else if (jogador2.vida <= 0)
        {
            return true;
        }
        return false;
    }
}


