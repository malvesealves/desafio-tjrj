import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Livro } from '../../../interfaces/cadastro/livro.model';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private serviceUrl = environment.apiUrl + '/livros';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Livro[]> {
    return this.http.get<Livro[]>(this.serviceUrl);
  }

  add(livro: Livro): Observable<Livro> {
    return this.http.post<Livro>(this.serviceUrl, livro);
  }  

  update(livro: Livro): Observable<void> {
    return this.http.put<void>(`${this.serviceUrl}/${livro.codL}`, livro);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.serviceUrl}/${id}`);
  }
}
