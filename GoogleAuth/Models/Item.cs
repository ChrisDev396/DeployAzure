using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public int ItemId { get; set; }

    public string? ShildNome { get; set; }

    public int Modelo { get; set; }//Desgaste/Modelo/Padrao

    public string? Email { get; set; }
}

