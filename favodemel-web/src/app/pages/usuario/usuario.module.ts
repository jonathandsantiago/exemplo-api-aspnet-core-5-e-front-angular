import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UsuarioComponent} from './usuario.component';
import {SharedModule} from '../../shared/shared.module';
import {UsuarioCrudComponent} from './usuario-crud/usuario-crud.component';
import {UsuarioRoutingModule} from './usuario-routing.module';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {PaginacaoModule} from '../../shared/paginacao/paginacao.module';

@NgModule({
  imports: [
    UsuarioRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    TooltipModule.forRoot(),
    PaginacaoModule
  ],
  declarations: [
    UsuarioComponent,
    UsuarioCrudComponent,
  ],
  exports: [
  ],
  providers: []
})
export class UsuarioModule {

}
