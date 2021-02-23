import {Paginacao} from '../helpers/paginacao';
import {Observable} from 'rxjs';
import {PaginationResponse} from '@datorama/akita';

export interface IService<TModel, TId> {
  get(paginacao: Paginacao, filter: any): Observable<PaginationResponse<TModel>>;

  getById(id: TId);

  add(model: TModel);

  update(id: TId, model: Partial<TModel>);
}
