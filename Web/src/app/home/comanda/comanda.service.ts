import {Injectable, Inject} from '@angular/core';
import {cacheable, ID, PaginationResponse} from '@datorama/akita';
import {Observable, throwError} from 'rxjs';
import {COMANDA_CONFIG, ComandaConfig} from './comanda.config';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {catchError, finalize, map, tap} from 'rxjs/operators';
import {Paginacao} from '../../helpers/paginacao';
import {ComandaStore} from './comanda.store';
import {Comanda} from '../../models/comanda';
import {IService} from '../../shared/interfaces';

@Injectable({providedIn: 'root'})
export class ComandaService implements IService<Comanda, ID> {

  constructor(
    protected store: ComandaStore,
    protected http: HttpClient,
    @Inject(COMANDA_CONFIG) protected config: ComandaConfig,
  ) {
  }

  public get(paginacao: Paginacao, filter?: any): Observable<PaginationResponse<Comanda>> {
    const headers = new HttpHeaders({'Content-Type': 'application/json;charset=utf-8'});

    return this.http.get<any>(`${this.config.api}/comandasabertas/${paginacao.pageSize}/${paginacao.pageNumber}`, {headers})
      .pipe(
        map((result) => {
          const paginatedResponse = {
            data: result.items,
            total: result.total,
            perPage: paginacao.pageSize,
            currentPage: paginacao.pageNumber,
          } as PaginationResponse<Comanda>;
          return paginatedResponse;
        }),
        catchError((error: HttpErrorResponse) => {
          return throwError(error);
        }),
      );
  }

  public setActive(ids: ID[]) {
    return this.store.setActive(ids);
  }

  public getById(id: ID) {
    this.store.setLoading(true);

    const request$ = this.http.get<Comanda>(`${this.config.api}/${id}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.store.setError(error);
          return throwError(error);
        }),
        tap((item) => this.store.add(item)),
      );

    const cacheableRequest = cacheable(this.store, request$)
      .pipe(finalize(() => {
        this.store.setLoading(false);
      }));
    return cacheableRequest;
  }

  public add(model: Comanda) {
    this.store.setLoading(true);

    return this.http.post<Comanda>(`${this.config.api}`, model)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.store.setError(error);
          return throwError(error);
        }),
        tap((modelAdded) => {
          this.store.add(modelAdded);
        }),
        finalize(() => {
          this.store.setLoading(false);
        })
      );
  }

  public update(id: ID, model: Partial<Comanda>) {
    this.store.setLoading(true);

    return this.http.put(`${this.config.api}/${id}`, model)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.store.setError(error);
          return throwError(error);
        }),
        tap(() => {
          this.store.update(id, model);
        }),
        finalize(() => {
          this.store.setLoading(false);
        }),
      ) as Observable<Comanda>;
  }
}
