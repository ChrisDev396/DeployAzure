using System;
public class CartaItemAtivo
{
    public CartaItemAtivo(string nome, int vida, int forca)
    {
        this.nome = nome;
        this.vida = vida;
        this.forca = forca;
    }

	public string nome { get; set; }
    public int vida { get; set; }
    public int forca { get; set; }
}

