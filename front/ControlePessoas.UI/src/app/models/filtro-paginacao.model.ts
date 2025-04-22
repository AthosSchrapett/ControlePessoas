import { FiltroPessoasEnum } from '../enums/filtro-pessoas.enum';

export interface FiltroPaginacao {
  pagina: number;
  itensPorPagina: number;
  filtroPessoas: FiltroPessoasEnum;
} 