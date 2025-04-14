import { Injectable } from '@angular/core';
import { Autor } from '../../../interfaces/cadastro/autor.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AutorService {

  private apiUrl = 'https://localhost:7012/autores';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Autor[]> {
    return this.http.get<Autor[]>(this.apiUrl);
  }

  add(autor: Autor): Observable<Autor> {
    return this.http.post<Autor>(this.apiUrl, autor);
  }  

  update(autor: Autor): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${autor.codAu}`, autor);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
