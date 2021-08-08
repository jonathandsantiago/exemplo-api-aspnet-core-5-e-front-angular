import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';
import {environment} from '../../environments/environment';
import {BaseService} from '../shared/services/base.service';
import {convertToInt} from '../shared/util';
import {Produto} from '../models/produto';
import {BehaviorSubject} from 'rxjs';

@Injectable({providedIn: 'root'})
export class ProdutoService extends BaseService<Produto> implements OnDestroy {

  protected urlApi = `${environment.apiUrl}/Produto`;
  private produtosSubject$ = new BehaviorSubject<Produto[]>([] as Produto[]);
  produtos$ = this.produtosSubject$.asObservable();

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
    super(http, spinner);
  }

  protected formatarEntidade(produto, args): any {
    const params = JSON.parse(JSON.stringify(produto));
    params.id = convertToInt(args.id, 0);
    return params;
  }

  ngOnDestroy(): void {
  }

  atualizarLista(produtos: any) {
    this.produtosSubject$.next(produtos);
  }
}
