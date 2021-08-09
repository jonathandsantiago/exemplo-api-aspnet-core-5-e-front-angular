import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {UsuarioComponent} from './usuario.component';
import {UsuarioEditComponent} from './usuario-edit/usuario-edit.component';

export const routes: Routes = [
  {path: '', component: UsuarioComponent},
  {path: 'cadastrar', component: UsuarioEditComponent},
  {path: ':id/editar', component: UsuarioEditComponent},
  {path: ':id/visualizar', component: UsuarioEditComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule {

}
