import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Livro } from '../models/livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = 'http://localhost:5225/livro'
  constructor() { }

  http = inject(HttpClient)

  getAllLivros() {
    return this.http.get<Livro[]>(this.apiUrl);
  }

  addLivro(data: any) {
    return this.http.post(this.apiUrl, data);
  }
  updateLivro(livro: Livro) {
    return this.http.put(`${this.apiUrl}/${livro.id}`, livro)
  }
  deleteLivro(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
