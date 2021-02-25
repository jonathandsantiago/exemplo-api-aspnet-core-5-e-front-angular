import {Component, OnDestroy, OnInit, TemplateRef} from '@angular/core';
import {Subscription} from 'rxjs';
import {ComandaService} from '../../services/comanda.service';
import {Comanda, ComandaSituacao} from '../../models/comanda';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {FormBuilder} from '@angular/forms';

@Component({
  selector: 'app-comanda',
  templateUrl: './comanda.component.html',
  styleUrls: ['./comanda.component.scss']
})
export class ComandaComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  comandasAberta: Comanda[];
  comandasEmAndamento: Comanda[];
  comandasFechada: Comanda[];

  modalRef: BsModalRef;
  mensagemConfirmacao: string;

  constructor(protected modalService: BsModalService,
              protected formBuilder: FormBuilder,
              protected comandaService: ComandaService) {
  }

  ngOnInit(): void {
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Aberta)
      .subscribe(comandasAberta => this.comandasAberta = comandasAberta));
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.EmAndamento)
      .subscribe(comandasEmAndamento => this.comandasEmAndamento = comandasEmAndamento));
    this.subscription.add(this.comandaService.obterTodosPorSituao(ComandaSituacao.Fechada)
      .subscribe(comandasFechada => this.comandasFechada = comandasFechada));
  }

  abrirModal(template: TemplateRef<any>, content?: any, largura = 'lg', message = null) {
    this.modalRef = this.modalService.show(template,
      {
        class: `modal-${largura} modal-dialog-centered`,
        ignoreBackdropClick: true
      }) as BsModalRef;

    if (message) {
      this.mensagemConfirmacao = message;
    }

    this.modalRef.content = content;
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
