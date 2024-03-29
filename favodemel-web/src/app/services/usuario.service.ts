import {Injectable, OnDestroy} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {finalize, map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs';
import {NgxSpinnerService} from 'ngx-spinner';

import {Usuario, UsuarioPerfil} from '../models/usuario';
import {environment} from '../../environments/environment';
import {BaseService, prepare} from './common/base.service';
import {convertToInt} from '../shared/functions';

@Injectable({providedIn: 'root'})
export class UsuarioService extends BaseService<Usuario> implements OnDestroy {

  protected urlApi = `${environment.apiUrl}/usuarios`;

  private usuarioLogadoSubject$: BehaviorSubject<Usuario>;
  usuarioLogado$: Observable<Usuario>;

  private usuariosSubject$ = new BehaviorSubject<any>([]);
  usuarios$ = this.usuariosSubject$.asObservable();

  get usuarioLogado(): Usuario {
    return this.usuarioLogadoSubject$.value;
  }

  constructor(protected router: Router,
              protected http: HttpClient,
              protected spinner: NgxSpinnerService) {
    super(http, spinner);
    const userCookie = localStorage.getItem('user');
    const user = userCookie ? JSON.parse(userCookie) : null;
    const imagem = localStorage.getItem('UserImage');

    if (user) {
      user.imagem = imagem ? imagem : 'assets/images/admin.fw.png';
    }

    this.usuarioLogadoSubject$ = new BehaviorSubject<Usuario>(user);
    this.usuarioLogado$ = this.usuarioLogadoSubject$.asObservable();
  }

  protected formatarEntidade(usuario, args): any {
    const params = JSON.parse(JSON.stringify(usuario));
    params.id = args.id;
    params.perfil = convertToInt(usuario.perfil);
    params.ativo = args.ativo ?? usuario.ativo;
    return params;
  }

  login(usuario) {
    return this.http.post<any>(`${this.urlApi}/login`, usuario)
      .pipe(prepare(() => this.spinner.show()),
        map((response: any) => {
          const usuarioLogado = response.usuario;
          usuarioLogado.token = response.accessToken;
          this.adicionarUsuarioLogado(usuarioLogado);
          return usuarioLogado;
        }),
        finalize(() => this.spinner.hide()));
  }

  obterUsuarios() {
    return this.usuariosSubject$.value;
  }

  async sair() {
    localStorage.clear();
    this.usuarioLogadoSubject$.next(null);
    await this.router.navigate(['/login']);
  }

  ativarOuInativar(id, ativo) {
    return this.http.put<boolean>(`${this.urlApi}/ativar-inativar/${id}`, {}, {params: {ativo}})
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  adicionarUsuario(usuario) {
    const usuarios = [...this.obterUsuarios()] as Usuario[];
    usuarios.push(usuario);
    this.atualizarListaUsuarios([...usuarios]);
  }

  atualizarUsuario(usuarioEdicao) {
    const usuarios = Object.assign([], this.obterUsuarios());
    this.atualizarListaUsuarios(usuarios.map(usuario => {

      if (usuarioEdicao.id === usuario.id) {
        return usuarioEdicao;
      }

      return usuario;
    }));
  }

  atualizarListaUsuarios(usuarios) {
    this.usuariosSubject$.next(usuarios);
  }

  alterarSenha(id, senha) {
    return this.http.put(`${this.urlApi}/alterar-senha/${id}`, {senha})
      .pipe(prepare(() => this.spinner.show()), finalize(() => this.spinner.hide()));
  }

  adicionarUsuarioLogado(usuario) {
    localStorage.clear();

    if (usuario.imagem && usuario.imagem.length <= 5200000) {
      localStorage.setItem('UserImage', usuario.imagem);
    }

    localStorage.setItem('user', JSON.stringify({
        id: usuario.id,
        nome: usuario.nome,
        perfil: usuario.perfil,
        token: usuario.token,
        ativo: usuario.ativo,
      }
    ));

    this.usuarioLogadoSubject$.next(usuario);
  }

  atualizarLista(usuarios: any) {
    this.usuariosSubject$.next(usuarios);
  }

  ngOnDestroy(): void {
    this.usuarioLogadoSubject$.unsubscribe();
  }

  obterTodosPorPerfil(perfil: UsuarioPerfil) {
    return this.http.get<any>(`${this.urlApi}/listar-por-perfil/${perfil}`);
  }
}
