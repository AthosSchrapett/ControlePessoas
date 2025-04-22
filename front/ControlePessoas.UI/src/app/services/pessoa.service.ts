import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { PessoaCreateDTO } from '../models/pessoa-create.dto';
import { PessoaGetAllDTO } from '../models/pessoa-get-all.dto';
import { PessoaGetDTO } from '../models/pessoa-get.dto';
import { PessoaUpdateDTO } from '../models/pessoa-update.dto';
import { FiltroPaginacao } from '../models/filtro-paginacao.model';
import { ResultadoPaginacao } from '../models/resultado-paginacao.model';

@Injectable({
  providedIn: 'root'
})
export class PessoaService {

  private apiUrl = `${environment.apiUrl}/pessoa`;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  });

  constructor(private http: HttpClient) { }

  getAll(filtro: FiltroPaginacao): Observable<ResultadoPaginacao<PessoaGetAllDTO>> {
    const params = new HttpParams()
      .set('pagina', filtro.pagina.toString())
      .set('itensPorPagina', filtro.itensPorPagina.toString())
      .set('filtroPessoas', filtro.filtroPessoas.toString());

    return this.http.get<ResultadoPaginacao<PessoaGetAllDTO>>(`${this.apiUrl}/paginacao`, { 
      params,
      headers: this.headers
    });
  }

  getById(id: string): Observable<PessoaGetDTO> {
    return this.http.get<PessoaGetDTO>(`${this.apiUrl}/${id}`);
  }

  create(pessoa: PessoaCreateDTO): Observable<PessoaGetDTO> {
    return this.http.post<PessoaGetDTO>(this.apiUrl, pessoa);
  }

  update(pessoa: PessoaUpdateDTO): Observable<PessoaGetDTO> {
    return this.http.put<PessoaGetDTO>(this.apiUrl, pessoa);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
} 