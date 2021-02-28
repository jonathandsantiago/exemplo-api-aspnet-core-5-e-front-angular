import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ComandaComponent} from './comanda.component';
import {SharedModule} from '../../shared/shared.module';
import {ComandaCrudComponent} from './comanda-crud/comanda-crud.component';
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
    ComandaCrudComponent,
    ComandaPedidoComponent
  ],
  exports: [
  ],
  providers: []
})
export class ComandaModule {

}
