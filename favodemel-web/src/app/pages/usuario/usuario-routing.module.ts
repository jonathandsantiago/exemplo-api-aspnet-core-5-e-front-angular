import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {UsuarioComponent} from './usuario.component';
import {UsuarioCrudComponent} from './usuario-crud/usuario-crud.component';

export const routes: Routes = [
  {path: '', component: UsuarioComponent},
  {path: 'cadastrar', component: UsuarioCrudComponent},
  {path: ':id/editar', component: UsuarioCrudComponent},
  {path: ':id/visualizar', component: UsuarioCrudComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule {

}
