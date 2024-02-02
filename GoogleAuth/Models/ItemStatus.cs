using System;
public class ItemStatus
{
    public ItemStatus(string nome/*, string tipo, int vida, int forca*/)
    {
        this.nome = nome;
        //this.tipo = tipo;
        //this.vida = vida;
        //this.forca = forca;
        if (nome == "item1")
        {
            tipo = "ativo";
            forca = 1;
            vida = 4;
        }
        else if (nome == "item2")
        {
            tipo = "ativo";
            forca = 2;
            vida = 3;
        }
        else if (nome == "item3")
        {
            tipo = "ativo-passiva";
            forca = 1;
            vida = 2;
        }
    }

	public string nome { get; set; }
    public string tipo { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
}

