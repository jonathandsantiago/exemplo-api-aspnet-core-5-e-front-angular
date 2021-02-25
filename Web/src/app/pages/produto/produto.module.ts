import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ProdutoComponent} from './produto.component';
import {SharedModule} from '../../shared/shared.module';
import {ProdutoCadastroComponent} from './produto-cadastro/produto-cadastro.component';
import {ProdutoEdicaoComponent} from './produto-edicao/produto-edicao.component';
import {ProdutoRoutingModule} from './produto-routing.module';

@NgModule({
  imports: [
    ProdutoRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    ProdutoComponent,
    ProdutoCadastroComponent,
    ProdutoEdicaoComponent
  ],
  exports: [
  ],
  providers: []
})
export class ProdutoModule {

}
