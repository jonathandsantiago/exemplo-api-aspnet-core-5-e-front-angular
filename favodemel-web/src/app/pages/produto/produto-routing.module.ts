import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ProdutoComponent} from './produto.component';
import {ProdutoEditComponent} from './produto-edit/produto-edit.component';

export const routes: Routes = [
  {path: '', component: ProdutoComponent},
  {path: 'cadastrar', component: ProdutoEditComponent},
  {path: ':id/editar', component: ProdutoEditComponent},
  {path: ':id/visualizar', component: ProdutoEditComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutoRoutingModule {

}
