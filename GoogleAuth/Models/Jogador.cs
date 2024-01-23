
using System;
public class Jogador
{
	public Jogador(string nome, string[] baralho, bool turno,string sala)
	{
        this.nome = nome;
        this.baralho = baralho;
        this.turno = turno;
        getHeroi(baralho[0]);
    }

    //public string sala { get; set; }
    public bool turno { get; set; }
    public string nome { get; set; }
    public string[] baralho { get; set; }

    public string heroi { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
    public bool talento = false;

    public void getHeroi(string heroi)
    {
        this.heroi = heroi;

        if (this.heroi == "brutaniuz")
        {
            
            vida = 15;
            forca = 3;
        }
        else if (this.heroi == "tecnita")
        {
            vida = 14;
            forca = 2;
        }
        else if (this.heroi == "ariedam")
        {
            vida = 16;
            forca = 1;
        }
        else if (this.heroi == "menrart")
        {
            vida = 14;
            forca = 3;
        }
    }

    public void usarPassiva(int passiva)
    {
        if (heroi == "brutaniuz")
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
            if (passiva == 1)
            {

            }
            else if (passiva == 2)
            {

            }
        }
        else if (nome == "ariedam")
        {
            if (passiva == 1)
            {

            }
            else if (passiva == 2)
            {

            }
        }
        else if (nome == "menrart")
        {
            if (passiva == 1)
            {

            }
            else if (passiva == 2)
            {

            }
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


