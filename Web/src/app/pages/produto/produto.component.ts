import {Component, Injector, OnInit} from '@angular/core';
import {combineLatest} from 'rxjs';
import {FormBuilder} from '@angular/forms';
import {ProdutoService} from '../../services/produto.service';
import {ListComponent} from '../../components/common/list.component';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent extends ListComponent implements OnInit {
  columns = [
    {nome: 'id', titulo: 'Id'},
    {nome: 'nome', titulo: 'Nome'},
    {nome: 'preco', titulo: 'Preço'},
    {nome: 'acao', titulo: '', textAlign: 'right'},
  ];

  produtos$ = this.produtoService.produtos$;

  constructor(injector: Injector,
              protected formBuilder: FormBuilder,
              protected produtoService: ProdutoService) {
    super(injector);
  }

  ngOnInit(): void {
    this.configurarPaginacao();
  }

  configurarPaginacao() {
    this.subscription.add(combineLatest([this.mudancaPagina$])
      .subscribe(([pageInfo]: any) => {
        this.produtoService.obter('ObterTodosPaginado', this.obterPagina(pageInfo))
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
