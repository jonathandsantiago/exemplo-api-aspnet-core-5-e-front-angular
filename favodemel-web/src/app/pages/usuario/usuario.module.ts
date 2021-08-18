import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {UsuarioComponent} from './usuario.component';
import {SharedModule} from '../../shared/shared.module';
import {UsuarioEditComponent} from './usuario-edit/usuario-edit.component';
import {UsuarioRoutingModule} from './usuario-routing.module';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {PaginacaoModule} from '../../shared/paginacao/paginacao.module';
import {NgxCurrencyModule} from 'ngx-currency';

@NgModule({
  imports: [
    UsuarioRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    TooltipModule.forRoot(),
    NgxCurrencyModule,
    PaginacaoModule
  ],
  declarations: [
    UsuarioComponent,
    UsuarioEditComponent,
  ],
  exports: [
  ],
  providers: []
})
export class UsuarioModule {

}
