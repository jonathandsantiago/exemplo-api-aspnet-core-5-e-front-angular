import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ComandaComponent} from './comanda.component';
import {SharedModule} from '../../shared/shared.module';
import {ComandaCadastroComponent} from './comanda-cadastro/comanda-cadastro.component';
import {ComandaEdicaoComponent} from './comanda-edicao/comanda-edicao.component';
import {ComandaRoutingModule} from './comanda-routing.module';
import {CardModule} from '../../components/card/card.module';
import {ModalModule} from 'ngx-bootstrap/modal';
import {ComandaPedidoComponent} from './companda-pedido/comanda-pedido.component';

@NgModule({
  imports: [
    ComandaRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    CardModule,
    ModalModule.forRoot(),
  ],
  declarations: [
    ComandaComponent,
    ComandaCadastroComponent,
    ComandaEdicaoComponent,
    ComandaPedidoComponent
  ],
  exports: [
  ],
  providers: []
})
export class ComandaModule {

}
