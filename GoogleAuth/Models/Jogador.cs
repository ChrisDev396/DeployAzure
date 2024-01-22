
using System;
public class Jogador
{
	public Jogador(string _nomeJogador, string[] _baralho, bool _turno)
	{
        nomeJogador = _nomeJogador;
        baralho = _baralho;
        turno = _turno;
        talento = false;
        //vida = getHeroiVida();
	}

	public string nomeJogador { get; set; }
    public string[] baralho { get; set; }
    public bool turno { get; set; }
    public bool talento { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }


    public void desembaralhar()
    { 
        //if (carta == baralho[0])
        //{
            
        //}
        foreach (string carta in baralho)
        {
            if (carta == "brutaniuz")
            {
                nomeJogador = "brutaniuz";
                vida = 15;
                forca = 3;
            }
            else if (carta == "tecnita")
            {
                nomeJogador = "tecnita";
                vida = 14;
                forca = 2;
            }
            else if (carta == "ariedam")
            {
                nomeJogador = "ariedam";
                vida = 16;
                forca = 1;
            }
            else
            {
                nomeJogador = "menrart";
                vida = 14;
                forca = 3;
            }
        }
    }

}


