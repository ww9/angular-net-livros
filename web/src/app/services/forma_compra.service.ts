import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { FormaCompra } from '../models/forma_compra';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormaCompraService {
  private apiUrl = `${environment.apiUrl}/formacompra`;
  constructor() { }

  http = inject(HttpClient);

  getAllFormaCompras() {
    return this.http.get<FormaCompra[]>(this.apiUrl);
  }

  addFormaCompra(data: any) {
    return this.http.post(this.apiUrl, { Descricao: data.descricao });
  }
  updateFormaCompra(formacompra: FormaCompra) {
    return this.http.put(`${this.apiUrl}/${formacompra.cod}`, formacompra);
  }
  deleteFormaCompra(cod: number) {
    return this.http.delete(`${this.apiUrl}/${cod}`);
  }
}
