import {Injectable} from '@angular/core';
import {QueryEntity} from '@datorama/akita';
import {ComandaState, ComandaStore} from './comanda.store';
import {Comanda} from '../../models/comanda';

@Injectable({
  providedIn: 'root'
})
export class ComandaQuery extends QueryEntity<ComandaState, Comanda> {

  constructor(protected store: ComandaStore) {
    super(store);
  }
}
