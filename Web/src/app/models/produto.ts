import {IModelBase} from './model-base';

export class Produto implements IModelBase<number> {
  id: number;
  nome: string;
  preco: number;
}
