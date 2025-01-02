import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LivroComponent } from './components/livro/livro.component';
import { AssuntoComponent } from './components/assunto/assunto.component';
import { AutorComponent } from './components/autor/autor.component';
import { FormaComponent } from './components/forma/forma.component';
import { RelatorioPorAutorComponent } from './components/relatorio-por-autor/relatorio-por-autor.component';

export const routes: Routes = [
  {
    path: "", component: HomeComponent
  },
  {
    path: "livro", component: LivroComponent
  },
  {
    path: "assunto", component: AssuntoComponent
  },
  {
    path: "autor", component: AutorComponent
  },
  {
    path: "forma", component: FormaComponent
  },
  {
    path: "relatorio_por_autor", component: RelatorioPorAutorComponent
  },
  // Rotas não encontradas vão para home
  {
    path: "**", redirectTo: ""
  },

];
