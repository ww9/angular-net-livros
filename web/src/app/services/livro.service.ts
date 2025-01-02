import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Livro } from '../models/livro';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = `${environment.apiUrl}/livro`;
  constructor() { }

  http = inject(HttpClient);

  getAllLivros() {
    return this.http.get<Livro[]>(this.apiUrl);
  }

  addLivro(data: any) {
    return this.http.post(this.apiUrl, data);
  }
  updateLivro(livro: Livro) {
    return this.http.put(`${this.apiUrl}/${livro.cod}`, livro);
  }
  deleteLivro(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
