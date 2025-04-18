import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RelatorioService {

  private serviceUrl = environment.apiUrl + '/relatorios';

  constructor(private http: HttpClient) {}

  getReport(): Observable<Blob> {
    return this.http.get(this.serviceUrl, {
      responseType: 'blob'
    });
  }
}
