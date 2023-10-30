using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Item
{
	[Key]
	public int ItemId { get; set; }

	[Required]
	[StringLength(50)]
	public string? Nome { get; set; }

    [Required]
    [Column(TypeName ="decimal(10,2)")]
	public decimal Preco { get; set; }
    //public string? ImagemUrl { get; set; }

    public int UsuarioId { get; set; }
	public Usuario? Usuario { get; set; }
}

