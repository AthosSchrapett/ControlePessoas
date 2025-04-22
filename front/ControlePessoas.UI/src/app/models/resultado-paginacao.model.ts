export interface ResultadoPaginacao<T> {
  itens: T[];
  totalRegistros: number;
  paginaAtual: number;
  totalPaginas: number;
} 