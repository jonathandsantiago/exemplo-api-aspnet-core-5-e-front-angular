import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PaginacaoComponent} from './paginacao.component';
import {PaginationModule} from 'ngx-bootstrap/pagination';

@NgModule({
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
  ],
  declarations: [
    PaginacaoComponent
  ],
  exports: [
    PaginacaoComponent
  ],
  providers: []
})
export class PaginacaoModule {

}
