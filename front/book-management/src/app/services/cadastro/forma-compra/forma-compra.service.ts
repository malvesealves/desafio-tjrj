import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormaCompra } from '../../../interfaces/cadastro/forma-compra.model';
import { environment } from '../../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class FormaCompraService {

  private serviceUrl = environment.apiUrl + '/formas-compra';

  constructor(private http: HttpClient) {}

  getAll(): Observable<FormaCompra[]> {
    return this.http.get<FormaCompra[]>(this.serviceUrl);
  }
}
