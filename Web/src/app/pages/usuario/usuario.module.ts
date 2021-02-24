import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UsuarioComponent} from './usuario.component';
import {SharedModule} from '../../shared/shared.module';
import {UsuarioCadastroComponent} from './usuario-cadastro/usuario-cadastro.component';
import {UsuarioEdicaoComponent} from './usuario-edicao/usuario-edicao.component';

@NgModule({
  imports: [
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
    UsuarioComponent,
    UsuarioCadastroComponent,
    UsuarioEdicaoComponent
  ],
  providers: []
})
export class UsuarioModule {

}
