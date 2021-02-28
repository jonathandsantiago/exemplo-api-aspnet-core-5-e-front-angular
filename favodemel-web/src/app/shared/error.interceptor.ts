import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError, timer} from 'rxjs';
import {catchError} from 'rxjs/operators';

import {UsuarioService} from '../services/usuario.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(protected authenticationService: UsuarioService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {

      if (err.status === 401) {
        this.authenticationService.sair().then(() => timer(600).subscribe(() => location.reload()));
      }

      if (err.error instanceof Blob) {
        return this.parseErrorBlob(err);
      }

      if (!err.error) {
        return throwError('Não foi possível estabelecer conexão com servidor.');
      }

      if (typeof err.error === 'string') {
        return throwError(err.error);
      }

      const error = err.error.message || 'Não foi possível estabelecer conexão com servidor!';
      return throwError(error);
    }));
  }

  parseErrorBlob(err: HttpErrorResponse) {
    return new Promise<any>((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = (e: Event) => {
        try {
          const file = (e.currentTarget as FileReader);
          reject(new HttpErrorResponse({
            error: file.result,
            headers: err.headers,
            status: err.status,
            statusText: err.statusText,
            url: err.url
          }));
        } catch (e) {
          reject(err);
        }
      };
      reader.onerror = (e) => {
        reject(err);
      };
      reader.readAsText(err.error);
    });
  }
}
