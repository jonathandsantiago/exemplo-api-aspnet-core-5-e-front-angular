import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ProdutoComponent} from './produto.component';
import {ProdutoEdicaoComponent} from './produto-edicao/produto-edicao.component';
import {ProdutoCadastroComponent} from './produto-cadastro/produto-cadastro.component';

export const routes: Routes = [
  {path: '', component: ProdutoComponent},
  {path: 'cadastrar', component: ProdutoCadastroComponent},
  {path: ':id/editar', component: ProdutoEdicaoComponent},
  {path: ':id/visualizar', data: {isVisualizacao: 'true'}, component: ProdutoEdicaoComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutoRoutingModule {

}
