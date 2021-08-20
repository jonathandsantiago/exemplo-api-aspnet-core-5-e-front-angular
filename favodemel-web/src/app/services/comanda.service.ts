import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';

import {environment} from '../../environments/environment';
import {BaseService, prepare} from './common/base.service';
import {convertToInt} from '../shared/functions';
import {Comanda} from '../models/comanda';
import {finalize, map} from 'rxjs/operators';
import {Message} from '@stomp/stompjs';
import {RxStompService} from '@stomp/ng2-stompjs';

@Injectable({providedIn: 'root'})
export class ComandaService extends BaseService<Comanda> {

  protected urlApi = `${environment.apiUrl}/comandas`;

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService,
              protected rxStompService: RxStompService) {
    super(http, spinner);
  }

  protected formatarEntidade(comanda, args): any {
    const params = JSON.parse(JSON.stringify(comanda));
    params.id = args.id;
    params.garcomId = comanda.garcomId;
    params.situacao = convertToInt(comanda.situacao);

    params.pedidos = (args.pedidos || []).map(c => {
      const pedido = JSON.parse(JSON.stringify(c));
      pedido.comandaId = params.id;
      pedido.quantidade = convertToInt(pedido.quantidade, 0);
      pedido.situacao = convertToInt(pedido.situacao);
      return pedido;
    });
    return params;
  }

  protected converterComandaMensageria(response) {
    if (!response || !response.body) {
      return null;
    }

    const body = JSON.parse(response.body);
    return body.message ? body.message.comanda : null;
  }

  obterTodosPaginadoPorSituao(filtro: any) {
    return this.http.get<any>(`${this.urlApi}/listar-paginado-por-situacao`, {params: filtro});
  }

  confirmar(id: any) {
    return this.http.post(`${this.urlApi}/confirmar`, {id})
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  fechar(id: any) {
    return this.http.post(`${this.urlApi}/fechar`, {id})
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  obterMensagensComandaCadastroCommand() {
    return this.rxStompService.watch('/exchange/ComandaCadastroCommand')
      .pipe(map((message: Message) => this.converterComandaMensageria(message)));
  }

  obterMensagensComandaEditarCommand() {
    return this.rxStompService.watch('/exchange/ComandaEditarCommand')
      .pipe(map((message: Message) => this.converterComandaMensageria(message)));
  }

  obterMensagensComandaConfirmarCommand() {
    return this.rxStompService.watch('/exchange/ComandaConfirmarCommand')
      .pipe(map((message: Message) => this.converterComandaMensageria(message)));
  }

  obterMensagensComandaFecharCommand() {
    return this.rxStompService.watch('/exchange/ComandaFecharCommand')
      .pipe(map((message: Message) => this.converterComandaMensageria(message)));
  }
}
