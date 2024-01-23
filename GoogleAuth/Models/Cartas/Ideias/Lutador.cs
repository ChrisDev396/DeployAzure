public class Lutador : Carta
{
    public Lutador(string nome, int vida, int forca) : base(nome)
    {
        this.vida = vida;
        this.forca = forca;
    }

    public int vida { get; set; }
    public int forca { get; set; }

    public void atacar(Lutador oponente)
    {
        oponente.vida -= forca;
    }

    public override void passiva(List<Lutador> lutadores)
    {
        
    }
}


