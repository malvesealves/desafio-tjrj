import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormaCompra } from '../../../interfaces/cadastro/forma-compra.model';

@Injectable({
  providedIn: 'root'
})
export class FormaCompraService {

  private apiUrl = 'https://localhost:7012/formas-compra';

  constructor(private http: HttpClient) {}

  getAll(): Observable<FormaCompra[]> {
    return this.http.get<FormaCompra[]>(this.apiUrl);
  }
}
