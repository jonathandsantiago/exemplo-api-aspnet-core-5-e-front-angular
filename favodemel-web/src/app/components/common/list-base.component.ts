import {BehaviorSubject, Subscription} from 'rxjs';
import {Component, Injector, OnDestroy} from '@angular/core';
import {PageInfo} from '../../models/pageinfo';

@Component({template: ''})
export abstract class ListBaseComponent implements OnDestroy {
  subscription: Subscription = new Subscription();
  mudancaPagina$ = new BehaviorSubject<PageInfo>({page: 1, itemsPerPage: 10} as PageInfo);
  pageNumber = 0;
  totalItens = 0;

  constructor(injector: Injector) {
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }


  mudarPagina(event) {
    this.pageNumber = event.page;
    this.mudancaPagina$.next(event);
  }
}
