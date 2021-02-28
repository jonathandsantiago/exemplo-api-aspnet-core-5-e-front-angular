import {Component, Injector, OnDestroy, OnInit} from '@angular/core';
import {combineLatest} from 'rxjs';
import {FormBuilder} from '@angular/forms';
import {UsuarioService} from '../../services/usuario.service';
import {DescricaoPerfil, Usuario, UsuarioPerfil} from '../../models/usuario';
import {ListComponent} from '../../components/common/list.component';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent extends ListComponent implements OnInit, OnDestroy {
  columns = [
    {nome: 'id', titulo: 'Id'},
    {nome: 'nome', titulo: 'Nome'},
    {nome: 'perfil', titulo: 'Perfil'},
    {nome: 'comissao', titulo: 'Comissão'},
    {nome: 'acao', titulo: '', textAlign: 'right'},
  ];

  descricaoPerfil = DescricaoPerfil;
  usuarios$ = this.usuarioService.usuarios$;
  usuarioLogado: Usuario;
  usuarioPerfil = UsuarioPerfil;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected usuarioService: UsuarioService) {
    super(injector);
    this.subscription.add(this.usuarioService.usuarioLogado$.subscribe((usuario: Usuario) => {
      this.usuarioLogado = usuario;
    }));
  }

  ngOnInit(): void {
    this.configurarPaginacao();
  }

  configurarPaginacao() {
    this.subscription.add(combineLatest([this.mudancaPagina$])
      .subscribe(([pageInfo]: any) => {
        this.usuarioService.obter('ObterTodosPaginado', this.obterPagina(pageInfo))
          .subscribe((data: any) => {
            this.totalItens = data.total;
            this.usuarioService.atualizarLista(data.itens);
          });
      }));
  }

  obterPagina(pageInfo) {
    const params: { [key: string]: string } = {};
    params.pagina = String(pageInfo.page - 1);
    params.limite = String(pageInfo.itemsPerPage);

    return params;
  }
}
