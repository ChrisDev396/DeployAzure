using System;
public class ItemStatus
{
    public ItemStatus(string nome, string tipo, int vida, int forca)
    {
        this.nome = nome;
        this.tipo = tipo;
        this.vida = vida;
        this.forca = forca;
    }

	public string nome { get; set; }
    public string tipo { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
}

