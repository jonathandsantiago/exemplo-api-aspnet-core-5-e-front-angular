import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgxSpinnerService} from 'ngx-spinner';

import {environment} from '../../environments/environment';
import {BaseService} from '../shared/services/base.service';
import {convertToInt} from '../shared/util';
import {Comanda} from '../models/comanda';

@Injectable({providedIn: 'root'})
export class ComandaService extends BaseService<Comanda> implements OnDestroy {

  protected urlApi = `${environment.apiUrl}/api/Comanda`;

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
    super(http, spinner);
  }

  protected formatarEntidade(comanda, args): any {
    const params = JSON.parse(JSON.stringify(comanda));
    params.id = convertToInt(args.id, 0);
    return params;
  }

  ngOnDestroy(): void {
  }
}
