using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    public Usuario()
    {
        Itens = new Collection<Item>();
    }

    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    //[Required]
    public int UsuarioId { get; set; }

    //[Required]
    [StringLength(50)]
    public string? Email { get; set; }

    [Required]
    [StringLength(10)]
    public string? Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    //public int Moedas { get; set; }

    public ICollection<Item>? Itens { get; set; }
}


