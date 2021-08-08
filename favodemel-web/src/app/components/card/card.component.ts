import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();

  constructor() {
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
