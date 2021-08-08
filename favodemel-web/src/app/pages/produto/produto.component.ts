import {Component, Injector, OnInit} from '@angular/core';
import {combineLatest} from 'rxjs';
import {ProdutoService} from '../../services/produto.service';
import {ListBaseComponent} from '../../components/common/list-base.component';
import {Usuario, UsuarioPerfil} from '../../models/usuario';
import {UsuarioService} from '../../services/usuario.service';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent extends ListBaseComponent implements OnInit {
  columns = [
    {nome: 'id', titulo: 'Id'},
    {nome: 'nome', titulo: 'Nome'},
    {nome: 'preco', titulo: 'Preço'},
    {nome: 'acao', titulo: '', textAlign: 'right'},
  ];

  produtos$ = this.produtoService.produtos$;
  usuarioLogado: Usuario;
  usuarioPerfil = UsuarioPerfil;

  constructor(injector: Injector,
              protected usuarioService: UsuarioService,
              protected produtoService: ProdutoService) {
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
        this.produtoService.obter('listar-paginado', this.obterPagina(pageInfo))
          .subscribe((data: any) => {
            this.totalItens = data.total;
            this.produtoService.atualizarLista(data.itens.map(c => {
              c.preco = `R$ ${c.preco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
              return c;
            }));
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
