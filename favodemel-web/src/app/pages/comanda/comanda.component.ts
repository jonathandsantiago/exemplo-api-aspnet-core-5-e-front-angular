import {Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import {Subscription} from 'rxjs';
import {ComandaService} from '../../services/comanda.service';
import {Comanda, ComandaPedido, ComandaSituacao} from '../../models/comanda';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {FormBuilder} from '@angular/forms';
import {UsuarioService} from '../../services/usuario.service';
import {Usuario, UsuarioPerfil} from '../../models/usuario';
import {RxStompService} from '@stomp/ng2-stompjs';
import {Message} from '@stomp/stompjs';

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
              protected formBuilder: FormBuilder,
              protected usuarioService: UsuarioService,
              protected comandaService: ComandaService,
              protected rxStompService: RxStompService) {
    this.subscription.add(this.usuarioService.usuarioLogado$.subscribe((usuario: Usuario) => {
      this.usuarioLogado = usuario;
    }));
  }

  ngOnInit(): void {
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Aberta)
      .subscribe(comandasAberta => this.comandasAberta = comandasAberta.map((comanda: Comanda) => {
        comanda.pedidos.map((pedido: ComandaPedido) => {
          pedido.produtoPreco = `${pedido.produtoPreco.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
          return pedido;
        });
        return comanda;
      })));
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.EmAndamento)
      .subscribe(comandasEmAndamento => this.comandasEmAndamento = comandasEmAndamento));
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Fechada)
      .subscribe(comandasFechada => this.comandasFechada = comandasFechada.map((comanda: Comanda) => {
        comanda.gorjetaGarcom = `${comanda.gorjetaGarcom.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
        comanda.totalAPagar = `${comanda.totalAPagar.toLocaleString('pt-br', {minimumFractionDigits: 2})}`;
        return comanda;
      })));

    this.subscription.add(this.rxStompService.watch('/exchange/FavoDeMel.Messaging.Commands.ComandaCommand').subscribe((message: Message) => {
      console.log(JSON.parse(message.body));
    }));
  }

  onSend() {
    const message = `Message generated at ${new Date()}`;
    this.rxStompService.publish({ destination: '/exchange/FavoDeMel.Messaging.Commands.ComandaCommand', body: message });
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

  confirmarPedido(id: any) {
    this.subscription.add(this.comandaService.confirmar(id).subscribe((comanda: Comanda) => {
      const index = this.comandasAberta.findIndex(c => c.id === id);
      this.comandasAberta.splice(index, 1);
      this.comandasEmAndamento.push(comanda);
    }));
  }

  fecharPedido(id: any) {
    this.subscription.add(this.comandaService.fechar(id).subscribe((comanda: Comanda) => {
      const index = this.comandasEmAndamento.findIndex(c => c.id === id);
      this.comandasEmAndamento.splice(index, 1);
      this.comandasFechada.push(comanda);
    }));
  }

  inserirComanda(comanda: Comanda) {
    this.comandasAberta.push(comanda);
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
