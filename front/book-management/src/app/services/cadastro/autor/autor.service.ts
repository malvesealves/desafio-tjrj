import { Injectable } from '@angular/core';
import { Autor } from '../../../interfaces/cadastro/autor.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class AutorService {

  private serviceUrl = environment.apiUrl + '/autores';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Autor[]> {
    return this.http.get<Autor[]>(this.serviceUrl);
  }

  add(autor: Autor): Observable<Autor> {
    return this.http.post<Autor>(this.serviceUrl, autor);
  }  

  update(autor: Autor): Observable<void> {
    return this.http.put<void>(`${this.serviceUrl}/${autor.codAu}`, autor);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.serviceUrl}/${id}`);
  }
}
