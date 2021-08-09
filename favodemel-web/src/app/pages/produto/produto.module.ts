import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ProdutoComponent} from './produto.component';
import {SharedModule} from '../../shared/shared.module';
import {ProdutoEditComponent} from './produto-edit/produto-edit.component';
import {ProdutoRoutingModule} from './produto-routing.module';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {PaginacaoModule} from '../../shared/paginacao/paginacao.module';

@NgModule({
  imports: [
    ProdutoRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    TooltipModule.forRoot(),
    PaginacaoModule
  ],
  declarations: [
    ProdutoComponent,
    ProdutoEditComponent,
  ],
  exports: [
  ],
  providers: []
})
export class ProdutoModule {

}
