import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ComandaCrudComponent} from './comanda-crud/comanda-crud.component';
import {ComandaComponent} from './comanda.component';

export const routes: Routes = [
  {path: '', component: ComandaComponent},
  {path: 'cadastrar', component: ComandaCrudComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComandaRoutingModule {

}
