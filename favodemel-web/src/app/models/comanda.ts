export class Comanda {
  id: any;
  garcomId: any;
  pedidos: ComandaPedido[];
  totalAPagar: any;
  gorjetaGarcom: any;
  situacao: ComandaSituacao;
  dataCadastro: any;
  codigo: string;
}

export class ComandaPedido {
  id: any;
  produtoId: any;
  produtoNome: string;
  produtoPreco: any;
  quantidade: number;
  situacao: ComandaPedidoSituacao;
}

export enum ComandaSituacao {
  Aberta = 1,
  EmAndamento = 2,
  Fechada = 3
}

export enum ComandaPedidoSituacao {
  Pedido = 1,
  Preparando = 2,
  Pronto = 3
}
