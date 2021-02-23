import {ModelBase} from './model-base';

export class Usuario implements ModelBase {
  id: number;
  nome: string;
  login: string;
  password: string;
  setor: UsuarioSetor;
  token?: string;
}

export enum UsuarioSetor {
  Garcon,
  Cozinha
}
