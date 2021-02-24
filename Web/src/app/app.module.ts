import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ToolbarModule} from './components/toolbar/toolbar.module';
import {FooterModule} from './components/footer/footer.module';
import {LayoutModule} from './components/layout/layout.module';
import {InjectableRxStompConfig, RxStompService, rxStompServiceFactory} from '@stomp/ng2-stompjs';
import {rxStompConfig} from './shared/rx-stomp.config';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {ErrorInterceptor} from './shared/error.interceptor';
import {JwtInterceptor} from './shared/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ToolbarModule,
    FooterModule,
    LayoutModule
  ],
  providers: [
    {provide: InjectableRxStompConfig, useValue: rxStompConfig},
    {provide: RxStompService, useFactory: rxStompServiceFactory, deps: [InjectableRxStompConfig]},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
