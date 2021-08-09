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
      const mensagem = JSON.parse(data.Mensagem);
      const value = JSON.parse(mensagem.Value);
      switch (mensagem.Evento) {
        case 'fila_pedido':
          this.filaPedidoSubject$.next(value);
          break;
        case 'confirmacao_pedido':
          this.confirmacaoPedidoSubject$.next(value);
          break;
        case 'finalizacao_pedido':
          this.finalizacaoPedidoSubject$.next(value);
          break;
        default:
          break;
      }
    }));
  }
}
