import {CommonModule} from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {InjectableRxStompConfig, RxStompService, rxStompServiceFactory} from '@stomp/ng2-stompjs';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import {ModalModule} from 'ngx-bootstrap/modal';
import {CardModule} from 'src/app/components/card/card.module';
import {ErrorInterceptor} from 'src/app/shared/error.interceptor';
import {JwtInterceptor} from 'src/app/shared/jwt.interceptor';
import {SharedModule} from 'src/app/shared/shared.module';
import {environment} from 'src/environments/environment';
import {ComandaRoutingModule} from './comanda-routing.module';

import {ComandaComponent} from './comanda.component';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ToolbarModule} from '../../components/layout/toolbar/toolbar.module';
import {LayoutModule} from '../../components/layout/layout.module';
import {NgxSpinnerModule} from 'ngx-spinner';
import {ToastrModule} from 'ngx-toastr';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';

const rxStompConfigFactory = (): InjectableRxStompConfig => environment.mensageriaConfig;

describe('ComandaComponent', () => {
  let component: ComandaComponent;
  let fixture: ComponentFixture<ComandaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserModule,
        HttpClientModule,
        HttpClientTestingModule,
        FormsModule,
        BrowserAnimationsModule,
        ToolbarModule,
        LayoutModule,
        NgxSpinnerModule,
        ToastrModule.forRoot(),
        BsDatepickerModule.forRoot(),
        RouterTestingModule,
        ComandaRoutingModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        CardModule,
        ModalModule.forRoot(),
        BsDatepickerModule.forRoot(),
      ],
      declarations: [ComandaComponent],
      providers: [
        {provide: InjectableRxStompConfig, useFactory: rxStompConfigFactory},
        {provide: RxStompService, useFactory: rxStompServiceFactory, deps: [InjectableRxStompConfig]},
        {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
        {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComandaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should comanda', () => {
    expect(component).toBeTruthy();
  });
});
