import {Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import {Subscription} from 'rxjs';
import {ComandaService} from '../../services/comanda.service';
import {Comanda, ComandaPedido, ComandaSituacao} from '../../models/comanda';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {UsuarioService} from '../../services/usuario.service';
import {Usuario, UsuarioPerfil} from '../../models/usuario';
import {ToastrService} from 'ngx-toastr';
import {RxStompService} from '@stomp/ng2-stompjs';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.scss']
})
export class ComandaComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  comandasAberta: Comanda[] = [];
  comandasEmAndamento: Comanda[] = [];
  comandasFechada: Comanda[] = [];

  modalRef: BsModalRef;
  tituloModal: string;
  usuarioLogado: Usuario;
  usuarioPerfil = UsuarioPerfil;

  constructor(protected modalService: BsModalService,
              protected usuarioService: UsuarioService,
              protected comandaService: ComandaService,
              protected toastService: ToastrService,
              protected rxStompService: RxStompService) {
    this.subscription.add(this.usuarioService.usuarioLogado$.subscribe((usuario: Usuario) => {
      this.usuarioLogado = usuario;
    }));
  }

  ngOnInit(): void {
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Aberta)
      .subscribe(comandasAberta => this.comandasAberta = comandasAberta
        .map((comanda: Comanda) => {
          comanda.pedidos
            .map((pedido: ComandaPedido) => {
              pedido.produtoPreco = `${pedido.produtoPreco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
              return pedido;
            });
          return comanda;
        })));

    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.EmAndamento)
      .subscribe(comandasEmAndamento => this.comandasEmAndamento = comandasEmAndamento));

    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Fechada)
      .subscribe(comandasFechada => this.comandasFechada = comandasFechada
        .map((comanda: Comanda) => {
          comanda.gorjetaGarcom = `${comanda.gorjetaGarcom.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
          comanda.totalAPagar = `${comanda.totalAPagar.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
          return comanda;
        })));

    this.subscription.add(this.comandaService.obterMensagensComandaCadastroCommand().subscribe(comanda => {
      if (comanda) {
        this.toastService.success(`Novo pedido cadastrado: ${comanda.codigo}`, null, {
          positionClass: 'toast-bottom-right',
          disableTimeOut: false,
          progressBar: true
        });
        this.inserirOuAtualizarComanda(comanda);
      }
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
    this.comandasAberta.splice(index, 1);
    this.comandasEmAndamento.push(comanda);
  }

  fecharComanda(id: any) {
    this.subscription.add(this.comandaService.fechar(id).subscribe());
  }

  moverComandaFechado(id, comanda) {
    const index = this.comandasEmAndamento.findIndex(c => c.id === id);
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
}
