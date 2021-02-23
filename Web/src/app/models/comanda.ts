import {ComandaPedido} from './comanda-pedido';
import {Usuario} from './usuario';
import {IModelBase} from './model-base';

export class Comanda implements IModelBase<number> {
  id: number;
  garcom: Usuario;
  pedidos: ComandaPedido[];
  totalAPagar: number;
  gorjetaGarcom: number;

  constructor(garcom: Usuario) {
    this.garcom = garcom;
    this.pedidos = [] as ComandaPedido[];
    this.totalAPagar = 0;
    this.gorjetaGarcom = 0;
  }
}
