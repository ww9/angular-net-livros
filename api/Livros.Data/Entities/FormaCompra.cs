using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("FormaCompra")]
public class FormaCompra
{
    [Key]
    public int Cod { get; set; }

    [Required]
    [StringLength(40)]
    public required string Descricao { get; set; }

    public virtual ICollection<LivroFormaCompra>? LivroFormaCompras { get; set; }
}
