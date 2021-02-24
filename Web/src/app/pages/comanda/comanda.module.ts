import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ComandaComponent} from './comanda.component';
import {SharedModule} from '../../shared/shared.module';
import {ComandaCadastroComponent} from './comanda-cadastro/comanda-cadastro.component';
import {ComandaEdicaoComponent} from './comanda-edicao/comanda-edicao.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    ComandaComponent,
    ComandaCadastroComponent,
    ComandaEdicaoComponent
  ],
  exports: [
    ComandaComponent,
    ComandaCadastroComponent,
    ComandaEdicaoComponent
  ],
  providers: []
})
export class ComandaModule {

}
