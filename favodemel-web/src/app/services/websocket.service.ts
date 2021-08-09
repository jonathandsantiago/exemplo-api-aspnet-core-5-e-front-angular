import {BehaviorSubject} from 'rxjs';
import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private socket: WebSocket;
  filaPedidoSubject$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  filaPedido$ = this.filaPedidoSubject$.asObservable();

  constructor() {
  }

  startSocket() {
    this.socket = new WebSocket(`wss://${environment.webSocket}`);
    this.socket.addEventListener('open', (ev => {
      console.log('Mensageria connectada!');
    }));
    this.socket.addEventListener('message', (ev => {
      console.log('Mensagem: ', ev);

      if (!ev || !ev.data) {
        return;
      }

      console.log('teste: ', ev.data);
      const messageBox = JSON.parse(ev.data);
      switch (messageBox.topic) {
        case 'fila_pedido':
          this.filaPedidoSubject$.next(messageBox.value);
          break;
        default:
          break;
      }
    }));
  }
}
