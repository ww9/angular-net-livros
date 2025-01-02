import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RelatorioPorAutorService {
  constructor(private http: HttpClient) { }

  getRelatorioPorAutor() {
    return this.http.get<any[]>(`${environment.apiUrl}/relatorioPorAutor`);
  }
}
