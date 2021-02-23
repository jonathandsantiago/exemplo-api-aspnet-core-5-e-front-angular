import {Injectable} from '@angular/core';
import {EntityState, EntityStore, StoreConfig, MultiActiveState} from '@datorama/akita';
import {Comanda} from '../../models/comanda';

export interface ComandaState extends EntityState<Comanda>, MultiActiveState {
  total: number;
  page: number;
  pageSize: number;
  filter: any;
  sort: { coluna: string; direcao: string; };
}

const initialState: ComandaState = {
  active: [],
  total: 0,
  page: 0,
  pageSize: 5,
  filter: {},
  sort: {coluna: 'id', direcao: 'asc'}
};

@Injectable({providedIn: 'root'})
@StoreConfig({name: 'Comanda'})
export class ComandaStore extends EntityStore<ComandaState, Comanda> {

  constructor() {
    super(initialState);
  }
}
