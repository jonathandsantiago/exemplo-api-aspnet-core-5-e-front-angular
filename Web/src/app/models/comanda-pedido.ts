import {Comanda} from './comanda';
import {Produto} from './produto';
import {IModelBase} from './model-base';

export class ComandaPedido implements IModelBase<number> {
  id: number;
  comanda: Comanda;
  produto: Produto;
  quantidade: number;
  situacao: ComandaPedidoSituacao;

  constructor(comanda: Comanda) {
    this.comanda = comanda;
    this.quantidade = 0;
    this.situacao = ComandaPedidoSituacao.pedido;
  }
}

export enum ComandaPedidoSituacao {
  pedido,
  preparando,
  pronto
}
