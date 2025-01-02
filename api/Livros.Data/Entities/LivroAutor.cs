using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Livro_Autor")]
public class LivroAutor
{
    [Key]
    public int Cod { get; set; }

    [ForeignKey(nameof(Livro))]
    public required int LivroCod { get; set; }
    public virtual Livro Livro { get; set; }

    [ForeignKey(nameof(Autor))]
    public required int AutorCod { get; set; }
    public virtual Autor Autor { get; set; }
}
