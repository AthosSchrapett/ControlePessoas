import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { PessoaCreateDTO } from '../models/pessoa-create.dto';
import { PessoaGetAllDTO } from '../models/pessoa-get-all.dto';
import { PessoaGetDTO } from '../models/pessoa-get.dto';
import { PessoaUpdateDTO } from '../models/pessoa-update.dto';

@Injectable({
  providedIn: 'root'
})
export class PessoaService {

  private apiUrl = `${environment.apiUrl}/pessoa`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<PessoaGetAllDTO[]> {
    return this.http.get<PessoaGetAllDTO[]>(this.apiUrl);
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