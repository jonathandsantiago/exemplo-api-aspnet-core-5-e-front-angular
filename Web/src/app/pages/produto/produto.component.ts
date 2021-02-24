import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();

  constructor() {
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
