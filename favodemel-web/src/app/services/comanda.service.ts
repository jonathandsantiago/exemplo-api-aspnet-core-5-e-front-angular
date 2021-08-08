import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';

import {environment} from '../../environments/environment';
import {BaseService, prepare} from '../shared/services/base.service';
import {convertToInt} from '../shared/util';
import {Comanda} from '../models/comanda';
import {finalize} from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class ComandaService extends BaseService<Comanda>{

  protected urlApi = `${environment.apiUrl}/Comanda`;

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
    super(http, spinner);
  }

  protected formatarEntidade(comanda, args): any {
    const params = JSON.parse(JSON.stringify(comanda));
    params.id = convertToInt(args.id, 0);
    params.garcomId = convertToInt(comanda.garcomId, 0);
    params.situacao = convertToInt(comanda.situacao);

    params.pedidos = (args.pedidos || []).map(c => {
      const pedido = JSON.parse(JSON.stringify(c));
      pedido.id = convertToInt(pedido.id, 0);
      pedido.comandaId = params.id;
      pedido.produtoId = convertToInt(pedido.produtoId, 0);
      pedido.quantidade = convertToInt(pedido.quantidade, 0);
      pedido.situacao = convertToInt(pedido.situacao);
      return pedido;
    });
    return params;
  }

  obterTodosPorSituao(situacao: any) {
    return this.http.get<any>(`${this.urlApi}/ObterTodosPorSituao/?situacao=${situacao}`);
  }

  confirmar(id: number) {
    return this.http.post(`${this.urlApi}/Confirmar`, id)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  fechar(id: number) {
    return this.http.post(`${this.urlApi}/Fechar`, id)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }
}
