
using System;
public class Jogador
{
	public Jogador(string usuario, string[] baralho, bool turno)
	{
        this.usuario = usuario;
        this.nome = baralho[0];
        this.baralho = baralho;
        this.turno = turno;
        this.heroi = baralho[1];

        GetHeroi(heroi);

        for (int i = 2; i < 7; i++)
        {
            itemStatus.Add(new ItemStatus(baralho[i]));
        }
        
    }

    public bool turno { get; set; }
    public string usuario { get; set; }
    public string nome { get; set; }
    public string[] baralho { get; set; }

    public string heroi { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
    public bool talento = false;

    public int countJogadas = 2;
    public int jogadas = 1;

    //public string mob = "";

    public List<ItemStatus> itemStatus {get;set;}

    public void GetHeroi(string heroi)
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
}


