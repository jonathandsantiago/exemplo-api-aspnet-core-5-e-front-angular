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
  confirmacaoPedidoSubject$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  confirmacaoPedido$ = this.confirmacaoPedidoSubject$.asObservable();
  finalizacaoPedidoSubject$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  finalizacaoPedido$ = this.finalizacaoPedidoSubject$.asObservable();

  constructor() {
  }

  startSocket() {
    this.socket = new WebSocket(`wss://${environment.webSocket}`);
    this.socket.addEventListener('open', (ev => {
      console.log('Mensageria connectada!');
    }));
    this.socket.addEventListener('message', (ev => {
      if (!ev.data) {
        return;
      }

      const data = JSON.parse(ev.data);
      console.log(data);
      switch (data.topic) {
        case 'fila_pedido':
          this.filaPedidoSubject$.next(data.mensagem);
          break;
        case 'confirmacao_pedido':
          this.confirmacaoPedidoSubject$.next(data.mensagem);
          break;
        case 'finalizacao_pedido':
          this.finalizacaoPedidoSubject$.next(data.mensagem);
          break;
        default:
          break;
      }
    }));
  }
}
