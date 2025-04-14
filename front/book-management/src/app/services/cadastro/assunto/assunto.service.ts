import { Injectable } from '@angular/core';
import { Assunto } from '../../../interfaces/cadastro/assunto.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {

  private serviceUrl = environment.apiUrl + '/assuntos';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(this.serviceUrl);
  }

  add(assunto: Assunto): Observable<Assunto> {
    return this.http.post<Assunto>(this.serviceUrl, assunto);
  }  

  update(assunto: Assunto): Observable<void> {
    return this.http.put<void>(`${this.serviceUrl}/${assunto.codAs}`, assunto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.serviceUrl}/${id}`);
  }
}
