export class Comanda {
  id: any;
  garcomId: any;
  pedidos: ComandaPedido[];
  totalAPagar: any;
  gorjetaGarcom: any;
  situacao: ComandaSituacao;
}

export class ComandaPedido {
  id: any;
}

export enum ComandaSituacao {
  Aberta = 1,
  Fechada = 2
}
