
using System;
public class Jogador
{
	public Jogador(string usuario, string[] baralho, bool turno)
	{
        this.usuario = usuario;
        nome = baralho[0];
        this.baralho = baralho;
        this.turno = turno;
        this.heroi = baralho[1];
        CartaMestre.GetHeroi(heroi,vida,forca);
    }

    public bool turno { get; set; }
    public string usuario { get; set; }
    public string nome { get; set; }
    public string[] baralho { get; set; }

    public string heroi { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
    public bool talento = false;
    public int countJogadas = 1;
    public int jogadas = 1;
    //public string mob = "";
    //public List<CartaItem> itemAtivo;



}


