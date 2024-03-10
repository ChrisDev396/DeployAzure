using System.ComponentModel.DataAnnotations;

public class Transacao
{

    [Key]
    public int TransacaoId { get; set; }

    [Required]
    public int QuantidadeMoedas { get; set; }

    [Required]
    [StringLength(50)]
    public string? NomeTitular { get; set; }

    public string? Email { get; set; }
}

