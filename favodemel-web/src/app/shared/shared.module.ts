import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {RouterModule} from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    TooltipModule,
    RouterModule,
  ],
  declarations: [
  ],
  exports: [
    CommonModule,
  ]
})
export class SharedModule {
}
