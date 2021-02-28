export class Usuario {
  id: any;
  nome: string;
  login: string;
  password: string;
  perfil: UsuarioPerfil;
  ativo: boolean;
  token?: string;
  comissao: any;
}

export enum UsuarioPerfil {
  Garcon = 1,
  Cozinheiro = 2,
  Administrador = 3,
}

export const DescricaoPerfil = {
  1: 'Garçom',
  2: 'Cozinheiro(a)',
  3: 'Administrador(a)'
};
