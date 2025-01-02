import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Livro } from '../models/livro';
import { LivroDto } from '../models/livroDto';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private apiUrl = `${environment.apiUrl}/livro`;
  constructor() { }

  http = inject(HttpClient);

  getAllLivros() {
    return this.http.get<LivroDto[]>(this.apiUrl);
  }
  addLivro(data: LivroDto) {
    return this.http.post<LivroDto>(this.apiUrl, data);
  }
  updateLivro(livro: LivroDto) {
    return this.http.put<LivroDto>(`${this.apiUrl}/${livro.cod}`, livro);
  }
  deleteLivro(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
