import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {HomeComponent} from './home.component';
import {ComandaEditComponent} from './comanda/comanda-edit/comanda-edit.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: 'cadastrar',
    component: ComandaEditComponent,
  },
  {
    path: ':comandaId',
    children: [
      {
        path: 'alterar',
        children: [{path: '', component: ComandaEditComponent}]
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule {
}
