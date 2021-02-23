import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs';
import {RxStompService} from '@stomp/ng2-stompjs';
import {Message} from '@stomp/stompjs';
import {Comanda} from '../../../models/comanda';

@Component({
  selector: 'app-comanda-cozinha',
  templateUrl: './comanda-cozinha.component.html',
  styleUrls: ['./comanda-cozinha.component.scss']
})
export class ComandaCozinhaComponent implements OnInit, OnDestroy {
  loading = false;
  public comandas: Comanda[] = [];
  private comandaSubscription: Subscription;

  constructor(private rxStompService: RxStompService) {
  }

  ngOnInit() {
    this.comandaSubscription = this.rxStompService.watch('/queue/teste').subscribe((message: Message) => {
      this.comandas.push(JSON.parse(message.body));
    });
  }

  ngOnDestroy() {
    this.comandaSubscription.unsubscribe();
  }

  onSend() {
    const message = `Message generated at ${new Date()}`;
    this.rxStompService.publish({ destination: '/queue/teste', body: message });
  }
}
