import { Component, OnInit } from '@angular/core';
import { RelatorioPorAutorService } from '../../services/relatorio-por-autor.service';
import { CommonModule, NgFor } from '@angular/common';

@Component({
  selector: 'app-relatorio-por-autor',
  templateUrl: './relatorio-por-autor.component.html',
  styleUrls: ['./relatorio-por-autor.component.css'],
  imports: [CommonModule],
})
export class RelatorioPorAutorComponent implements OnInit {
  relatorio: any[] = [];
  groupedRelatorio: any[] = [];

  constructor(private relatorioService: RelatorioPorAutorService) { }

  ngOnInit() {
    this.relatorioService.getRelatorioPorAutor()
      .subscribe(data => {
        this.relatorio = data;
        const map = new Map<string, any[]>();
        for (const item of this.relatorio) {
          if (!map.has(item.autorNome)) {
            map.set(item.autorNome, []);
          }
          map.get(item.autorNome)?.push(item);
        }
        this.groupedRelatorio = [];
        map.forEach((items, autorNome) => {
          this.groupedRelatorio.push({ autorNome, items });
        });
      });
  }

  public printPage(): void {
    window.print();
  }
}
