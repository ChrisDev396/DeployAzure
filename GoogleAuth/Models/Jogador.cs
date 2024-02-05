
using System;
public class Jogador
{
	public Jogador(string usuario, string[] baralho, bool turno)
	{
        this.usuario = usuario;
        this.nome = baralho[0];
        this.baralho = baralho;
        this.turno = turno;

        itemStatus = new List<ItemStatus>
        {
            new ItemStatus(baralho[1])
        };
    }

    public bool turno { get; set; }
    public string usuario { get; set; }
    public string nome { get; set; }
    public string[] baralho { get; set; }

    public bool talento = false;

    public int countJogadas = 2;
    public int jogadas = 1;

    //public string mob = "";

    public List<ItemStatus> itemStatus { get; set; }
}


