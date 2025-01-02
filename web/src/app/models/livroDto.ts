export interface LivroDto {
  cod: number;
  titulo: string;
  editora: string;
  edicao: number | null;
  anoPublicacao: number;
  assuntoCods: number[];
  autorCods: number[];
  formaCompraVals?: Array<{
    formaCompraCod: number;
    valor: number;
  }>;
}
