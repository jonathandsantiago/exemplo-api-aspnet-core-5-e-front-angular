import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ProdutoComponent} from './produto.component';
import {ProdutoCrudComponent} from './produto-crud/produto-crud.component';

export const routes: Routes = [
  {path: '', component: ProdutoComponent},
  {path: 'cadastrar', component: ProdutoCrudComponent},
  {path: ':id/editar', component: ProdutoCrudComponent},
  {path: ':id/visualizar', component: ProdutoCrudComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutoRoutingModule {

}
