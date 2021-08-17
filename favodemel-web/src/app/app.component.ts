import {Component, OnDestroy, OnInit} from '@angular/core';
import { RxStompService, StompState } from '@stomp/ng2-stompjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(protected rxStompService: RxStompService ) {

  }

  ngOnInit() {
    this.rxStompService.connectionState$.subscribe(c => console.log(StompState[c]));
  }

  ngOnDestroy() {
  }
}
