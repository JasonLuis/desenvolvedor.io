using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCore.Domain;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Column("Phone")]
    public string Telefone { get; set; }
    public string Cep { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
}
