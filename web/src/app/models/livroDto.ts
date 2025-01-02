export interface LivroDto {
  cod: number;
  titulo: string;
  editora: string;
  edicao: number | null;
  anoPublicacao: number;
  assuntoCods: number[];
}
