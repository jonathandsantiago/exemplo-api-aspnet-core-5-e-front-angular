import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {BehaviorSubject} from 'rxjs';

import {environment} from '../../environments/environment';
import {BaseService} from './common/base.service';
import {Produto} from '../models/produto';

@Injectable({providedIn: 'root'})
export class ProdutoService extends BaseService<Produto> implements OnDestroy {

  protected urlApi = `${environment.apiUrl}/produtos`;
  private produtosSubject$ = new BehaviorSubject<Produto[]>([] as Produto[]);
  produtos$ = this.produtosSubject$.asObservable();

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
    super(http, spinner);
  }

  protected formatarEntidade(produto, args): any {
    const params = JSON.parse(JSON.stringify(produto));
    params.id = args.id;
    return params;
  }

  ngOnDestroy(): void {
  }

  atualizarLista(produtos: any) {
    this.produtosSubject$.next(produtos);
  }
}
