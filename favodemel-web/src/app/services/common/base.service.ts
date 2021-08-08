import {HttpClient} from '@angular/common/http';
import {NgxSpinnerService} from 'ngx-spinner';
import {finalize} from 'rxjs/operators';
import {defer, Observable} from 'rxjs';

export function prepare<T>(callback: () => void): (source: Observable<T>) => Observable<T> {
  return (source: Observable<T>): Observable<T> => defer(() => {
    callback();
    return source;
  });
}

export abstract class BaseService<TEntidade> {
  protected abstract urlApi: string;

  constructor(protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
  }

  protected formatarEntidade(entidade: TEntidade, args: any = null) {
    return entidade;
  }

  cadastrar(entidade: TEntidade, args: any = null) {
    const params = this.formatarEntidade(entidade, args);
    return this.http.post(`${this.urlApi}/cadastrar`, params)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  editar(entidade: TEntidade, args: any = null) {
    const params = this.formatarEntidade(entidade, args);
    return this.http.put(`${this.urlApi}/editar`, params)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  obterTodos(params?: any) {
    return this.http.get<any>(`${this.urlApi}/listar-todos`, params);
  }

  obter(url: string, params?: any) {
    return this.http.get<any>(`${this.urlApi}/${url}`, {params})
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  obterPorId(id: any) {
    return this.http.get<TEntidade>(`${this.urlApi}/${id}`);
  }

  excluir(id: number) {
    return this.http.delete(`${this.urlApi}/excluir/${id}`)
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }
}
