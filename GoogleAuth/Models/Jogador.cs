
using System;
public class Jogador
{
	public Jogador(string nome, string[] baralho, bool turno,string sala)
	{
        this.nome = nome;
        this.baralho = baralho;
        this.turno = turno;
        this.sala = sala;
    }

    public bool turno { get; set; }
    public string nome { get; set; }
    public string sala { get; set; }
    public string[] baralho { get; set; }
    
    public int vida { get; set; }
    public int forca { get; set; }
    public bool talento = false;

    public void getHeroi(string carta)
    {
        if (carta == "brutaniuz")
        {
            vida = 15;
            forca = 3;
        }
        else if (carta == "tecnita")
        {
            vida = 14;
            forca = 2;
        }
        else if (carta == "ariedam")
        {
            vida = 16;
            forca = 1;
        }
        else
        {
            vida = 14;
            forca = 3;
        }
    }

    public void usarPassiva(int passiva)
    {
        if (nome == "brutaniuz")
        {
            if (passiva == 1)
            {

            }
            else if (passiva == 2)
            {

            }
        }
        else if (nome == "tecnita")
        {

        }
        else if (nome == "ariedam")
        {

        }
        else if (nome == "menrart")
        {

        }
    }

    public void usarTalento(int talento)
    {
        if (nome == "brutaniuz")
        {
           if (talento == 1)
           {

           }
           else if (talento == 2)
           {  

           }
        }
        else if (nome == "tecnita")
        {
            
        }
        else if (nome == "ariedam")
        {
            
        }
        else if (nome == "menrart")
        {
           
        }
    }

}


