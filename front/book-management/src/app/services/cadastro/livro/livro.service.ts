import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Livro } from '../../../interfaces/cadastro/livro.model';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private apiUrl = 'https://localhost:7012/livros';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Livro[]> {
    return this.http.get<Livro[]>(this.apiUrl);
  }

  add(livro: Livro): Observable<Livro> {
    return this.http.post<Livro>(this.apiUrl, livro);
  }  

  update(livro: Livro): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${livro.codL}`, livro);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
