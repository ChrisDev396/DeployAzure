using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    [StringLength(10)]
    public string? NomeUsuario { get; set; }

    public string? Email { get; set; }

    public DateTime DataCriacao { get; set; }

    public int Partidas { get; set; }
    public int Vitorias { get; set; }
    public int Pontuacao { get; set; }
}

//public Usuario()
//{
//    Itens = new Collection<Item>();
//    Transacoes = new Collection<Transacao>();
//}

//public ICollection<Item>? Itens { get; set; }
//public ICollection<Transacao>? Transacoes { get; set; }


