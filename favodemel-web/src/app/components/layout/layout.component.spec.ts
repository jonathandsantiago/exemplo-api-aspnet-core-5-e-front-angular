import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {LayoutComponent} from './layout.component';

describe('HeaderComponent', () => {
  let component: LayoutComponent;
  let fixture: ComponentFixture<LayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LayoutComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should main-comanda-edit', () => {
    expect(component).toBeTruthy();
  });
});
