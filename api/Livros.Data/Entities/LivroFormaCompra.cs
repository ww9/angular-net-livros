using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livros.Data.Entities;

[Table("Livro_FormaCompra")]
public class LivroFormaCompra
{
    [Key]
    public int Cod { get; set; }

    [Required]
    [Range(0, 9999)]
    public double Valor { get; set; }

    [ForeignKey(nameof(Livro))]
    public required int LivroCod { get; set; }
    public virtual Livro Livro { get; set; }

    [ForeignKey(nameof(FormaCompra))]
    public required int FormaCompraCod { get; set; }
    public virtual FormaCompra FormaCompra { get; set; }
}
