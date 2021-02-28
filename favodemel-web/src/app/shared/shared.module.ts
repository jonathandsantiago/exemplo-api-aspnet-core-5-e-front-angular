import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {RouterModule} from '@angular/router';
import {SpinnerButtonDirective} from './directives/spinner-button.directive';
import {DragDropDirective} from './directives/drag-drop.directive';

@NgModule({
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    TooltipModule,
    RouterModule,
  ],
  declarations: [
    SpinnerButtonDirective,
    DragDropDirective,
  ],
  exports: [
    CommonModule,
    SpinnerButtonDirective,
    DragDropDirective,
  ]
})
export class SharedModule {
}
