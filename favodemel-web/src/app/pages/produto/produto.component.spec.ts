import {CommonModule} from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {NgxCurrencyModule} from 'ngx-currency';
import {NgxSpinnerModule} from 'ngx-spinner';
import {ToastrModule} from 'ngx-toastr';
import {AppRoutingModule} from 'src/app/app-routing.module';
import {LayoutModule} from 'src/app/components/layout/layout.module';
import {ToolbarModule} from 'src/app/components/layout/toolbar/toolbar.module';
import {PaginacaoModule} from 'src/app/components/paginacao/paginacao.module';
import {ErrorInterceptor} from 'src/app/shared/error.interceptor';
import {JwtInterceptor} from 'src/app/shared/jwt.interceptor';
import {SharedModule} from 'src/app/shared/shared.module';
import {ProdutoRoutingModule} from './produto-routing.module';

import {ProdutoComponent} from './produto.component';

describe('ProdutoComponent', () => {
  let component: ProdutoComponent;
  let fixture: ComponentFixture<ProdutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToolbarModule,
        LayoutModule,
        NgxSpinnerModule,
        ToastrModule.forRoot(),
        BsDatepickerModule.forRoot(),
        ProdutoRoutingModule,
        CommonModule,
        SharedModule,
        NgxCurrencyModule,
        TooltipModule.forRoot(),
        PaginacaoModule
      ],
      declarations: [ ProdutoComponent ],
      providers: [
        {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
        {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should produto', () => {
    expect(component).toBeTruthy();
  });
});
