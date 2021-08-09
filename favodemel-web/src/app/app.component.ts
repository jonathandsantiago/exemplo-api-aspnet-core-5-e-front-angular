import {Component, OnDestroy, OnInit} from '@angular/core';
import {WebSocketService} from './services/websocket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  constructor(private websocketService: WebSocketService) {
  }

  ngOnInit() {
    this.websocketService.startSocket();
  }

  ngOnDestroy() {
  }
}
