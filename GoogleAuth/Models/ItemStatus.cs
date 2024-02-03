using System;
public class ItemStatus
{
    public ItemStatus(string nome/*, string tipo, int vida, int forca*/)
    {
        this.nome = nome;

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
        else if (nome == "item4")
        {
            tipo = "ativo-passiva";
            forca = 2;
            vida = 2;
        }
        else if (nome == "item5")
        {
            tipo = "passiva";
            forca = 3;
            vida = 2;
        }
    }

	public string nome { get; set; }
    public string tipo { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
}

