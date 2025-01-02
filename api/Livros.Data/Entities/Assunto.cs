using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Assunto")]
public class Assunto
{
    [Key]
    public int Cod { get; set; }  // Primary Key

    [Required]
    [StringLength(20)]
    public required string Descricao { get; set; }

    // Navigation properties
    public virtual ICollection<LivroAssunto>? LivroAssuntos { get; set; }
}
