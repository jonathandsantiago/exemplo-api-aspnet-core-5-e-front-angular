import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {UsuarioComponent} from './usuario.component';
import {UsuarioCadastroComponent} from './usuario-cadastro/usuario-cadastro.component';
import {UsuarioEdicaoComponent} from './usuario-edicao/usuario-edicao.component';

export const routes: Routes = [
  {path: '', component: UsuarioComponent},
  {path: 'cadastrar', component: UsuarioCadastroComponent},
  {path: ':id/editar', component: UsuarioEdicaoComponent},
  {path: ':id/visualizar', data: {isVisualizacao: 'true'}, component: UsuarioEdicaoComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule {

}
