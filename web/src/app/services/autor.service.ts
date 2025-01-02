import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Autor } from '../models/autor';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private apiUrl = `${environment.apiUrl}/autor`;
  constructor() { }

  http = inject(HttpClient);

  getAllAutors() {
    return this.http.get<Autor[]>(this.apiUrl);
  }

  addAutor(data: any) {
    return this.http.post(this.apiUrl, { Nome: data.nome });
  }
  updateAutor(autor: Autor) {
    return this.http.put(`${this.apiUrl}/${autor.cod}`, autor);
  }
  deleteAutor(cod: number) {
    return this.http.delete(`${this.apiUrl}/${cod}`);
  }
}
