export class Usuario {
  id: any;
  nome: string;
  login: string;
  password: string;
  perfil: UsuarioPerfil;
  ativo: boolean;
  token?: string;
}

export enum UsuarioPerfil {
  Garcon = 1,
  Cozinheiro = 2,
  Administrador = 3,
}
