using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Autor")]
public class Autor
{
    [Key]
    public int Cod { get; set; }

    [Required]
    [StringLength(40)]
    public required string Nome { get; set; }

    public virtual ICollection<LivroAutor>? LivroAutores { get; set; }
}
