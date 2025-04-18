import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/base/home/home.component';
import { LivroComponent } from './modules/cadastro/livro/livro.component';
import { AutorComponent } from './modules/cadastro/autor/autor.component';
import { AssuntoComponent } from './modules/cadastro/assunto/assunto.component';
import { RelatorioComponent } from './modules/relatorio/relatorio.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'assunto', component: AssuntoComponent },
    { path: 'autor', component: AutorComponent },
    { path: 'livro', component: LivroComponent },
    { path: 'relatorio', component: RelatorioComponent },
];

@NgModule({    
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
  })
  export class AppRoutesModule {}