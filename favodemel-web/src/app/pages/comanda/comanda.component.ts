import {Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import {FormBuilder, FormControl} from '@angular/forms';
import {BehaviorSubject, combineLatest, Subscription} from 'rxjs';
import {ComandaService} from '../../services/comanda.service';
import {Comanda, ComandaPedido, ComandaSituacao} from '../../models/comanda';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {UsuarioService} from '../../services/usuario.service';
import {Usuario, UsuarioPerfil} from '../../models/usuario';
import {ToastrService} from 'ngx-toastr';
import {RxStompService} from '@stomp/ng2-stompjs';
import * as moment from 'moment';
import {convertDateTimeString} from '../../shared/functions';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.scss']
})
export class ComandaComponent implements OnInit, OnDestroy {
  readonly formGroup = this.formBuilder.group({
    dataCadastro: new FormControl(null)
  });

  subscription: Subscription = new Subscription();
  mudancaPaginaComandasAberta$ = new BehaviorSubject<any>({page: 1, limite: 10});
  mudancaPaginaComandasEmAndamento$ = new BehaviorSubject<any>({page: 1, limite: 10});
  mudancaPaginaComandasFechadas$ = new BehaviorSubject<any>({page: 1, limite: 10});

  comandasAberta: Comanda[] = [];
  comandasEmAndamento: Comanda[] = [];
  comandasFechada: Comanda[] = [];

  modalRef: BsModalRef;
  tituloModal: string;
  usuarioLogado: Usuario;
  usuarioPerfil = UsuarioPerfil;
  comandaSituacao = ComandaSituacao;
  totalEmAberto = 0;
  totalEmAndamento = 0;
  totalFechada = 0;

  constructor(protected formBuilder: FormBuilder,
              protected modalService: BsModalService,
              protected usuarioService: UsuarioService,
              protected comandaService: ComandaService,
              protected toastService: ToastrService,
              protected rxStompService: RxStompService) {
    this.subscription.add(this.usuarioService.usuarioLogado$.subscribe((usuario: Usuario) => {
      this.usuarioLogado = usuario;
    }));
  }

  ngOnInit(): void {
    this.configurarBuscas();
    this.configurarBuscasConsumers();
  }

  configurarBuscas() {
    const mudancaData$ = this.formGroup.get('dataCadastro').valueChanges;
    this.subscription.add(combineLatest([this.mudancaPaginaComandasAberta$, mudancaData$])
      .subscribe(([pageInfo, dataCadastro]: any) => {
        this.comandaService.obterTodosPaginadoPorSituao(this.obterFiltro(ComandaSituacao.Aberta, pageInfo, dataCadastro))
          .subscribe(result => {
            const comandas = result.itens
              .map((comanda: Comanda) => {
                comanda.pedidos
                  .map((pedido: ComandaPedido) => {
                    pedido.produtoPreco = `${pedido.produtoPreco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
                    return pedido;
                  });
                return comanda;
              });

            this.totalEmAberto = result.total;
            this.comandasAberta = result.total === 0 ? [] : this.comandasAberta
              .concat(comandas.filter(c => this.comandasAberta.findIndex(d => d.id === c.id) <= -1));
          });
      }));

    this.subscription.add(combineLatest([this.mudancaPaginaComandasEmAndamento$, mudancaData$])
      .subscribe(([pageInfo, dataCadastro]: any) => {
        this.comandaService.obterTodosPaginadoPorSituao(this.obterFiltro(ComandaSituacao.EmAndamento, pageInfo, dataCadastro))
          .subscribe(result => {
            this.totalEmAndamento = result.total;
            this.comandasEmAndamento = result.total === 0 ? [] : this.comandasEmAndamento
              .concat(result.itens.filter(c => this.comandasEmAndamento.findIndex(d => d.id === c.id) <= -1));
          });
      }));

    this.subscription.add(combineLatest([this.mudancaPaginaComandasFechadas$, mudancaData$])
      .subscribe(([pageInfo, dataCadastro]: any) => {
        this.comandaService.obterTodosPaginadoPorSituao(this.obterFiltro(ComandaSituacao.Fechada, pageInfo, dataCadastro))
          .subscribe(result => {
            const comandas = result.itens
              .map((comanda: Comanda) => {
                comanda.gorjetaGarcom = `${comanda.gorjetaGarcom.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
                comanda.totalAPagar = `${comanda.totalAPagar.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
                return comanda;
              });

            this.totalFechada = result.total;
            this.comandasFechada = result.total === 0 ? [] : this.comandasFechada
              .concat(comandas.filter(c => this.comandasFechada.findIndex(d => d.id === c.id) <= -1));
          });
      }));
    this.formGroup.get('dataCadastro').setValue(moment().toDate());
  }

  configurarBuscasConsumers() {
    this.subscription.add(this.comandaService.obterMensagensComandaCadastroCommand().subscribe(comanda => {
      if (comanda) {
        this.toastService.success(`Novo pedido cadastrado: ${comanda.codigo}`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });
        this.totalEmAberto += 1;
        this.inserirOuAtualizarComanda(comanda);      }
    }));

    this.subscription.add(this.comandaService.obterMensagensComandaEditarCommand().subscribe(comanda => {
      if (comanda) {
        this.toastService.success(`Atualizado pedido: ${comanda.codigo}`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });
        this.inserirOuAtualizarComanda(comanda);
      }
    }));

    this.subscription.add(this.comandaService.obterMensagensComandaConfirmarCommand().subscribe(comanda => {
      if (comanda) {
        this.toastService.success(`Confirmado o pedido: ${comanda.codigo}`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });
        this.moverPedidoConfirmado(comanda.id, comanda);
      }
    }));

    this.subscription.add(this.comandaService.obterMensagensComandaFecharCommand().subscribe(comanda => {
      if (comanda) {
        this.toastService.success(`Fechado o pedido: ${comanda.codigo}`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });
        this.moverComandaFechado(comanda.id, comanda);
      }
    }));
  }

  obterFiltro(situacao, pageInfo, data) {
    const params: { [key: string]: string } = {};
    params.pagina = String(pageInfo.page);
    params.limite = String(pageInfo.limite);
    params.situacao = String(situacao);
    params.data = data ? convertDateTimeString(data) : convertDateTimeString(moment().toDate());

    return params;
  }

  abrirModal(template: TemplateRef<any>, content?: any, titulo = null, largura = 'lg') {
    this.modalRef = this.modalService.show(template,
      {
        class: `modal-${largura} modal-dialog-centered`,
        ignoreBackdropClick: true
      }) as BsModalRef;

    this.tituloModal = titulo ?? 'Comanda';
    this.modalRef.content = content;
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  confirmarComanda(id: any) {
    this.subscription.add(this.comandaService.confirmar(id).subscribe());
  }

  moverPedidoConfirmado(id, comanda) {
    const index = this.comandasAberta.findIndex(c => c.id === id);
    this.totalEmAberto -= 1;
    this.totalEmAndamento += 1;
    this.comandasAberta.splice(index, 1);
    this.comandasEmAndamento.push(comanda);
  }

  fecharComanda(id: any) {
    this.subscription.add(this.comandaService.fechar(id).subscribe());
  }

  moverComandaFechado(id, comanda) {
    const index = this.comandasEmAndamento.findIndex(c => c.id === id);
    this.totalEmAndamento -= 1;
    this.totalFechada += 1;
    this.comandasEmAndamento.splice(index, 1);
    this.comandasFechada.push(comanda);
  }

  inserirOuAtualizarComanda(comanda: Comanda) {
    if (comanda.situacao === ComandaSituacao.Aberta) {
      this.removerComanda(comanda, this.comandasAberta);
      this.comandasAberta.push(comanda);
    } else {
      this.atualizarComanda(comanda);
    }
  }

  removerComanda(comanda, comandas) {
    const index = comandas.findIndex(c => c.id === comanda.id);
    if (index >= 0) {
      comandas.splice(index, 1);
    }
  }

  atualizarComanda(comanda: Comanda) {
    switch (comanda.situacao) {
      case ComandaSituacao.Aberta:
        const comandaAberta = this.comandasAberta.find(c => c.id === comanda.id);
        if (comandaAberta) {
          comandaAberta.garcomId = comanda.garcomId;
          comandaAberta.pedidos = comanda.pedidos.map((pedido: ComandaPedido) => {
            pedido.produtoPreco = `${pedido.produtoPreco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
            return pedido;
          });
        }
        break;
      case ComandaSituacao.EmAndamento:
        const comandaEmAndamento = this.comandasEmAndamento.find(c => c.id === comanda.id);
        if (comandaEmAndamento) {
          comandaEmAndamento.garcomId = comanda.garcomId;
          comandaEmAndamento.pedidos = comanda.pedidos.map((pedido: ComandaPedido) => {
            pedido.produtoPreco = `${pedido.produtoPreco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
            return pedido;
          });
        }
        break;
      case ComandaSituacao.Fechada:
        break;
    }
  }

  carregarMais(situacao: ComandaSituacao) {
    let pagina;
    switch (situacao) {
      case ComandaSituacao.Aberta:
        pagina = this.mudancaPaginaComandasAberta$.value;
        pagina.page += 1;
        this.mudancaPaginaComandasAberta$.next(pagina);
        break;
      case ComandaSituacao.EmAndamento:
        pagina = this.mudancaPaginaComandasEmAndamento$.value;
        pagina.page += 1;
        this.mudancaPaginaComandasEmAndamento$.next(pagina);
        break;
      case ComandaSituacao.Fechada:
        pagina = this.mudancaPaginaComandasFechadas$.value;
        pagina.page += 1;
        this.mudancaPaginaComandasFechadas$.next(pagina);
        break;
    }
  }
}
