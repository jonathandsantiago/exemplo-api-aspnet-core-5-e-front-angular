import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';

import {UsuarioService} from '../services/usuario.service';
import {environment} from '../../environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private usuarioService: UsuarioService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const usuario = this.usuarioService.usuarioLogado;
    const isApiUrl = request.url.startsWith(environment.apiUrl);

    if (usuario && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${usuario.token}`,
          Accept: 'text/plain, text/html,application/xhtml+xml,application/xml;' +
            'q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3'
        }
      });
    }

    return next.handle(request);
  }
}
