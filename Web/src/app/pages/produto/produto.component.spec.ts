import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutoComponent } from './produto.component';

describe('ComandaComponent', () => {
  let component: ProdutoComponent;
  let fixture: ComponentFixture<ProdutoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProdutoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should main-comanda-cadastro', () => {
    expect(component).toBeTruthy();
  });
});
