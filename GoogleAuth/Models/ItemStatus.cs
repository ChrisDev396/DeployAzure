using System;
public class ItemStatus
{
    public ItemStatus(string nome/*, string tipo, int vida, int forca*/)
    {
        Suporte.GetHeroi(nome);

        if (Suporte.nome is null)
        {
            Suporte.GetGenerica(nome);
        }

        this.nome = Suporte.nome;
        tipo = Suporte.tipo;
        forca = Suporte.forca;
        vida = Suporte.vida;

    }

	public string nome { get; set; }
    public string tipo { get; set; }
    public int forca { get; set; }
    public int vida { get; set; }

    public string ToFormattedString()
    {
        return $"{nome}/{tipo}/{forca}/{vida}";
    }
}

