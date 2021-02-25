import {NgModule} from '@angular/core';
import {LoginRoutingModule} from './login-routing.module';
import {CommonModule} from '@angular/common';
import {LoginComponent} from './login.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

@NgModule({
  imports: [
    LoginRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [LoginComponent],
  providers: []
})
export class LoginModule {

}
