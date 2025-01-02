using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Livro_Assunto")]
public class LivroAssunto
{
    [Key]
    public int Cod { get; set; }

    [ForeignKey(nameof(Livro))]
    public required int LivroCod { get; set; }
    public virtual Livro Livro { get; set; }

    [ForeignKey(nameof(Assunto))]
    public required int AssuntoCod { get; set; }
    public virtual Assunto Assunto { get; set; }
}
