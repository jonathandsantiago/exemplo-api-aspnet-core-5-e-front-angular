import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UsuarioComponent} from './usuario.component';
import {SharedModule} from '../../shared/shared.module';
import {UsuarioCadastroComponent} from './usuario-cadastro/usuario-cadastro.component';
import {UsuarioEdicaoComponent} from './usuario-edicao/usuario-edicao.component';
import {UsuarioRoutingModule} from './usuario-routing.module';

@NgModule({
  imports: [
    UsuarioRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    UsuarioComponent,
    UsuarioCadastroComponent,
    UsuarioEdicaoComponent
  ],
  exports: [
  ],
  providers: []
})
export class UsuarioModule {

}
