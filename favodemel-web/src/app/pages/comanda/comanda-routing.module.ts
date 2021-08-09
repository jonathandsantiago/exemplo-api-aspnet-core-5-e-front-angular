import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ComandaEditComponent} from './comanda-edit/comanda-edit.component';
import {ComandaComponent} from './comanda.component';

export const routes: Routes = [
  {path: '', component: ComandaComponent},
  {path: 'cadastrar', component: ComandaEditComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComandaRoutingModule {

}
