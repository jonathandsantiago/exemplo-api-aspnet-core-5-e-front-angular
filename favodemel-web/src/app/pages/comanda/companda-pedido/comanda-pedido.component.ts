import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Subscription} from 'rxjs';
import {Produto} from '../../../models/produto';

@Component({
  selector: 'app-comanda-pedido',
  templateUrl: './comanda-pedido.component.html',
  styleUrls: ['./comanda-pedido.component.scss']
})
export class ComandaPedidoComponent implements OnInit, OnDestroy {
  @Input() isVisualizacao: boolean;
  @Input() form: FormGroup;
  @Input() produtos: Produto[];

  subscription: Subscription = new Subscription();

  get formControl() {
    return this.form.controls;
  }

  constructor(protected formBuilder: FormBuilder) {
  }

  ngOnInit(): void {

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
