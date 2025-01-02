using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Livro")]
public class Livro
{
    [Key]
    public int Cod { get; set; }

    [Required]
    [StringLength(40)]
    public required string Titulo { get; set; }

    [StringLength(40)]
    public required string Editora { get; set; }

    public int? Edicao { get; set; }

    [StringLength(4)]
    public required int AnoPublicacao { get; set; }

    public virtual ICollection<LivroAutor>? LivroAutores { get; set; }
    public virtual ICollection<LivroAssunto>? LivroAssuntos { get; set; }
}

