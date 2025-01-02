import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Assunto } from '../models/assunto';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {
  private apiUrl = 'http://localhost:5225/assunto'
  constructor() { }

  http = inject(HttpClient)

  getAllAssuntos() {
    return this.http.get<Assunto[]>(this.apiUrl);
  }

  addAssunto(data: any) {
    return this.http.post(this.apiUrl, { Descricao: data.descricao });
  }
  updateAssunto(assunto: Assunto) {
    return this.http.put(`${this.apiUrl}/${assunto.cod}`, assunto)
  }
  deleteAssunto(cod: number) {
    return this.http.delete(`${this.apiUrl}/${cod}`);
  }
}
