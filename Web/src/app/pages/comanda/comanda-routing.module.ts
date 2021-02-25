import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ComandaEdicaoComponent} from './comanda-edicao/comanda-edicao.component';
import {ComandaCadastroComponent} from './comanda-cadastro/comanda-cadastro.component';
import {ComandaComponent} from './comanda.component';

export const routes: Routes = [
  {path: '', component: ComandaComponent},
  {path: 'cadastrar', component: ComandaCadastroComponent},
  {path: ':id/editar', component: ComandaEdicaoComponent},
  {path: ':id/visualizar', data: {isVisualizacao: 'true'}, component: ComandaEdicaoComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComandaRoutingModule {

}
