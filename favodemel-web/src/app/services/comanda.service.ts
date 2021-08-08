import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';

import {environment} from '../../environments/environment';
import {BaseService, prepare} from './common/base.service';
import {convertToInt} from '../shared/functions';
import {Comanda} from '../models/comanda';
import {finalize} from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class ComandaService extends BaseService<Comanda>{

  protected urlApi = `${environment.apiUrl}/comandas`;

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
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
    console.log(JSON.stringify(params));
    return params;
  }

  obterTodosPorSituao(situacao: any) {
    return this.http.get<any>(`${this.urlApi}/listar-por-situacao/${situacao}`);
  }

  confirmar(id: number) {
    return this.http.post(`${this.urlApi}/confirmar`, id)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  fechar(id: number) {
    return this.http.post(`${this.urlApi}/fechar`, id)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }
}
