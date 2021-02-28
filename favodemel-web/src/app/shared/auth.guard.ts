import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';

import {UsuarioService} from '../services/usuario.service';
import {UsuarioPerfil} from '../models/usuario';

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate {
  constructor(protected router: Router,
              protected usuarioService: UsuarioService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const usuarioLogado = this.usuarioService.usuarioLogado;

    if (usuarioLogado) {
      return this.permiteAcessarRotina(usuarioLogado, state);
    }

    this.router.navigate(['/login'], {queryParams: {returnUrl: state.url}}).then();
    return false;
  }

  permiteAcessarRotina(usuarioLogado, state: RouterStateSnapshot) {
    if (usuarioLogado.perfil === UsuarioPerfil.Administrador) {
      return true;
    } else if (state.url === `/usuario/${usuarioLogado.id}/editar`) {
      return true;
    }

    if (state.url.includes('editar')) {
      return false;
    }

    return true;
  }
}

