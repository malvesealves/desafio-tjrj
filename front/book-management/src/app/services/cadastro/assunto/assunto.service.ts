import { Injectable } from '@angular/core';
import { Assunto } from '../../../interfaces/cadastro/assunto.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {

  private apiUrl = 'https://localhost:7012/assuntos';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(this.apiUrl);
  }

  add(assunto: Assunto): Observable<Assunto> {
    return this.http.post<Assunto>(this.apiUrl, assunto);
  }  

  update(assunto: Assunto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${assunto.codAs}`, assunto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
