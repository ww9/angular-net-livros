namespace Livros.Application.Dtos;
public class LivroDto
{
	public int Cod { get; set; }
	public string Titulo { get; set; } = "";
	public string Editora { get; set; } = "";
	public int? Edicao { get; set; }
	public int AnoPublicacao { get; set; }
	public List<int> AssuntoCods { get; set; } = new();
}