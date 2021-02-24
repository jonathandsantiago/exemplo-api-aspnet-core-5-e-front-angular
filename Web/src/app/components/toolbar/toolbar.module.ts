import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {ToolbarComponent} from './toolbar.component';
import {SharedModule} from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    ToolbarComponent
  ],
  exports: [
    ToolbarComponent
  ],
  providers: []
})
export class ToolbarModule {

}
