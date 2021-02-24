import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.scss']
})
export class ComandaComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();

  constructor() {
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
